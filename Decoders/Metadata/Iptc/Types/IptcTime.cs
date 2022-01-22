// -----------------------------------------------------------------------
// <copyright file="IptcTime.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

using System;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class IptcTime : IptcTypeBase, IMetadataTypedValue
{
  internal IptcTime( ushort tagId )
    : base( tagId, MetadataType.Time )
  {
  }

  public void AddToExistingValue( ReadOnlySpan<byte> buffer )
  {
    var value = Create( buffer );
    if( value != null )
    {
      _metadata.Add( value );
    }
  }

  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    var value = Create( buffer );
    if( value != null )
    {
      _metadata.Add( value );
    }
  }

  private MetadataTagValue? Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.ConvertBuffer( buffer, StringEncoding.Ascii );
    bufferValue = bufferValue.Replace( "+", string.Empty );

    if( TimeOnly.TryParseExact( bufferValue, TimeFormatStringWithZone, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out var timeWithZone ) )
    {
      return new MetadataTagValue( Type: TagType,
                                   IsArray: false,
                                   TagId: TagId,
                                   TagName: Name,
                                   Value: timeWithZone );
    }
    else if( TimeOnly.TryParseExact( bufferValue, TimeFormatString, null, System.Globalization.DateTimeStyles.None, out var time ) )
    {
      return new MetadataTagValue( Type: TagType,
                                   IsArray: false,
                                   TagId: TagId,
                                   TagName: Name,
                                   Value: time );
    }

    return null;
  }

  private const string TimeFormatStringWithZone = "HHmmsszzz";
  private const string TimeFormatString = "HHmmss";
}
