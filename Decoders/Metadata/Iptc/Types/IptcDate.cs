// -----------------------------------------------------------------------
// <copyright file="IptcDate.cs" company="CodeRanger.com">
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
public class IptcDate : IptcTypeBase, IMetadataTypedValue
{
  internal IptcDate( ushort tagId )
    : base( tagId, MetadataType.Date )
  {
  }

  /// <summary>
  /// Adds a new value to the metadata object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
  public void AddToExistingValue( ReadOnlySpan<byte> buffer )
  {
    var value = Create( buffer );
    if( value != null )
    {
      _metadata.Add( value );
    }
  }

  /// <summary>
  /// Sets the value of the object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    var value = Create( buffer );
    if( value != null )
    {
      _metadata.Add( value );
    }
  }

  /// <summary>
  /// Returns a string that represents the object
  /// </summary>
  /// <returns>String that represents the object</returns>
  public override string ToString()
  {
    var output = string.Empty;
    foreach( var data in _metadata )
    {
      var value = (DateTime)data.Value;
      if( output.Length > 0 )
      {
        output += " / ";
      }
      output += $"{Name} = {value:yyyy-MM-dd}";
    }

    return output;
  }

  private MetadataTagValue? Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.ConvertBuffer( buffer, StringEncoding.Ascii );
    if( DateTime.TryParseExact( bufferValue, DateFormatString, null, System.Globalization.DateTimeStyles.None, out var dt ) )
    {
      return new MetadataTagValue( Type: TagType,
                                   IsArray: false,
                                   TagId: TagId,
                                   TagName: Name,
                                   Value: dt );
    }

    return null;
  }

  private const string DateFormatString = "yyyyMMdd";
}
