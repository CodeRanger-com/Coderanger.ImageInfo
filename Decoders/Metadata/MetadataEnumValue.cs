// -----------------------------------------------------------------------
// <copyright file="MetadataEnumValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

/// <summary>
/// Simple record type holding an enum value type with information
/// </summary>
/// <param name="EnumValue"></param>
/// <param name="Information"></param>
public record MetadataEnumValue( ushort EnumValue, string Information )
{
  public override string ToString()
  {
    return $"{EnumValue} ({Information})";
  }
}
