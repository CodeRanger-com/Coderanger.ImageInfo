// -----------------------------------------------------------------------
// <copyright file="ExifConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

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
    // 0x45, 0x78, 0x69, 0x66 = 69, 120, 105, 102
    (byte)'E', (byte)'x', (byte)'i', (byte)'f', (byte)'\0', (byte)'\0'
  };

  /// <summary>
  /// Tag constants
  /// https://www.exiv2.org/tags.html
  /// </summary>
  internal static class Tags
  {
    /// <summary>
    /// Number of pixels in given resolution unit for X axis
    /// </summary>
    internal static int ImageWidth => 0x0100;

    /// <summary>
    /// Number of pixels in given resolution unit for X axis
    /// </summary>
    internal static int ImageHeight => 0x0101;

    /// <summary>
    /// Number of pixels in given resolution unit for X axis
    /// </summary>
    internal static int ResolutionX => 0x011A;

    /// <summary>
    /// Number of pixels in given resolution unit for Y axis
    /// </summary>
    internal static int ResolutionY => 0x011B;

    /// <summary>
    /// Type of unit resolution X and Y values are stored in
    /// </summary>
    /// <remarks>
    /// If the image resolution is unknown, 2 (inches) is designated
    /// </remarks>
    internal static int ResolutionUnit => 0x0128;
  }
}
