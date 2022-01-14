// -----------------------------------------------------------------------
// <copyright file="IptcByte.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class IptcByte : IptcTypeBase, IMetadataTypedValue
{
  internal IptcByte( ushort tagId, byte[] data )
    : base( MetadataType.Byte, tagId )
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
    return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: value );
  }

  private readonly byte[] _data;
}
