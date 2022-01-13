// -----------------------------------------------------------------------
// <copyright file="ExifConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif;

internal static class ExifConstants
{
  /// <summary>
  /// Header signature for EXIF header marker
  /// </summary>
  internal static byte[] MagicBytes => new[]
  {
    // 0x45, 0x78, 0x69, 0x66
    (byte)'E', (byte)'x', (byte)'i', (byte)'f', (byte)'\0', (byte)'\0'
  };
  internal const uint MagicBytesVersion = 0x_45_78_69_66;

  /// <summary>
  /// IFD Offsets
  /// </summary>
  internal enum IfdOffset
  {
    /// <summary>
    /// Exif IFD
    /// </summary>
    Exif = 0x8769,

    /// <summary>
    /// GPS IFD
    /// </summary>
    Gps = 0x8825,

    /// <summary>
    /// IPTC Record IFD
    /// </summary>
    Iptc = 0x83bb,

    /// <summary>
    /// XMP Packet IFD
    /// </summary>
    Xmp = 0x02bc,

    /// <summary>
    /// Interoperability IFD
    /// </summary>
    Inter = 0xA005,
  }

  internal static class EncodingSignature
  {
    internal static byte[] Ascii => new[]
    {
      (byte)'A', (byte)'S', (byte)'C', (byte)'I', (byte)'I', (byte)'\0', (byte)'\0', (byte)'\0'
    };
    internal const ulong AsciiValue = 0x_41_53_43_49_49_00_00_00;

    internal static byte[] Unicode => new[]
    {
      (byte)'U', (byte)'N', (byte)'I', (byte)'C', (byte)'O', (byte)'D', (byte)'E', (byte)'\0'
    };
    internal const ulong UnicodeValue = 0x_55_4E_49_43_4F_44_45_00;

    internal static byte[] Jis => new[]
    {
      (byte)'J', (byte)'I', (byte)'S', (byte)'\0', (byte)'\0', (byte)'\0', (byte)'\0', (byte)'\0'
    };
    internal const ulong JisValue = 0x_4A_49_53_00_00_00_00_00;
  }

  internal const ushort InteroperabilityOffsetFix = 0x2000;

  internal const int ExifDirectorySize = 12;
}
