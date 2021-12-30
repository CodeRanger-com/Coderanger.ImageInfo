// -----------------------------------------------------------------------
// <copyright file="ChunkBase.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// Provides a simple record storing info on a PNG chunk
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Chunks;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// Provides a simple record storing info on a PNG chunk
/// </summary>
internal record ChunkBase( long DataLength, byte[] ChunkType, long DataStartPosition )
{
  /// <summary>
  /// Lazy load the data segment when required, with optional max limit on bytes
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="maxBytesToRead"></param>
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
  /// Helper method to jump to the end of this chunk
  /// </summary>
  /// <param name="reader"></param>
  internal void SkipToEnd( BinaryReader reader )
  {
    // Length does not include the 4 Length bytes or the 4 CRC bytes at the end so seek
    // accordingly
    reader.BaseStream.Seek( DataStartPosition + DataLength + 4 + 4, SeekOrigin.Begin );
  }

  /// <summary>
  /// Data portion of the chunk; only loaded if needed
  /// </summary>
  internal byte[]? Data { get; private set; } = null;

  const string ErrorMessage = "Invalid PNG chunk";
}
