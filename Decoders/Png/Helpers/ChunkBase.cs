// -----------------------------------------------------------------------
// <copyright file="ChunkBase.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// Provides a simple record storing info on a PNG chunk and providing some
// base functionality
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Helpers;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// Provides a simple record storing info on a PNG chunk and providing some
/// base functionality
/// </summary>
internal record ChunkBase( long DataLength, byte[] ChunkType, long DataStartPosition )
{
  /// <summary>
  /// Lazy load the data segment when required
  /// </summary>
  /// <param name="reader"></param>
  internal void LoadData( BinaryReader reader )
  {
    if( DataLength == 0 )
    {
      return;
    }

    Data = reader.ReadBytes( (int)DataLength );
    if( Data == null )
    {
      throw ExceptionHelper.Throw( reader, ErrorMessage );
    }

    if( !IsValid( reader ) )
    {
      throw ExceptionHelper.Throw( reader, ErrorMessage );
    }
  }

  internal bool IsValid( BinaryReader reader )
  {
    // Read written out CRC, calculate and compare
    var crcCheck = reader.ReadBigEndianInt32();

    var crc = Crc32Checksum.Calculate( ChunkType, 0, ChunkType.Length );
    if( Data != null )
    {
      crc = Crc32Checksum.Calculate( crc, Data, 0, (int)DataLength );
    }

    return crc == crcCheck;
  }

  /// <summary>
  /// Helper method to jump forward to the end of this chunk
  /// </summary>
  /// <param name="reader"></param>
  internal void SkipToEnd( BinaryReader reader )
  {
    // DataLength does not include the 4 Length bytes or the 4 CRC bytes at the end
    // so seek accordingly
    var skipForward = DataStartPosition + DataLength + 4 + 4 - reader.Position();
    if( skipForward > 0 )
    {
      reader.Skip( skipForward );
    }
  }

  /// <summary>
  /// Data portion of the chunk; only loaded if needed
  /// </summary>
  internal byte[]? Data { get; private set; } = null;

  const string ErrorMessage = "Invalid PNG chunk";
}
