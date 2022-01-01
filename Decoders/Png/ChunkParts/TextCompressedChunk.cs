// -----------------------------------------------------------------------
// <copyright file="TextCompressedChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Png;
using Coderanger.ImageInfo.Decoders.Png.Helpers;

internal struct TextCompressedChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new TextCompressedChunk( chunk );
  }

  internal TextCompressedChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      // Text chunk data are stored in 4 segments, with null termination between
      // Keyword	            1-79 bytes (Latin1 character string)
      // Null separator         1 byte (null character)
      // Compression method     1 byte
      // Compressed text stream 0 or more bytes (Latin1 character string)

      var keyword = TextChunkHelper.GetTerminatedStringFromBuffer( _chunk.Data );

      // Length of title plus the null terminator
      var dataStart = keyword.ByteEndPosition + 1;

      // Compression method is 0 for zlib deflate, other values reserved for future
      var compressionMethod = _chunk.Data[ dataStart ];
      if( compressionMethod == 0 )
      {
        // Decompress the data after the decompression method byte
        var decompressor = new TextDecompressor( _chunk.Data.AsSpan( dataStart + 1 ) );

        var value = decompressor.GetString( StringEncoding.Ascii );
        Text = new PngText( keyword.Value, value );
      }
      else
      {
        throw new NotSupportedException( "Unsupported PNG Text Chunk compression method" );
      }
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  public PngText? Text { get; private set; } = null;

  private readonly ChunkBase _chunk;
}
