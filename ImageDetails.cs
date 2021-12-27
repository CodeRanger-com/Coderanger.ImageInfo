// -----------------------------------------------------------------------
// <copyright file="ImageDetails.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// Provides read-only information on the decoded image
/// </summary>
/// <param name="Width">Pixel width</param>
/// <param name="Height">Pixel height</param>
/// <param name="HorizontalResolution">Resolution in Dots per Inch</param>
/// <param name="VerticalResolution">Resolution in Dots per Inch</param>
/// <param name="ExifTags">Dictionary of exif and photo tags</param>
/// <param name="GpsTags">Dictionary of exif gps tags</param>
/// <param name="MimeType">Mime type as 'image/type'</param>
internal record ImageDetails( long Width, long Height, int HorizontalResolution, int VerticalResolution, string MimeType, Dictionary<ushort, IExifValue>? ExifTags, Dictionary<ushort, IExifValue>? GpsTags );
