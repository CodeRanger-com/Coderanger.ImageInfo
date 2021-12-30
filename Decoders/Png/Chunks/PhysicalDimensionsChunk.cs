// -----------------------------------------------------------------------
// <copyright file="PhysicalDimensionsChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Chunks;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal struct PhysicalDimensionsChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new PhysicalDimensionsChunk( chunk );
  }

  internal PhysicalDimensionsChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      var xPixels = DataConversion.UInt32FromBigEndianBuffer( _chunk.Data, 0 );
      var yPixels = DataConversion.UInt32FromBigEndianBuffer( _chunk.Data, 4 );

      var unit = _chunk.Data[ 8 ];
      switch( unit )
      {
        case 0:
          // unknown
          break;

        case 1:
          XResolutionDpi = UnitConvertor.ToDpi( DensityUnit.PixelsPerMeter, xPixels );
          YResolutionDpi = UnitConvertor.ToDpi( DensityUnit.PixelsPerMeter, yPixels );
          break;
      }
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  public int XResolutionDpi { get; private set; } = 0;
  public int YResolutionDpi { get; private set; } = 0;

  private readonly ChunkBase _chunk;
}
