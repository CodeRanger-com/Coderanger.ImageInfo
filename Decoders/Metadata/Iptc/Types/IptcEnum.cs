// -----------------------------------------------------------------------
// <copyright file="IptcEnum.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

using System.Collections.Generic;
using System.ComponentModel;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class IptcEnum : IptcTypeBase, IMetadataTypedValue
{
  internal IptcEnum( ushort tagId, byte[] data )
    : base( MetadataType.Enum, tagId )
  {
    _data = data;
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
    var bufferValue = DataConversion.ConvertBuffer( value, StringEncoding.Utf8 );
    var numberValue = Convert.ToUInt16( bufferValue );

    var enumValue = new MetadataEnumValue( numberValue, GetEnumValue( numberValue ) );
    return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: enumValue );
  }

  private string GetEnumValue( ushort value )
  {
    var enumValue = MetadataTagEnumAttribute.GetTagEnumValue( ReflectionIptcTag, TagId, value );
    if( enumValue != null )
    {
      return enumValue;
    }

    return string.Empty;
  }

  private readonly byte[] _data;
}
