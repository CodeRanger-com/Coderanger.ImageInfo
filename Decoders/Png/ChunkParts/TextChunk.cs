// -----------------------------------------------------------------------
// <copyright file="TextChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata.Png;
using Coderanger.ImageInfo.Decoders.Png.Helpers;

internal struct TextChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new TextChunk( chunk );
  }

  internal TextChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      // Text chunk data are stored in 3 segments, with null termination between
      // Keyword	      1-79 bytes (Latin1 character string)
      // Null separator   1 byte (null character)
      // Text string      0 or more bytes (Latin1 character string)

      var keyword = TextChunkHelper.GetTerminatedStringFromBuffer( _chunk.Data );

      // Skip the length of keyword and the null terminator
      var dataStart = keyword.ByteEndPosition + 1;

      var valueBuffer = _chunk.Data.AsSpan( dataStart );
      var value = DataConversion.ConvertBuffer( valueBuffer, StringEncoding.Ascii );

      Text = new PngText( keyword.Value, value );
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  public PngText? Text { get; private set; } = null;

  private readonly ChunkBase _chunk;
}
