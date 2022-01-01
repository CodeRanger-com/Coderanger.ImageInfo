// -----------------------------------------------------------------------
// <copyright file="TextInternationalChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Png;
using Coderanger.ImageInfo.Decoders.Png.Helpers;

internal struct TextInternationalChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new TextInternationalChunk( chunk );
  }

  internal TextInternationalChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      // Text chunk data are stored in 4 segments, with null termination between
      // Keyword	              1-79 bytes (Latin1 character string)
      // Null separator           1 byte (null character)
      // Compression flag         1 byte (0 = uncompressed, 1 = compressed)
      // Compression method       1 byte
      // Language tag             0 or more bytes (Latin1 character string)
      // Null separator           1 byte (null character)
      // Translated keyword       0 or more bytes (UTF8 character string)
      // Null separator           1 byte (null character)
      // Text stream              0 or more bytes compressed or uncompressed (UTF8 character string)

      var keyword = TextChunkHelper.GetTerminatedStringFromBuffer( _chunk.Data );

      // Length of title plus the null terminator
      var dataStart = keyword.ByteEndPosition + 1;

      var isCompressed = Convert.ToBoolean( _chunk.Data[ dataStart++ ] );

      // Compression method is 0 for zlib deflate, other values reserved for future
      var compressionMethod = _chunk.Data[ dataStart++ ];

      var language = TextChunkHelper.GetTerminatedStringFromBuffer( _chunk.Data.AsSpan( dataStart ) );
      dataStart += language.ByteEndPosition + 1; // Move past end null

      var translatedKeyword = TextChunkHelper.GetTerminatedStringFromBuffer( _chunk.Data.AsSpan( dataStart ), StringEncoding.Utf8 );
      dataStart += translatedKeyword.ByteEndPosition + 1; // Move past end null

      string? value;
      if( isCompressed && compressionMethod == 0 )
      {
        var decompressor = new TextDecompressor( _chunk.Data.AsSpan( dataStart ) );
        value = decompressor.GetString( StringEncoding.Utf8 );
      }
      else
      {
        var valueBuffer = _chunk.Data.AsSpan( dataStart );
        value = DataConversion.ConvertBuffer( valueBuffer, StringEncoding.Utf8 );
      }

      Text = new PngText( keyword.Value, value, language.Value, translatedKeyword.Value );
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  public PngText? Text { get; private set; } = null;

  private readonly ChunkBase _chunk;
}
