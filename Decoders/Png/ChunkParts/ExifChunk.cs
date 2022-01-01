// -----------------------------------------------------------------------
// <copyright file="ExifChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.Png.Helpers;

internal struct ExifChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new ExifChunk( chunk );
  }

  internal ExifChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      ///
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  private readonly ChunkBase _chunk;
}
