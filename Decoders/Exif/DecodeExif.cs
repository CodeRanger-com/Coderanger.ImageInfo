// -----------------------------------------------------------------------
// <copyright file="DecodeExif.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// https://www.cipa.jp/std/documents/e/DC-008-2012_E.pdf
// https://www.media.mit.edu/pia/Research/deepview/exif.html
// </comment>
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
    ExtractTagsFromIfd( ExifProfileType.Exif, ifdOffset, ref _exifTags );

    ExtractTagsFromIfd( ExifProfileType.Exif, _ifdExifOffset, ref _exifTags );
    ExtractTagsFromIfd( ExifProfileType.Gps, _ifdGpsOffset, ref _gpsTags );
    ExtractTagsFromIfd( ExifProfileType.Interoperability, _ifdInterOffset, ref _interTags );

    // Finished all IFDs
    _processed = true;
  }

  private void ExtractTagsFromIfd( ExifProfileType profile, int ifdOffset, ref Dictionary<ushort, IExifValue> tags )
  {
    if( ifdOffset > 0 )
    {
      do
      {
        _reader.BaseStream.Seek( _segmentStart + ifdOffset, SeekOrigin.Begin );

        var ifdDirectoryBuffer = _reader.ReadBytes( 2 );
        var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer, 0, _exifByteOrder );
        for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
        {
          DiscoverIfdOffsets();

          var dataValue = ExifTagValueFactory.Create( profile, _reader, _segmentStart, _exifByteOrder );
          if( dataValue != null && !tags.ContainsKey( dataValue.Tag ) )
          {
            tags.Add( dataValue.Tag, dataValue );
          }
        }

        // Last entry after directories is the offset to next IFD
        var nextIfdBuffer = _reader.ReadBytes( 4 );
        ifdOffset = DataConversion.Int32FromBuffer( nextIfdBuffer, 0, _exifByteOrder );
      } while( ifdOffset != 0 );
    }
  }

  private bool DiscoverIfdOffsets()
  {
    try
    {
      var directoryBuffer = _reader.ReadBytes( IfdOffsetSegmentSize );
      var (tag, _, _, valueBuffer) = ExifTagValueFactory.CrackData( directoryBuffer, _exifByteOrder );
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
    if( _exifTags.TryGetValue( ExifTag.ResolutionUnit, out var resUnitTag ) )
    {
      DensityUnit? exifDensityUnit = null;

      if( ( (ExifUShort)resUnitTag ).TryGetValue( out var unitVal ) )
      {
        exifDensityUnit = unitVal?.Value switch
        {
          2 => DensityUnit.PixelsPerInch,
          3 => DensityUnit.PixelsPerCentimeter,
          _ => DensityUnit.PixelsPerInch, // Unknown or not set, assume inches
        };
      }

      if( !exifDensityUnit.HasValue )
      {
        return;
      }

      if( _exifTags.TryGetValue( ExifTag.XResolution, out var xResTag ) )
      {
        if( xResTag is ExifURational exifValue && exifValue.TryGetValue( out var dpi ) && dpi?.Value != null )
        {
          HorizontalDpi = UnitConvertor.ToDpi( exifDensityUnit.Value, (double)dpi.Value );
        }
      }
      if( _exifTags.TryGetValue( ExifTag.YResolution, out var yResTag ) )
      {
        if( yResTag is ExifURational exifValue && exifValue.TryGetValue( out var dpi ) && dpi?.Value != null )
        {
          VerticalDpi = UnitConvertor.ToDpi( exifDensityUnit.Value, (double)dpi.Value );
        }
      }
    }
  }

  internal int HorizontalDpi { get; set; } = 0;
  internal int VerticalDpi { get; set; } = 0;

  internal Dictionary<ushort, IExifValue> GetExifTags()
  {
    return _exifTags;
  }

  internal Dictionary<ushort, IExifValue> GetGpsTags()
  {
    return _gpsTags;
  }

  internal Dictionary<ushort, IExifValue> GetInteroperabilityfTags()
  {
    return _interTags;
  }

  private const int TiffSignatureValue = 42;

  private Dictionary<ushort, IExifValue> _exifTags = new();
  private Dictionary<ushort, IExifValue> _gpsTags = new();
  private Dictionary<ushort, IExifValue> _interTags = new();

  private long _segmentStart = 0;
  private bool _processed = false;
  private TiffByteOrder _exifByteOrder = TiffByteOrder.Unknown;

  private readonly BinaryReader _reader;
}
