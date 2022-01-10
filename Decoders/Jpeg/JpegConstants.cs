// -----------------------------------------------------------------------
// <copyright file="JpegConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Jpeg;

internal static class JpegConstants
{
  /// <summary>
  /// Header signature for a JPEG image format
  /// </summary>
  internal static readonly byte[] MagicNumber = new byte[] { 0xff, 0xd8 };

  internal static class Markers
  {
    /// <summary>
    /// Start of a marker block
    /// </summary>
    internal const byte Start = 0xFF;

    /// <summary>
    /// Start of Image
    /// </summary>
    internal const byte ImageStart = 0xD8;

    /// <summary>
    /// End of Image
    /// </summary>
    internal const byte ImageEnd = 0xD9;

    /// <summary>
    /// Restart
    /// </summary>
    internal const byte Restart = 0xDD;

    /// <summary>
    /// Frame Start (Baseline DCT)
    /// </summary>
    internal const byte BaselineStart = 0xC0;

    /// <summary>
    /// Frame Start (Extended Sequential DCT)
    /// </summary>
    internal const byte ExtendedSequentialStart = 0xC1;

    /// <summary>
    /// Frame Start (Progressive DCT)
    /// </summary>
    internal const byte ProgressiveStart = 0xC2;

    /// <summary>
    /// Application specific marker for jpeg jfif segment
    /// </summary>
    internal const byte App0 = 0xE0;

    /// <summary>
    /// Application specific marker for jpeg exif segment
    /// </summary>
    internal const byte App1 = 0xE1;

    /// <summary>
    /// Marker constants for JFIF segment of JPEG
    /// </summary>
    internal static class Jfif
    {
      /// <summary>
      /// Header signature for JFIF header marker
      /// </summary>
      internal static byte[] MagicBytes => new[]
      {
        (byte)'J', (byte)'F', (byte)'I', (byte)'F', (byte)'\0'
      };

      /// <summary>
      /// Length of JFIF Marker segment
      /// </summary>
      internal const int SegmentLength = 13;
    }
  }
}
