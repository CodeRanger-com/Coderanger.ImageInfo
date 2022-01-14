// -----------------------------------------------------------------------
// <copyright file="IptcDate.cs" company="CodeRanger.com">
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
public class IptcDate : IptcTypeBase, IMetadataTypedValue
{
  internal IptcDate( ushort tagId, byte[] data )
    : base( MetadataType.Date, tagId )
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
    var bufferValue = DataConversion.ConvertBuffer( value, StringEncoding.Ascii );
    if( DateOnly.TryParseExact( bufferValue, DateFormatString, null, System.Globalization.DateTimeStyles.None, out var dt ) )
    {
      return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: dt );
    }

    return null;
  }

  private readonly byte[] _data;
  private const string DateFormatString = "yyyyMMdd";
}
