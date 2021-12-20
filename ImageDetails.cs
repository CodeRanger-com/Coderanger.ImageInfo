// -----------------------------------------------------------------------
// <copyright file="ImageDetails.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

/// <summary>
/// Provides read-only information on the decoded image
/// </summary>
/// <param name="Width">Pixel width</param>
/// <param name="Height">Pixel height</param>
/// <param name="HorizontalResolution">Resolution in Dots per Inch</param>
/// <param name="VerticalResolution">Resolution in Dots per Inch</param>
/// <param name="MimeType">Mime type as 'image/type'</param>
public record ImageDetails( int Width, int Height, int HorizontalResolution, int VerticalResolution, string MimeType );
