// -----------------------------------------------------------------------
// <copyright file="BmpConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Bmp;

internal static class BmpConstants
{
  internal static readonly byte[] MagicNumber = new byte[] { (byte)'B', (byte)'M' };
  internal static readonly int MagicNumberValue = 0x_00_00_42_4D;

  internal static class HeaderSize
  {
    internal const int OS2Version2 = 12;
    internal const int WinVersion3 = 40;
    internal const int AdobeVersion3a = 52;
    internal const int AdobeVersion3b = 56;
    internal const int WinVersion4 = 108;
    internal const int Version5 = 124;
  }
}
