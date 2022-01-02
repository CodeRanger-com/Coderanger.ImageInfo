// -----------------------------------------------------------------------
// <copyright file="DecodeJpeg.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// JPEGs are written in Big Endian format
// Specifications:
// https://en.wikipedia.org/wiki/JPEG_File_Interchange_Format
// https://www.w3.org/Graphics/JPEG/itu-t81.pdf
// https://www.w3.org/Graphics/JPEG/jfif3.pdf
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Jpeg;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Exif;

/// <summary>
/// Decoder for the JPEG image format
/// </summary>
internal class DecodeJpeg : IDecoder
{
  public IDecoder? DetectFormat( BinaryReader reader )
  {
    // Set it to the start of the stream
    reader.Position( 0 );

    // Validate the stream is long enough
    if( JpegConstants.MagicNumber.Length >= reader.Length() )
    {
      // Cant be a valid format but perhaps its another one
      return null;
    }

    var header = reader.ReadBytes( JpegConstants.MagicNumber.Length );
    if( header.SequenceEqual( JpegConstants.MagicNumber ) )
    {
      return this;
    }

    return null;
  }

  public ImageDetails Decode( BinaryReader reader )
  {
    // Stream will be at the position it was last left at when
    // detecting the format, so it will be at the end of the signature
    // header

    Reset();

    bool eof = false;
    while( !eof )
    {
      if( reader.Position() >= reader.Length() )
      {
        eof = true;
        continue;
      }

      // Look for frame start before starting
      var markerPrefix = reader.ReadBytes( 2 );
      if( markerPrefix[ 0 ] != JpegConstants.Markers.Start )
      {
        continue;
      }

      _remainingInFrame = 0;
      var markerType = markerPrefix[ 1 ];
      switch( markerType )
      {
        // start and end of the image
        case JpegConstants.Markers.ImageStart:
          _remainingInFrame = 0;
          continue;

        // end of image, exit
        case JpegConstants.Markers.ImageEnd:
          _remainingInFrame = 0;
          eof = true;
          continue;

        // restart interval
        case JpegConstants.Markers.Restart:
          _remainingInFrame = 2;
          break;

        // the next two bytes are the length
        default:
          _remainingInFrame = reader.ReadBigEndianUInt16() - 2;
          break;
      }

      // Process the data in the frame
      if( _remainingInFrame > 0 )
      {
        var startOfSegment = reader.Position();

        if( markerType == JpegConstants.Markers.Jfif.App )
        {
          // Check there is a valid APP0/JFIF marker
          if( _remainingInFrame >= JpegConstants.Markers.Jfif.SegmentLength )
          {
            DecodeJfif( reader );
          }
        }
        else if( markerType == ExifConstants.App )
        {
          _exifDecoder = DecodeExif.DecodeFromReader( reader );
        }
        else if( markerType == JpegConstants.Markers.BaselineStart
                || markerType == JpegConstants.Markers.ExtendedSequentialStart
                || markerType == JpegConstants.Markers.ProgressiveStart )
        {
          // skip bitplane x 1 byte
          reader.Skip( 1 );

          // Buffer the data we need
          var frameBuffer = reader.ReadBytes( 4 );

          // 2 byte width/height values
          _jfifHeight = DataConversion.Int16FromBigEndianBuffer( frameBuffer, 0 );
          _jfifWidth = DataConversion.Int16FromBigEndianBuffer( frameBuffer, 2 );

          _remainingInFrame -= 5; // buffer + bitplane
        }

        // Jump to end of segment
        reader.BaseStream.Seek( startOfSegment + _remainingInFrame, SeekOrigin.Begin );
      }

      SyncResolution();
    }

    if( _width > 0 && _height > 0 )
    {
      return new ImageDetails( _width, _height, _horizontalDpi, _verticalDpi, "image/jpeg", _exifDecoder?.GetProfileTags() );
    }

    throw ExceptionHelper.Throw( reader, ErrorMessage );
  }

  private void DecodeJfif( BinaryReader reader )
  {
    // Buffer only the data we need
    var data = reader.ReadBytes( 12 );

    if( data.Length >= JpegConstants.Markers.Jfif.MagicBytes.Length
      && data.AsSpan( 0, JpegConstants.Markers.Jfif.MagicBytes.Length ).SequenceEqual( JpegConstants.Markers.Jfif.MagicBytes ) )
    {
      _processedJfif = true;

      // Ignore Position 5 = majorVersion
      // Ignore Position 6 = minorVersion
      var densityUnits = (DensityUnit)data[ 7 ];
      short xDensity = DataConversion.Int16FromBigEndianBuffer( data, 8 );
      short yDensity = DataConversion.Int16FromBigEndianBuffer( data, 10 );
      if( xDensity > 0 && yDensity > 0 )
      {
        _jfifDensityUnits = densityUnits;
        _jfifHorizontalResolution = xDensity;
        _jfifVerticalResolution = yDensity;
      }
    }
  }

  private void SyncResolution()
  {
    if( _processedJfif || _exifDecoder != null )
    {
      _horizontalDpi = 0;
      _verticalDpi = 0;

      if( _exifDecoder != null )
      {
        _horizontalDpi = _exifDecoder.HorizontalDpi;
        _verticalDpi = _exifDecoder.VerticalDpi;
      }


      // Check whether the exif values are better?
      if( _jfifHorizontalResolution != 0 && _jfifVerticalResolution != 0 )
      {
        var xDpi = UnitConvertor.ToDpi( _jfifDensityUnits, _jfifHorizontalResolution );
        var yDpi = UnitConvertor.ToDpi( _jfifDensityUnits, _jfifVerticalResolution );

        if( xDpi > _horizontalDpi )
        {
          _horizontalDpi = xDpi;
        }

        if( yDpi > _verticalDpi )
        {
          _verticalDpi = yDpi;
        }
      }

      if( _jfifWidth != 0 && _jfifHeight != 0 )
      {
        _width = _jfifWidth;
        _height = _jfifHeight;
      }
    }
  }

  private void Reset()
  {
    _width = 0;
    _height = 0;

    _processedJfif = false;
    _jfifDensityUnits = DensityUnit.PixelsPerInch;
    _jfifHorizontalResolution = 0;
    _jfifVerticalResolution = 0;
    _jfifWidth = 0;
    _jfifHeight = 0;

    _exifDecoder = null;
  }

  private long _width = 0;
  private long _height = 0;
  private int _horizontalDpi = 0;
  private int _verticalDpi = 0;

  private DecodeExif? _exifDecoder = null;

  private bool _processedJfif = false;
  private long _jfifWidth = 0;
  private long _jfifHeight = 0;
  private DensityUnit _jfifDensityUnits = DensityUnit.PixelsPerInch;
  private short _jfifHorizontalResolution = 0;
  private short _jfifVerticalResolution = 0;

  private int _remainingInFrame = 0;

  const string ErrorMessage = "Invalid JPEG format";
}
