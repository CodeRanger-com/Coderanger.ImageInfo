// -----------------------------------------------------------------------
// <copyright file="CreateChunkFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Helpers;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Png.ChunkParts;

internal static class ChunkFactory
{
  /// <summary>
  /// Create a chunk object with basic info from chunk details acquired from reader
  /// </summary>
  /// <param name="reader">BinaryReader wrapping a valid image stream</param>
  /// <returns>Valid known chunk or an UnknownChunk</returns>
  internal static IChunk ReadChunk( BinaryReader reader )
  {
    var dataLength = reader.ReadBigEndianUInt32();

    // Chunk length starts from the type and does not include the length
    // or SRC so store the position for skipping to the end later
    var dataStart = reader.Position();

    var chunkType = reader.ReadBytes( 4 );

    // Only bother validating the CRC if the buffer needs to be retrieved;
    // this allows a more optimal experience in skipping chunks that arent
    // needed

    var chunkBase = new ChunkBase( dataLength, chunkType, dataStart );
    if( ChunkManager.TryGetChunkForType( chunkType, chunkBase, out var validChunk ) && validChunk is not null )
    {
      return validChunk;
    }

    return new UnknownChunk( chunkBase );
  }
}
