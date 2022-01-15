// -----------------------------------------------------------------------
// <copyright file="IptcEnum.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

using System;
using System.Collections.Generic;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class IptcEnum : IptcTypeBase, IMetadataTypedValue
{
  internal enum EnumType
  {
    Numeric,
    String,
  }

  internal IptcEnum( ushort tagId, byte[] data, EnumType enumType )
    : base( MetadataType.Enum, tagId )
  {
    _data = data;
    _enumType = enumType;
  }

  public string StringValue => ToString();

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  void IMetadataTypedValue.AddToExistingValue( byte[] value )
  {
    base.AddToExistingValue( Create( value ) );
  }

  /// <summary>
  /// Processes the data buffer for each type value
  /// </summary>
  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    yield return Create( _data );
  }

  private MetadataTagValue Create( byte[] value )
  {
    var enumValue = GetEnumFromValue( value );
    return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: enumValue );
  }

  private MetadataEnumValue GetEnumFromValue( byte[] value )
  {
    var bufferValue = DataConversion.ConvertBuffer( value.AsSpan(), StringEncoding.Utf8 );

    if( _enumType == EnumType.String )
    {
      var attributeInfo = MetadataTagEnumAttribute.GetTagEnumValue( ReflectionIptcTag, TagId, bufferValue );
      return new MetadataEnumValue( bufferValue, attributeInfo ?? string.Empty );
    }
    else
    {
      var numberValue = Convert.ToUInt16( bufferValue );
      var attributeInfo = MetadataTagEnumAttribute.GetTagEnumValue( ReflectionIptcTag, TagId, numberValue );
      return new MetadataEnumValue( bufferValue, attributeInfo ?? string.Empty );
    }
  }

  private readonly byte[] _data;
  private readonly EnumType _enumType;
}
