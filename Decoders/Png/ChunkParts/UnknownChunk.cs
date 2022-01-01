// -----------------------------------------------------------------------
// <copyright file="UnknownChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.Png.Helpers;

/// <summary>
/// Special chunk which stores the base chunk and so allows the ability
/// to skip without any additional logic
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
