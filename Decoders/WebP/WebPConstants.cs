// -----------------------------------------------------------------------
// <copyright file="WebPConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.WebP;

internal static class WebPConstants
{
  // LE so byte values are in reverse of byte order
  internal static class Riff
  {
    internal static readonly byte[] MagicNumber = new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' };
    internal static readonly uint MagicNumberValue = 0x46_46_49_52;
  }

  internal static class WebP
  {
    internal static readonly byte[] MagicNumber = new byte[] { (byte)'W', (byte)'E', (byte)'B', (byte)'P' };
    internal static readonly uint MagicNumberValue = 0x50_42_45_57;
  }

  internal static class Chunks
  {
    internal static readonly byte[] VP8 = new byte[] { (byte)'V', (byte)'P', (byte)'8', (byte)' ' };
    internal static readonly uint VP8Value = 0x20_38_50_56;
    internal static readonly byte[] VP8Signature = { 0x9D, 0x01, 0x2A };

    internal static readonly byte[] VP8L = new byte[] { (byte)'V', (byte)'P', (byte)'8', (byte)'L' };
    internal static readonly uint VP8LValue = 0x4C_38_50_56;
    internal static readonly byte VP8LSignature = 0x2F;

    internal static readonly byte[] VP8X = new byte[] { (byte)'V', (byte)'P', (byte)'8', (byte)'X' };
    internal static readonly uint VP8XValue = 0x58_38_50_56;

    internal static readonly byte[] Exif = new byte[] { (byte)'E', (byte)'X', (byte)'I', (byte)'F' };
    internal static readonly uint ExifValue = 0x46_49_58_45;

    internal static readonly byte[] Xmp = new byte[] { (byte)'X', (byte)'M', (byte)'P', (byte)' ' };
    internal static readonly uint XmpValue = 0x20_50_4D_58;
  }

  internal const string MimeType = "image/webp";
}
