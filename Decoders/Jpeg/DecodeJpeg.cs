// -----------------------------------------------------------------------
// <copyright file="DecodeJpeg.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// JPEGs are written in Big Endian format
// Specifications:
// https://www.w3.org/Graphics/JPEG/itu-t81.pdf
// https://www.w3.org/Graphics/JPEG/jfif3.pdf
// https://en.wikipedia.org/wiki/JPEG_File_Interchange_Format
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Jpeg;

using System;
using System.IO;
using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Exif;

/// <summary>
/// Decoder for the JPEG image format
/// </summary>
internal class DecodeJpeg : IDecoder
{
  public IDecoder? DetectFormat( BinaryReader reader )
  {
    // Set it to the start
    reader.BaseStream.Position = 0;

    // Valid the stream is long enough
    if( JpegConstants.MagicNumber.Length >= reader.BaseStream.Length )
    {
      // Cant be a valid JPEG format but perhaps its another one
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
    _width = 0;
    _height = 0;
    _exifDataStart = 0;
    _horizontalResolution = 0;
    _verticalResolution = 0;
    _resolutionUnit = DensityUnit.PixelsPerInch;

    bool eof = false;
    while( !eof )
    {
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
        var startOfSegment = reader.BaseStream.Position;

        if( markerType == JpegConstants.Markers.Jfif.App )
        {
          // Check there is a valid APP0 marker
          if( _remainingInFrame >= JpegConstants.Markers.Jfif.SegmentLength )
          {
            DecodeJfif( reader );
          }
        }
        else if( markerType == ExifConstants.App )
        {
          DecodeExif( reader );
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
          _height = DataConversion.Int16FromBigEndianBuffer( frameBuffer, 0 );
          _width = DataConversion.Int16FromBigEndianBuffer( frameBuffer, 2 );

          _remainingInFrame -= 5;
        }

        // Jump to end of segment
        reader.BaseStream.Seek( startOfSegment + _remainingInFrame, SeekOrigin.Begin );
      }

      if( _width > 0 && _height > 0 && _horizontalResolution > 0 && _verticalResolution > 0 )
      {
        // Short circuit when all the elements exist
        var horizontalDpi = UnitConvertor.ToDpi( _resolutionUnit, _horizontalResolution );
        var verticalDpi = UnitConvertor.ToDpi( _resolutionUnit, _verticalResolution );

        reader.BaseStream.Position = 0;
        return new ImageDetails( _width, _height, horizontalDpi, verticalDpi, "image/jpeg" );
      }

      if( reader.BaseStream.Position >= reader.BaseStream.Length )
      {
        eof = true;
      }
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
      // Ignore Position 5 = majorVersion
      // Ignore Position 6 = minorVersion
      var densityUnits = (DensityUnit)data[ 7 ];
      short xDensity = DataConversion.Int16FromBigEndianBuffer( data, 8 );
      short yDensity = DataConversion.Int16FromBigEndianBuffer( data, 10 );
      if( xDensity > 0 && yDensity > 0 )
      {
        _resolutionUnit = densityUnits;
        _horizontalResolution = xDensity;
        _verticalResolution = yDensity;
      }
    }
  }

  // TODO: Move to Exif decoder
  private void DecodeExif( BinaryReader reader )
  {
    // Exif data is little endian

    // Buffer only the data we need
    var data = reader.ReadBytes( ExifConstants.MagicBytes.Length );
    if( DecoderHelper.MatchesMagic( data, ExifConstants.MagicBytes ) )
    {
      // JPEG is big endian, but Exif data is stored as TIFF and
      // That contains bytes as to what byte order the TIFF data is
      // written as
      _exifDataStart = reader.BaseStream.Position;

      // Get TIFF header
      data = reader.ReadBytes( TiffConstants.HeaderLength );
      _exifByteOrder = ByteOrderHelper.GetTiffByteOrder( data );

      var signature = DataConversion.Int16FromBuffer( data, 2, _exifByteOrder );
      if( signature != 42 )
      {
        ExceptionHelper.Throw( reader, "Invalid TIFF header signature" );
      }

      // Find the tags containing the resolution
      if( _horizontalResolution == 0 )
      {
        // We have an EXIF header so we can reset values to the EXIF default
        // When the image resolution is unknown, 72 [dpi] is designated
        _horizontalResolution = 72;
        _verticalResolution = 72;
        _resolutionUnit = DensityUnit.PixelsPerInch;
      }

      // Offset is from the beginning of this header (i.e. at the point of the byte order marker)
      var ifdOffsetStart = DataConversion.Int32FromBuffer( data, 4, _exifByteOrder ) /*- TiffConstants.HeaderLength*/;

      do
      {
        reader.BaseStream.Seek( _exifDataStart + ifdOffsetStart, SeekOrigin.Begin );

        var ifdDirectoryBuffer = reader.ReadBytes( 2 );
        var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer, 0, _exifByteOrder );
        for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
        {
          DecodeDirectory( reader );
        }

        // Last entry after directories is the offset to next IFD
        var nextIfdBuffer = reader.ReadBytes( 4 );
        ifdOffsetStart = DataConversion.Int32FromBuffer( nextIfdBuffer, 0, _exifByteOrder );
      } while( ifdOffsetStart != 0 );

      // Finished all IFDs
    }
  }

  private void DecodeDirectory( BinaryReader reader )
  {
    // Pre-buffer all bytes needed
    var directoryBuffer = reader.ReadBytes( 12 );

    var exifTag = DataConversion.Int16FromBuffer( directoryBuffer, 0, _exifByteOrder );
    var exifDataType = DataConversion.Int16FromBuffer( directoryBuffer, 2, _exifByteOrder );
    var exifValue = DataConversion.Int32FromBuffer( directoryBuffer, 8, _exifByteOrder );
    var componentCount = DataConversion.Int32FromBuffer( directoryBuffer, 4, _exifByteOrder );
    if( exifTag == ExifConstants.Tags.ResolutionX )
    {
      _horizontalResolution = GetRationalValue( reader, exifDataType, exifValue, componentCount );
    }
    else if( exifTag == ExifConstants.Tags.ResolutionY )
    {
      _verticalResolution = GetRationalValue( reader, exifDataType, exifValue, componentCount );
    }
    else if( exifTag == ExifConstants.Tags.ImageWidth )
    {
      // Use JPEG/JFIF image dimensions in preference
      if( _width == 0 )
      {
        //var svalue = buffer.AsSpan( 8, 4 );
        //_width = GetRationalValue( reader, exifDataType, exifValue, componentCount );
      }
    }
    else if( exifTag == ExifConstants.Tags.ImageHeight )
    {
      // Use JPEG/JFIF image dimensions in preference
      if( _height == 0 )
      {
        //var svalue = buffer.AsSpan( 8, 4 );
        //_height = GetRationalValue( reader, exifDataType, exifValue, componentCount );
      }
    }
    else if( exifTag == ExifConstants.Tags.ResolutionUnit )
    {
      var tagValue = DataConversion.Int16FromBuffer( directoryBuffer, 8, _exifByteOrder );

      _resolutionUnit = tagValue switch
      {
        2 => DensityUnit.PixelsPerInch,
        3 => DensityUnit.PixelsPerCentimeter,
        _ => DensityUnit.PixelsPerInch, // Unknown or not set, assume inches
      };
    }
  }

  private double GetRationalValue( BinaryReader reader, short type, int exifValue, int dataCount )
  {
    var tagDataSize = GetTagDataSize( type );

    var currentStreamPosition = reader.BaseStream.Position;

    // Rational type is 8 bytes, so size * 8 is the data size
    // Total data length is larger than 4bytes, so next 4bytes contains an offset to data
    reader.BaseStream.Seek( _exifDataStart + exifValue, SeekOrigin.Begin );
    var dataValue = reader.ReadBytes( dataCount * tagDataSize );
    if( dataCount * tagDataSize == 8 )
    {
      var enumer = DataConversion.Int32FromBuffer( dataValue, 0, _exifByteOrder );
      var denom = DataConversion.Int32FromBuffer( dataValue, 4, _exifByteOrder );

      // Reset current position
      reader.BaseStream.Position = currentStreamPosition;

      return enumer / denom;
    }

    return 0;
  }

  private static int GetTagDataSize( int type )
  {
    switch( type )
    {
      case 1: // unsigned byte
      case 2: // ascii strings    
      case 6: // signed byte
        return 1;

      case 3: // unsigned short
      case 8: // signed short
        return 2;

      case 4: // unsigned long
      case 9: // signed long
      case 11: // single float
        return 4;

      case 5: // unsigned rational
      case 10: // signed rational
      case 12: // double float
        return 8;
    }

    return 0;
  }

  private int _width = 0;
  private int _height = 0;

  private long _exifDataStart = 0;
  private double _horizontalResolution = 0;
  private double _verticalResolution = 0;
  private DensityUnit _resolutionUnit = DensityUnit.PixelsPerInch;

  private int _remainingInFrame = 0;
  private ByteOrder _exifByteOrder = ByteOrder.Unknown;

  const string ErrorMessage = "Invalid JPEG format";
}
