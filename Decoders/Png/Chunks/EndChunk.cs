// -----------------------------------------------------------------------
// <copyright file="EndChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Chunks;

internal struct EndChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new EndChunk( chunk );
  }

  internal EndChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  private readonly ChunkBase _chunk;
}
