// -----------------------------------------------------------------------
// <copyright file="IptcString.cs" company="CodeRanger.com">
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
public class IptcString : IptcTypeBase, IMetadataTypedValue
{
  internal IptcString( ushort tagId, byte[] data )
    : base( MetadataType.String, tagId )
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
    return new MetadataTagValue( Type: ExifType, IsArray: false, TagId: TagId, TagName: Name, Value: bufferValue );
  }

  private readonly byte[] _data;
}
