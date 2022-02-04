// -----------------------------------------------------------------------
// <copyright file="ChunkManager.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Helpers;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Png.ChunkParts;

internal class ChunkManager
{
  internal static bool TryGetChunkForType( byte[] chunkTypeBuffer, ChunkBase chunkBase, out IChunk? chunk )
  {
    var chunkType = DataConversion.UInt32FromBigEndianBuffer( chunkTypeBuffer );
    if( RegisteredChunks.TryGetValue( chunkType, out var chunkCreator ) && chunkCreator is not null )
    {
      chunk = chunkCreator( chunkBase );
      return true;
    }

    chunk = null;
    return false;
  }

  private static readonly Dictionary<uint, Func<ChunkBase, IChunk>> RegisteredChunks = new()
  {
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.IHeader ), HeaderChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.IPhys ), PhysicalDimensionsChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.ITime ), TimeChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.IText ), TextChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.ITextCompressed ), TextCompressedChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.ITextInternational ), TextInternationalChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.IExif ), ExifChunk.Create },
    { DataConversion.UInt32FromBigEndianBuffer( PngConstants.Chunks.IEnd ), EndChunk.Create },
  };
}
