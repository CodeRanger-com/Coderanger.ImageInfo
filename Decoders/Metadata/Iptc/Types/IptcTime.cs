// -----------------------------------------------------------------------
// <copyright file="IptcTime.cs" company="CodeRanger.com">
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
public class IptcTime : IptcTypeBase, IMetadataTypedValue
{
  internal IptcTime( ushort tagId, byte[] data )
    : base( MetadataType.Time, tagId )
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
    bufferValue = bufferValue.Replace( "+", string.Empty );
    if( TimeOnly.TryParseExact( bufferValue, TimeFormatStringWithZone, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out var timeWithZone ) )
    {
      return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: timeWithZone );
    }
    else if( TimeOnly.TryParseExact( bufferValue, TimeFormatString, null, System.Globalization.DateTimeStyles.None, out var time ) )
    {
      return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: time );
    }

    return null;
  }

  private readonly byte[] _data;
  private const string TimeFormatStringWithZone = "HHmmsszzz";
  private const string TimeFormatString = "HHmmss";
}
