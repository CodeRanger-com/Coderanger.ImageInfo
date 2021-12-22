// -----------------------------------------------------------------------
// <copyright file="ImageDetails.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders.Exif;
using Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// Provides read-only information on the decoded image
/// </summary>
/// <param name="Width">Pixel width</param>
/// <param name="Height">Pixel height</param>
/// <param name="HorizontalResolution">Resolution in Dots per Inch</param>
/// <param name="VerticalResolution">Resolution in Dots per Inch</param>
/// <param name="Tags">Dictionary of exif tags</param>
/// <param name="MimeType">Mime type as 'image/type'</param>
public record ImageDetails( long Width, long Height, int HorizontalResolution, int VerticalResolution, string MimeType, Dictionary<ushort, ExifValue> Tags );
