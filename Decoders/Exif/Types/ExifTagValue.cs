// -----------------------------------------------------------------------
// <copyright file="ExifTagValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// Simple record to describe the type and value of an Exif tag
/// </summary>
public record ExifTagValue( ExifType Type, string TagName, object? Value );
