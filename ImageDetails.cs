// -----------------------------------------------------------------------
// <copyright file="ImageDetails.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders.Metadata;

/// <summary>
/// Provides read-only information on the decoded image
/// </summary>
/// <param name="Width">Pixel width</param>
/// <param name="Height">Pixel height</param>
/// <param name="HorizontalResolution">Resolution in Dots per Inch</param>
/// <param name="VerticalResolution">Resolution in Dots per Inch</param>
/// <param name="Metadata">Dictionary of profiles to list of exif, iptc, png, xmp, gps, photo or private keyword tags; or null if none have been found</param>
/// <param name="MimeType">Mime type as 'image/type'</param>
internal record ImageDetails( long Width,
                              long Height,
                              int HorizontalResolution,
                              int VerticalResolution,
                              string MimeType,
                              Dictionary<MetadataProfileType, List<IMetadataTypedValue>>? Metadata );
