// -----------------------------------------------------------------------
// <copyright file="TextDecompressor.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Helpers;

using System.IO.Compression;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal ref struct TextDecompressor
{
  internal TextDecompressor( ReadOnlySpan<byte> buffer )
  {
    _buffer = buffer;
  }

  internal string GetString( StringEncoding encoding )
  {
    // 0   = 1 byte   CMF
    // 1   = 1 byte   Flags
    // 2-6 = 4 bytes  CRC (optional)
    var flags = _buffer[ 1 ];

    // If we have a dictionary, move past the crc cos we dont particularly care about checking
    // else skip just the cmf and flags bytes
    bool fdict = ( flags & 32 ) != 0;
    var compressedBytes = _buffer[ ( fdict ? 6 : 2 ).. ];

    using var memoryStream = new MemoryStream( compressedBytes.ToArray() );
    using var decompressor = new DeflateStream( memoryStream, CompressionMode.Decompress );

    var uncompressedBytes = new List<byte>();

    // Load 8 bytes at a time, could be higher for speed but use more resources
    var tempBuffer = new byte[ 8 ];
    int bytesRead = decompressor.Read( tempBuffer, 0, tempBuffer.Length );
    while( bytesRead != 0 )
    {
      uncompressedBytes.AddRange( tempBuffer.AsSpan( 0, bytesRead ).ToArray() );
      bytesRead = decompressor.Read( tempBuffer, 0, tempBuffer.Length );
    }

    return DataConversion.ConvertBuffer( uncompressedBytes.ToArray(), encoding );
  }

  private readonly ReadOnlySpan<byte> _buffer;
}
