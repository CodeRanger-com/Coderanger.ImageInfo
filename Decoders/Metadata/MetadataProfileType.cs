// -----------------------------------------------------------------------
// <copyright file="MetadataProfileType" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

/// <summary>
/// Types of Metadata Profiles available
/// </summary>
public enum MetadataProfileType
{
  /// <summary>
  /// Determines an EXIF profile
  /// </summary>
  Exif,

  /// <summary>
  /// Determines a GPS profile
  /// </summary>
  Gps,

  /// <summary>
  /// Determines an Interoperability profile
  /// </summary>
  Interoperability,

  /// <summary>
  /// Determines an IPTC profile
  /// </summary>
  Iptc,

  /// <summary>
  /// Determines an XMP profile
  /// </summary>
  Xmp,

  /// <summary>
  /// Determines a PNG Text profile
  /// </summary>
  PngText,
}
