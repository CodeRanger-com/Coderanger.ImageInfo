// -----------------------------------------------------------------------
// <copyright file="IptcLong.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

using System.Collections.Generic;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class IptcLong : IptcTypeBase, IMetadataTypedValue
{
  internal IptcLong( ushort tagId, byte[] data )
    : base( MetadataType.Long, tagId )
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
  internal override IEnumerable<MetadataTagValue?> ExtractValues()
  {
    yield return Create( _data );
  }

  private MetadataTagValue? Create( byte[] value )
  {
    var bufferValue = DataConversion.Int64FromBigEndianBuffer( value );
    return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: bufferValue );
  }

  private readonly byte[] _data;
}

/// <summary>
/// 
/// </summary>
public class IptcULong : IptcTypeBase, IMetadataTypedValue
{
  internal IptcULong( ushort tagId, byte[] data )
    : base( MetadataType.ULong, tagId )
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
  internal override IEnumerable<MetadataTagValue?> ExtractValues()
  {
    yield return Create( _data );
  }

  private MetadataTagValue? Create( byte[] value )
  {
    var bufferValue = DataConversion.UInt64FromBigEndianBuffer( value );
    return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: bufferValue );
  }

  private readonly byte[] _data;
}
