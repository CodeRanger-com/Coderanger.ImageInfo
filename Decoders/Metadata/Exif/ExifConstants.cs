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
  /// Application specific marker for jpeg exif segment
  /// </summary>
  internal const byte App = 0xE1;

  /// <summary>
  /// Header signature for EXIF header marker
  /// </summary>
  internal static byte[] MagicBytes => new[]
  {
    // 0x45, 0x78, 0x69, 0x66
    (byte)'E', (byte)'x', (byte)'i', (byte)'f', (byte)'\0', (byte)'\0'
  };

  /// <summary>
  /// IFD Offsets
  /// </summary>
  internal enum IfdOffset
  {
    /// <summary>
    /// Exif IFD
    /// </summary>
    Exif = 0x8769, // 34665

    /// <summary>
    /// GPS IFD
    /// </summary>
    Gps = 0x8825, // 34853

    /// <summary>
    /// Interoperability IFD
    /// </summary>
    Inter = 0xA005, // 40965
  }

  internal const ushort InteroperabilityOffsetFix = 0x2000;
}
