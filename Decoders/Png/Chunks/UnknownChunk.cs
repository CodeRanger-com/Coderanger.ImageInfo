// -----------------------------------------------------------------------
// <copyright file="UnknownChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Chunks;

/// <summary>
/// Special chunk which allows the ability to skip
/// </summary>
internal struct UnknownChunk : IChunk
{
  internal UnknownChunk( ChunkBase chunk )
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
