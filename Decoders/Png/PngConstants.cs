// -----------------------------------------------------------------------
// <copyright file="PngConstants.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png;

internal static class PngConstants
{
  /// <summary>
  /// Header signature for a PNG image format
  /// </summary>
  internal static readonly byte[] MagicNumber = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

  internal static class Chunks
  {
    /// <summary>
    /// Start of a header chunk which is the first chunk in the datastream
    /// </summary>
    internal static byte[] IHeader = new byte[] { (byte)'I', (byte)'H', (byte)'D', (byte)'R' };

    /// <summary>
    /// Start of the pHYs chunk which specifies the intended pixel size or aspect ratio for display of the image
    /// </summary>
    internal static byte[] IPhys = new byte[] { (byte)'p', (byte)'H', (byte)'Y', (byte)'s' };

    /// <summary>
    /// Start of an optional Exif block as defined in PNG Extensions 1.5
    /// </summary>
    internal static byte[] IExif = new byte[] { (byte)'e', (byte)'X', (byte)'I', (byte)'f' };

    /// <summary>
    /// Start of the time last modified chunk
    /// </summary>
    internal static byte[] ITime = new byte[] { (byte)'t', (byte)'I', (byte)'M', (byte)'E' };

    /// <summary>
    /// Start of an ASCII text chunk
    /// </summary>
    internal static byte[] IText = new byte[] { (byte)'t', (byte)'E', (byte)'X', (byte)'t' };

    /// <summary>
    /// Start of a compressed ASCII text chunk
    /// </summary>
    internal static byte[] ITextCompressed = new byte[] { (byte)'z', (byte)'T', (byte)'X', (byte)'t' };

    /// <summary>
    /// Start of a compressed International UTF8 text chunk
    /// </summary>
    internal static byte[] ITextInternational = new byte[] { (byte)'i', (byte)'T', (byte)'X', (byte)'t' };

    /// <summary>
    /// Start of the IEND file end chunk which marks the end of the PNG datastream
    /// </summary>
    internal static byte[] IEnd = new byte[] { (byte)'I', (byte)'E', (byte)'N', (byte)'D' };
  }
}
