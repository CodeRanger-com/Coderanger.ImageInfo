// -----------------------------------------------------------------------
// <copyright file="GifConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Gif;

internal static class GifConstants
{
  internal static ReadOnlySpan<byte> MagicNumber => new[] { (byte)'G', (byte)'I', (byte)'F' };
  internal static ReadOnlySpan<byte> MagicNumberVersion87 => new[] { (byte)'8', (byte)'7', (byte)'a' };
  internal static ReadOnlySpan<byte> MagicNumberVersion89 => new[] { (byte)'8', (byte)'9', (byte)'a' };

  internal const string MimeType = "image/gif";

  internal static class ExtensionBlock
  {
    internal const byte Identifier = (byte)'!';
    internal const byte Label = 0xFF;
    internal const byte BlockSize = 0x0B;
    internal const byte End = 0x01;

    internal static class XmpApplication
    {
      internal static ReadOnlySpan<byte> Identifier => new[]
      {
        (byte)'X', (byte)'M', (byte)'P', (byte)' ', (byte)'D', (byte)'a', (byte)'t', (byte)'a',
        (byte)'X', (byte)'M', (byte)'P'
      };
    }

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
}
