// -----------------------------------------------------------------------
// <copyright file="ChunkManager.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Chunks;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal class ChunkManager
{
  internal static bool TryGetChunkForType( byte[] chunkTypeBuffer, ChunkBase chunkBase, out IChunk? chunk )
  {
    var chunkType = DataConversion.UInt16FromBigEndianBuffer( chunkTypeBuffer );
    if( RegisteredChunks.TryGetValue( chunkType, out var chunkCreator ) && chunkCreator != null )
    {
      chunk = chunkCreator( chunkBase );
      return true;
    }

    chunk = null;
    return false;
  }

  private static readonly Dictionary<uint, Func<ChunkBase, IChunk>> RegisteredChunks = new()
  {
    { DataConversion.UInt16FromBigEndianBuffer( PngConstants.Chunks.IHeader ), HeaderChunk.Create },
    { DataConversion.UInt16FromBigEndianBuffer( PngConstants.Chunks.IPhys ), PhysicalDimensionsChunk.Create },
    { DataConversion.UInt16FromBigEndianBuffer( PngConstants.Chunks.ITime ), TimeChunk.Create },
    { DataConversion.UInt16FromBigEndianBuffer( PngConstants.Chunks.IExif ), ExifChunk.Create },
    { DataConversion.UInt16FromBigEndianBuffer( PngConstants.Chunks.IEnd ), EndChunk.Create },
  };
}
