// -----------------------------------------------------------------------
// <copyright file="IptcConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc;

internal static class IptcConstants
{
  internal static class AdobePhotoshop
  {
    /// <summary>
    /// Header signature for Adobe Photoshop header marker
    /// </summary>
    internal static byte[] MagicBytes => new[]
    {
      (byte)'P', (byte)'h', (byte)'o', (byte)'t', (byte)'o', (byte)'s', (byte)'h', (byte)'o', (byte)'p', (byte)' ', (byte)'3', (byte)'.', (byte)'0', (byte)'\0'
    };

    /// <summary>
    /// Adobe 8BIM marker for an image resource block
    /// </summary>
    internal static ReadOnlySpan<byte> ImageResourceMarker => new[]
    {
      (byte)'8', (byte)'B', (byte)'I', (byte)'M'
    };
    internal const uint ImageResourceMarkerValue = 0x_38_42_49_4D;


    /// <summary>
    /// The IPTC Image resource ID
    /// </summary>
    internal static ReadOnlySpan<byte> IptcMarker => new[]
    {
      (byte)4, (byte)4
    };
    internal const ushort IptcMarkerValue = 0x_04_04;
  }
}
