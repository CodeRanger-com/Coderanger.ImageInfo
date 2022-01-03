// -----------------------------------------------------------------------
// <copyright file="MetadataTagValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

/// <summary>
/// Simple record to describe the type and value of a metadata tag
/// </summary>
public record MetadataTagValue( MetadataType Type, bool IsArray, ushort TagId, string TagName, object Value )
{
  public override string ToString()
  {
    var value = Value?.ToString() ?? string.Empty;

    if( IsArray )
    {
      return $"{string.Join( " / ", value )}";
    }

    return value;
  }
}
