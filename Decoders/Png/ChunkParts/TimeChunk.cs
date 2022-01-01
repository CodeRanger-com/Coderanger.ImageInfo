// -----------------------------------------------------------------------
// <copyright file="TimeChunk.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.ChunkParts;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Png.Helpers;

internal struct TimeChunk : IChunk
{
  internal static IChunk Create( ChunkBase chunk )
  {
    return new TimeChunk( chunk );
  }

  internal TimeChunk( ChunkBase chunk )
  {
    _chunk = chunk;
  }

  public void LoadData( BinaryReader reader )
  {
    _chunk.LoadData( reader );
    if( _chunk.Data != null )
    {
      /*
      Year	  2 bytes (complete; for example, 1995, not 95)
      Month	  1 byte (1-12)
      Day	  1 byte (1-31)
      Hour	  1 byte (0-23)
      Minute  1 byte (0-59)
      Second  1 byte (0-60) (to allow for leap seconds)
      */
      int year = DataConversion.Int16FromBigEndianBuffer( _chunk.Data );
      int month = _chunk.Data[ 2 ];
      int day = _chunk.Data[ 3 ];
      int hour = _chunk.Data[ 4 ];
      int minute = _chunk.Data[ 5 ];
      int second = _chunk.Data[ 6 ];

      LastModified = new DateTime( year, month, day, hour, minute, second, DateTimeKind.Utc );
    }
  }

  public void SkipToEnd( BinaryReader reader )
  {
    _chunk.SkipToEnd( reader );
  }

  public DateTime LastModified { get; private set; } = default;

  private readonly ChunkBase _chunk;
}
