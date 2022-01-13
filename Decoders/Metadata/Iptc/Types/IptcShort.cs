// -----------------------------------------------------------------------
// <copyright file="IptcShort.cs" company="CodeRanger.com">
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
public class IptcShort : IptcTypeBase, IMetadataTypedValue
{
  internal IptcShort( ushort tagId, byte[] data )
    : base( MetadataType.Short, tagId )
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
    var bufferValue = DataConversion.Int16FromBigEndianBuffer( value );
    return new MetadataTagValue( Type: ExifType, IsArray: false, TagId: TagId, TagName: Name, Value: bufferValue );
  }

  private readonly byte[] _data;
}

/// <summary>
/// 
/// </summary>
public class IptcUShort : IptcTypeBase, IMetadataTypedValue
{
  internal IptcUShort( ushort tagId, byte[] data )
    : base( MetadataType.UShort, tagId )
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
    var bufferValue = DataConversion.UInt16FromBigEndianBuffer( value );
    return new MetadataTagValue( Type: ExifType, IsArray: false, TagId: TagId, TagName: Name, Value: bufferValue );
  }

  private readonly byte[] _data;
}
