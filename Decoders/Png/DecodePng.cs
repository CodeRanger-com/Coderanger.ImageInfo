// -----------------------------------------------------------------------
// <copyright file="DecodePng.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// PNGs are written in Big Endian (Network Byte Order) format
// Specifications:
// https://www.w3.org/TR/PNG/
// https://exiftool.org/TagNames/PNG.html
// https://ftp-osl.osuosl.org/pub/libpng/documents/pngext-1.5.0.html#C.eXIf
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Exif;
using Coderanger.ImageInfo.Decoders.Png.Helpers;
using Coderanger.ImageInfo.Decoders.Png.ChunkParts;
using Coderanger.ImageInfo.Decoders.Metadata;
using Coderanger.ImageInfo.Decoders.Metadata.Png;
using Coderanger.ImageInfo.Decoders.Metadata.Xmp;

/// <summary>
/// Decoder for the PNG image format
/// </summary>
internal class DecodePng : IDecoder
{
  public static IDecoder? DetectFormat( BinaryReader reader )
  {
    // Set it to the start of the stream
    reader.Position( 0 );

    // Validate the stream is long enough
    if( PngConstants.MagicNumber.Length >= reader.Length() )
    {
      // Cant be a valid format but perhaps its another one
      return null;
    }

    var header = reader.ReadBytes( PngConstants.MagicNumber.Length );
    var headerValue = DataConversion.UInt64FromBigEndianBuffer( header );
    if( headerValue == PngConstants.MagicNumberValue )
    {
      return new DecodePng();
    }

    return null;
  }

  public ImageDetails Decode( BinaryReader reader )
  {
    // Stream will be at the position it was last left at when
    // detecting the format, so it will be at the end of the signature
    // header

    bool eof = false;
    while( !eof )
    {
      if( reader.Position() >= reader.Length() )
      {
        eof = true;
        continue;
      }

      // Extract chunks and evaluate if its one that is required.
      // Any unknown chunks will return the UnknownChunk object so
      // that it can still be skipped
      var chunk = ChunkFactory.ReadChunk( reader );
      if( chunk is HeaderChunk header )
      {
        header.LoadData( reader );
        _width = header.Width;
        _height = header.Height;
      }
      else if( chunk is PhysicalDimensionsChunk physical )
      {
        physical.LoadData( reader );
        _resolutionX = physical.XResolutionDpi;
        _resolutionY = physical.YResolutionDpi;
      }
      else if( chunk is TimeChunk time )
      {
        time.LoadData( reader );
      }
      else if( chunk is TextChunk txt )
      {
        txt.LoadData( reader );
        if( txt.Text != null )
        {
          _metadataItems.Add( txt.Text );
        }
      }
      else if( chunk is TextCompressedChunk txtCompressed )
      {
        txtCompressed.LoadData( reader );
        if( txtCompressed.Text != null )
        {
          _metadataItems.Add( txtCompressed.Text );
        }
      }
      else if( chunk is TextInternationalChunk txtInternational )
      {
        txtInternational.LoadData( reader );
        if( txtInternational.Text != null )
        {
          if( txtInternational.Text.Keyword == PngConstants.Chunks.XmpKeyword )
          {
            var xmpData = XmpTagFactory.Create();
            xmpData.SetValue( txtInternational.Text.TextValue );
            _xmpDataValue = xmpData;
          }
          else
          {
            _metadataItems.Add( txtInternational.Text );
          }
        }
      }
      else if( chunk is ExifChunk exif )
      {
        exif.LoadData( reader );
        _exifDecoder = exif.ExifData;
      }
      else if( chunk is EndChunk )
      {
        eof = true;
        continue;
      }

      // Move position to end of chunk
      chunk.SkipToEnd( reader );
    }

    if( _width > 0 && _height > 0 )
    {
      var tags = BuildTagList();

      return new ImageDetails( _width, _height, _resolutionX, _resolutionY, PngConstants.MimeType, tags.Count > 0 ? tags : null );
    }

    throw ExceptionHelper.Throw( reader, ErrorMessage );
  }

  private Dictionary<MetadataProfileType, List<IMetadataTypedValue>> BuildTagList()
  {
    Dictionary<MetadataProfileType, List<IMetadataTypedValue>> tags = new();

    if( _exifDecoder?.HasTags() ?? false )
    {
      _exifDecoder.AddTagsToProfile( ref tags );
    }

    if( _metadataItems.Count > 0 )
    {
      if( !tags.TryGetValue( MetadataProfileType.PngText, out var value ) )
      {
        value = new List<IMetadataTypedValue>();
        tags.Add( MetadataProfileType.PngText, value );
      }

      foreach( var data in _metadataItems )
      {
        value.Add( new PngMetadata( data ) );
      }
    }

    if( _xmpDataValue != null )
    {
      if( !tags.TryGetValue( MetadataProfileType.Xmp, out var value ) )
      {
        value = new List<IMetadataTypedValue>();
        tags.Add( MetadataProfileType.Xmp, value );
      }

      value.Add( _xmpDataValue );
    }

    return tags;
  }

  private long _width;
  private long _height;
  private int _resolutionX = -1;
  private int _resolutionY = -1;
  private readonly List<PngText> _metadataItems = new();
  private DecodeExif? _exifDecoder;
  private IMetadataTypedValue? _xmpDataValue = null;

  const string ErrorMessage = "Invalid PNG format";
}
