// -----------------------------------------------------------------------
// <copyright file="ShortEnum.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;

/// <summary>
/// Simple record type holding an enum value type with information
/// </summary>
/// <param name="EnumValue"></param>
/// <param name="Information"></param>
public record ShortEnum( ushort EnumValue, string Information )
{
  public override string ToString()
  {
    return $"{EnumValue} ({Information})";
  }
}
