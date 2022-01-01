// -----------------------------------------------------------------------
// <copyright file="HeaderChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Png.Helpers;

internal struct HeaderChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new HeaderChunk( chunk );
  }

  internal HeaderChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      // Header chunk data contains:
      // 4 bytes = width
      // 4 bytes = height
      // 1 byte = bit depth
      // 1 byte = colour type
      // 1 byte = compression method
      // 1 byte = filter method
      // 1 byte = interlace method
      Width = DataConversion.UInt32FromBigEndianBuffer( _chunk.Data, 0 );
      Height = DataConversion.UInt32FromBigEndianBuffer( _chunk.Data, 4 );
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  public uint Width { get; private set; } = 0;
  public uint Height { get; private set; } = 0;

  private readonly ChunkBase _chunk;
}
