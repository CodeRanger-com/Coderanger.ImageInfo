// -----------------------------------------------------------------------
// <copyright file="TiffConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

internal static class TiffConstants
{
  internal const int HeaderLength = 8;

  internal static class ByteOrder
  {
    /// <summary>
    /// Marker to determine Little Endian byte order in EXIF data
    /// </summary>
    internal static byte[] LittleEndian => new[]
    {
      // 0x49, 0x49 = 73
      (byte)'I', (byte)'I'
    };
    internal const ushort LittleEndianValue = 0x_49_49;

    /// <summary>
    /// Marker to determine Big Endian byte order in EXIF data
    /// </summary>
    internal static byte[] BigEndian => new[]
    {
      // 0x4d, 0x4d = 77
      (byte)'M', (byte)'M'
    };
    internal const ushort BigEndianValue = 0x_4D_4D;
  }
}
