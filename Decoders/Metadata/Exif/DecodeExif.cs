﻿// -----------------------------------------------------------------------
// <copyright file="DecodeExif.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// Exifs state which endian format is used within the header segment:
// https://en.wikipedia.org/wiki/Exif
// Specifications:
// https://www.cipa.jp/std/documents/e/DC-008-2012_E.pdf
// https://www.itu.int/itudoc/itu-t/com16/tiff-fx/docs/tiff6.pdf
// https://www.media.mit.edu/pia/Research/deepview/exif.html
// https://www.exiv2.org/tags.html
// http://www.exif.org/Exif2-2.PDF
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;
using Coderanger.ImageInfo.Decoders.Metadata.Xmp;

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
  internal static DecodeExif? DecodeFromReader( BinaryReader reader, bool validateSignature = true )
  {
    var decoder = new DecodeExif( reader );
    if( decoder.Decode( validateSignature ) )
    {
      return decoder;
    }  

    return null;
  }

  internal static DecodeExif? DecodeFromBuffer( byte[] buffer, int count, bool validateSignature )
  {
    using var stream = new MemoryStream( buffer, 0, count );
    using var reader = new BinaryReader( stream );

    var decoder = new DecodeExif( reader );
    if( decoder.Decode( validateSignature ) )
    {
      return decoder;
    }

    return null;
  }

  internal int HorizontalDpi { get; set; } = 0;
  internal int VerticalDpi { get; set; } = 0;

  internal bool AddTagsToProfile( ref Metadata metadata )
  {
    var tags = _metadata.GetTags();
    if( tags is not null )
    {
      foreach( var profile in tags.Keys )
      {
        if( tags.TryGetValue( profile, out var profileTags ) )
        {
          metadata.AddTags( profile, profileTags );
        }
      }
      return true;
    }

    return false;
  }

  private DecodeExif( BinaryReader reader )
  {
    _reader = reader;
  }

  private bool Decode( bool validateSignature = true )
  {
    if( _processed )
    {
      // Already done
      return false;
    }

    // Buffer only the data we need
    if( validateSignature )
    {
      var signature = _reader.ReadBytes( ExifConstants.MagicBytes.Length );
      var signatureValue = DataConversion.UInt32FromBigEndianBuffer( signature.AsSpan( 0, 4 ) );
      if( signatureValue != ExifConstants.MagicBytesVersion )
      {
        return false;
      }
    }

    _segmentStart = _reader.Position();

    var segmentData = ValidateHeader();
    ExtractTagsFromSegment( segmentData.AsSpan() );

    ExtractResolutionInfo();

    return true;
  }

  /// <summary>
  /// Validates whether this is a valid TIFF segment
  /// </summary>
  /// <returns></returns>
  private byte[] ValidateHeader()
  {
    byte[] data = _reader.ReadBytes( TiffConstants.HeaderLength );
    _exifByteOrder = ByteOrderHelper.GetTiffByteOrder( data.AsSpan( 0, 2 ) );

    var signature = DataConversion.Int16FromBuffer( data.AsSpan( 2, 2 ), _exifByteOrder );
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
  private void ExtractTagsFromSegment( ReadOnlySpan<byte> data )
  {
    // Offset is from the beginning of this header (i.e. at the point of the byte order marker)
    var ifdOffset = DataConversion.Int32FromBuffer( data.Slice( 4 ), _exifByteOrder );
    ExtractTagsFromIfd( MetadataProfileType.Exif, ifdOffset );

    ExtractTagsFromIfd( MetadataProfileType.Exif, _ifdSubExifOffset );
    ExtractTagsFromIfd( MetadataProfileType.Gps, _ifdGpsOffset );
    ExtractTagsFromIfd( MetadataProfileType.Interoperability, _ifdInterOffset );

    ExtractIptc();
    ExtractXmp();

    // Finished all IFDs
    _processed = true;
  }

  private void ExtractIptc()
  {
    if( _ifdIptcOffset > 0 )
    {
      if( _segmentStart + _ifdIptcOffset + ExifConstants.ExifDirectorySize >= _reader.Length() )
      {
        // Ensure we havent gone out of bounds and there are actually 12 bytes
        // left for the whole IFD chunk
        return;
      }

      _reader.BaseStream.Seek( _segmentStart + _ifdIptcOffset, SeekOrigin.Begin );
    }
  }

  private void ExtractXmp()
  {
    if( _idXmpOffset > 0 && _idXmpSize > 0 )
    {
      if( _segmentStart + _idXmpOffset + _idXmpSize >= _reader.Length() )
      {
        // Ensure we havent gone out of bounds and there are actually enough
        // bytes left for the whole XMP chunk
        return;
      }

      _reader.BaseStream.Seek( _segmentStart + _idXmpOffset, SeekOrigin.Begin );

      var xmp = _reader.ReadBytes( _idXmpSize );
      XmpTagFactory.AddXmp( xmp.AsSpan(), ref _metadata );
    }
  }

  private void ExtractTagsFromIfd( MetadataProfileType profile, int ifdOffset )
  {
    if( ifdOffset > 0 )
    {
      do
      {
        if( _segmentStart + ifdOffset + ExifConstants.ExifDirectorySize >= _reader.Length() )
        {
          // Ensure we havent gone out of bounds and there are actually 12 bytes
          // left for the whole IFD chunk
          break;
        }

        _reader.BaseStream.Seek( _segmentStart + ifdOffset, SeekOrigin.Begin );

        var ifdDirectoryBuffer = _reader.ReadBytes( 2 );
        var directoryCount = DataConversion.Int16FromBuffer( ifdDirectoryBuffer.AsSpan( 0, 2 ), _exifByteOrder );
        for( var directoryIndex = 0; directoryIndex < directoryCount; directoryIndex++ )
        {
          var isIfdOffset = DiscoverIfdOffsets();
          if( !isIfdOffset )
          {
            var dataValue = ExifTagValueFactory.Create( profile, _reader, _segmentStart, _exifByteOrder );
            if( dataValue is not null )
            {
              dataValue.SetValue( null );
              _metadata.AddTag( profile, dataValue );
            }
          }
          else
          {
            _reader.Skip( ExifConstants.ExifDirectorySize );
          }
        }

        // Last entry after directories should be the offset to next IFD or 4 nulls,
        // but this isnt always the case as ImageSharp had been saving just 2 nulls
        // up to and including v1.04
        var nextIfdBuffer = _reader.ReadBytes( 4 );
        if( nextIfdBuffer.Length == 0 )
        {
          break;
        }
        ifdOffset = DataConversion.Int32FromBuffer( nextIfdBuffer.AsSpan( 0, 4 ), _exifByteOrder );
      } while( ifdOffset != 0 );
    }
  }

  private bool DiscoverIfdOffsets()
  {
    try
    {
      var directoryBuffer = _reader.ReadBytes( IfdOffsetSegmentSize );
      var crackedData = ExifTagValueFactory.CrackedData.Create( directoryBuffer.AsSpan(), _exifByteOrder );
      var value = DataConversion.Int32FromBuffer( crackedData.DataBuffer, _exifByteOrder );

      switch( crackedData.Tag )
      {
        case (ushort)ExifConstants.IfdOffset.Exif:
          _ifdSubExifOffset = value;
          return true;

        case (ushort)ExifConstants.IfdOffset.Gps:
          _ifdGpsOffset = value;
          return true;

        case (ushort)ExifConstants.IfdOffset.Iptc:
          _ifdIptcOffset = value;
          return true;

        case (ushort)ExifConstants.IfdOffset.Xmp:
          _idXmpOffset = value;
          _idXmpSize = crackedData.ComponentCount;
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

  /// <summary>
  /// Extract and store only the resolution info
  /// </summary>
  private void ExtractResolutionInfo()
  {
    var resUnitTag = _metadata.FindTag( MetadataProfileType.Exif, ExifTag.ResolutionUnit );
    if( resUnitTag is not null )
    {
      DensityUnit? exifDensityUnit = null;

      if( resUnitTag.TryGetValue( out var unitVal ) && unitVal is not null )
      {
        var enumValue = (MetadataEnumValue)unitVal.Value;

        exifDensityUnit = enumValue.EnumValue switch
        {
          "2" => DensityUnit.PixelsPerInch,
          "3" => DensityUnit.PixelsPerCentimeter,
          _ => DensityUnit.PixelsPerInch, // Unknown or not set, assume inches
        };
      }

      if( !exifDensityUnit.HasValue )
      {
        return;
      }

      var xResTag = _metadata.FindTag( MetadataProfileType.Exif, ExifTag.XResolution );
      if( xResTag is not null )
      {
        if( xResTag is ExifURational exifValue && exifValue.TryGetValue( out var dpi ) && dpi?.Value is not null )
        {
          var rational = dpi.Value as Rational;
          HorizontalDpi = UnitConvertor.ToDpi( exifDensityUnit.Value, rational?.ToDouble() ?? 0 );
        }
      }

      var yResTag = _metadata.FindTag( MetadataProfileType.Exif, ExifTag.YResolution );
      if( yResTag is not null )
      {
        if( yResTag is ExifURational exifValue && exifValue.TryGetValue( out var dpi ) && dpi?.Value is not null )
        {
          var rational = dpi.Value as Rational;
          VerticalDpi = UnitConvertor.ToDpi( exifDensityUnit.Value, rational?.ToDouble() ?? 0 );
        }
      }
    }
  }

  private int _ifdSubExifOffset = -1;
  private int _ifdGpsOffset = -1;
  private int _ifdIptcOffset = -1;
  private int _idXmpOffset = -1;
  private int _idXmpSize = 0;
  private int _ifdInterOffset = -1;
  private long _segmentStart = 0;
  private bool _processed = false;
  private ByteOrder _exifByteOrder = ByteOrder.Unknown;

  private Metadata _metadata = new();
  private readonly BinaryReader _reader;

  private const short IfdOffsetSegmentSize = 12;
  private const int TiffSignatureValue = 42;
}
