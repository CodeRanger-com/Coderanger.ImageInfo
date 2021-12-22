// -----------------------------------------------------------------------
// <copyright file="DecodeExif.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// JPEG is big endian, but Exif data is stored as TIFF and
/// that contains bytes as to what byte order the TIFF data is
/// written as
/// </remarks>
internal class DecodeExif
{
  internal static DecodeExif? DecodeFromReader( BinaryReader reader )
  {
    var decoder = new DecodeExif( reader );
    if( decoder.Decode() )
    {
      return decoder;
    }  

    return null;
  }

  private DecodeExif( BinaryReader reader )
  {
    _reader = reader;
  }

  private bool Decode()
  {
    if( _processed )
    {
      // Already done
      return false;
    }

    // Buffer only the data we need
    var data = _reader.ReadBytes( ExifConstants.MagicBytes.Length );
    if( DecoderHelper.MatchesMagic( data, ExifConstants.MagicBytes ) )
    {
      _segmentStart = _reader.BaseStream.Position;

      var segmentData = ValidateHeader();
      ExtractTagsFromSegment( segmentData );

      ExtractResolutionInfo();

      return true;
    }

    return false;
  }

  /// <summary>
  /// Validates whether this is a valid TIFF segment
  /// </summary>
  /// <returns></returns>
  private byte[] ValidateHeader()
  {
    byte[] data = _reader.ReadBytes( TiffConstants.HeaderLength );
    _exifByteOrder = TiffByteOrderHelper.GetTiffByteOrder( data );

    var signature = DataConversion.Int16FromBuffer( data, 2, _exifByteOrder );
    if( signature != TiffSignatureValue )
    {
      ExceptionHelper.Throw( _reader, "Invalid TIFF header signature" );
    }

    return data;
  }

  /// <summary>
  /// Extract all EXIF tags from all directories in the chunk
  /// </summary>
  /// <param name="data"></param>
  private void ExtractTagsFromSegment( byte[] data )
  {
    // Offset is from the beginning of this header (i.e. at the point of the byte order marker)
    var ifdOffset = DataConversion.Int32FromBuffer( data, 4, _exifByteOrder );

    do
    {
      _reader.BaseStream.Seek( _segmentStart + ifdOffset, SeekOrigin.Begin );

      var ifdDirectoryBuffer = _reader.ReadBytes( 2 );
      var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer, 0, _exifByteOrder );
      for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
      {
        SetSpecialOffset();

        var dataValue = ExifTagValueFactory.Create( ExifProfileType.Exif, _reader, _segmentStart, _exifByteOrder );
        if( dataValue != null && !ExifTags.ContainsKey( dataValue.Component.Tag ) )
        {
          ExifTags.Add( dataValue.Component.Tag, dataValue );
        }
      }

      // Last entry after directories is the offset to next IFD
      var nextIfdBuffer = _reader.ReadBytes( 4 );
      ifdOffset = DataConversion.Int32FromBuffer( nextIfdBuffer, 0, _exifByteOrder );
    } while( ifdOffset != 0 );

    if( _ifdExifOffset > 0 )
    {
      ifdOffset = _ifdExifOffset;
      do
      {
        _reader.BaseStream.Seek( _segmentStart + ifdOffset, SeekOrigin.Begin );

        var ifdDirectoryBuffer = _reader.ReadBytes( 2 );
        var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer, 0, _exifByteOrder );
        for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
        {
          SetSpecialOffset();

          var dataValue = ExifTagValueFactory.Create( ExifProfileType.Exif, _reader, _segmentStart, _exifByteOrder );
          if( dataValue != null && !ExifTags.ContainsKey( dataValue.Component.Tag ) )
          {
            ExifTags.Add( dataValue.Component.Tag, dataValue );
          }
        }

        // Last entry after directories is the offset to next IFD
        var nextIfdBuffer = _reader.ReadBytes( 4 );
        ifdOffset = DataConversion.Int32FromBuffer( nextIfdBuffer, 0, _exifByteOrder );
      } while( ifdOffset != 0 );
    }

    if( _ifdGpsOffset > 0 )
    {
      ifdOffset = _ifdGpsOffset;
      do
      {
        _reader.BaseStream.Seek( _segmentStart + ifdOffset, SeekOrigin.Begin );

        var ifdDirectoryBuffer = _reader.ReadBytes( 2 );
        var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer, 0, _exifByteOrder );
        for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
        {
          var dataValue = ExifTagValueFactory.Create( ExifProfileType.Gps, _reader, _segmentStart, _exifByteOrder );
          if( dataValue != null && !GpsTags.ContainsKey( dataValue.Component.Tag ) )
          {
            GpsTags.Add( dataValue.Component.Tag, dataValue );
          }
        }

        // Last entry after directories is the offset to next IFD
        var nextIfdBuffer = _reader.ReadBytes( 4 );
        ifdOffset = DataConversion.Int32FromBuffer( nextIfdBuffer, 0, _exifByteOrder );
      } while( ifdOffset != 0 );
    }

    if( _ifdInterOffset > 0 )
    {
      ifdOffset = _ifdInterOffset;
      do
      {
        _reader.BaseStream.Seek( _segmentStart + ifdOffset, SeekOrigin.Begin );

        var ifdDirectoryBuffer = _reader.ReadBytes( 2 );
        var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer, 0, _exifByteOrder );
        for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
        {
          var dataValue = ExifTagValueFactory.Create( ExifProfileType.Gps, _reader, _segmentStart, _exifByteOrder );
          if( dataValue != null && !InterTags.ContainsKey( dataValue.Component.Tag ) )
          {
            InterTags.Add( dataValue.Component.Tag, dataValue );
          }
        }

        // Last entry after directories is the offset to next IFD
        var nextIfdBuffer = _reader.ReadBytes( 4 );
        ifdOffset = DataConversion.Int32FromBuffer( nextIfdBuffer, 0, _exifByteOrder );
      } while( ifdOffset != 0 );
    }

    // Finished all IFDs
    _processed = true;
  }

  private bool SetSpecialOffset()
  {
    try
    {
      var directoryBuffer = _reader.ReadBytes( IfdOffsetSegmentSize );
      var (tag, dataType, componentCount, valueBuffer) = ExifTagValueFactory.CrackData( directoryBuffer, _exifByteOrder );
      var value = DataConversion.Int32FromBuffer( valueBuffer, 0, _exifByteOrder );

      switch( tag )
      {
        case (ushort)ExifConstants.IfdOffset.Exif:
          _ifdExifOffset = value;
          return true;

        case (ushort)ExifConstants.IfdOffset.Gps:
          _ifdGpsOffset = value;
          return true;

        case (ushort)ExifConstants.IfdOffset.Inter:
          _ifdInterOffset = value;
          return true;
      }
    }
    finally
    {
      // Put stream position back
      _reader.BaseStream.Seek( -IfdOffsetSegmentSize, SeekOrigin.Current );
    }

    return false;
  }

  private const short IfdOffsetSegmentSize = 12;
  private int _ifdExifOffset = -1;
  private int _ifdGpsOffset = -1;
  private int _ifdInterOffset = -1;

  /// <summary>
  /// Extract and store only the resolution info
  /// </summary>
  private void ExtractResolutionInfo()
  {
    if( ExifTags.TryGetValue( ExifTag.ResolutionUnit, out var resUnitTag ) )
    {
      DensityUnit? exifDensityUnit = null;

      if( ( (ExifUShort)resUnitTag ).TryGetUShort( out var unitVal ) )
      {
        exifDensityUnit = unitVal switch
        {
          2 => DensityUnit.PixelsPerInch,
          3 => DensityUnit.PixelsPerCentimeter,
          _ => DensityUnit.PixelsPerInch, // Unknown or not set, assume inches
        };
      }

      if( exifDensityUnit.HasValue && ExifTags.TryGetValue( ExifTag.XResolution, out var xResTag ) )
      {
        var exifValue = xResTag as ExifURational;
        if( exifValue?.TryGetRational( out var xDpi ) ?? false )
        {
          HorizontalDpi = UnitConvertor.ToDpi( exifDensityUnit.Value, xDpi );
        }
      }
      if( exifDensityUnit.HasValue && ExifTags.TryGetValue( ExifTag.YResolution, out var yResTag ) )
      {
        var exifValue = yResTag as ExifURational;
        if( exifValue?.TryGetRational( out var yDpi ) ?? false )
        {
          VerticalDpi = UnitConvertor.ToDpi( exifDensityUnit.Value, yDpi );
        }
      }
    }
  }

  internal int HorizontalDpi { get; set; } = 0;
  internal int VerticalDpi { get; set; } = 0;
  internal Dictionary<ushort, ExifValue> ExifTags { get; set; } = new();
  internal Dictionary<ushort, ExifValue> GpsTags { get; set; } = new();
  internal Dictionary<ushort, ExifValue> InterTags { get; set; } = new();

  private const int TiffSignatureValue = 42;

  private long _segmentStart = 0;
  private bool _processed = false;
  private TiffByteOrder _exifByteOrder = TiffByteOrder.Unknown;

  private readonly BinaryReader _reader;
}
