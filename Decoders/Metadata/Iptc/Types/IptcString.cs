// -----------------------------------------------------------------------
// <copyright file="IptcString.cs" company="CodeRanger.com">
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
public class IptcString : IptcTypeBase, IMetadataTypedValue
{
  internal IptcString( ushort tagId )
    : base( tagId, MetadataType.String )
  {
  }

  /// <summary>
  /// Adds a new value to the metadata object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
  public void AddToExistingValue( ReadOnlySpan<byte> buffer )
  {
    var value = Create( buffer );
    if( value is not null )
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
    if( value is not null )
    {
      _metadata.Add( value );
    }
  }

  private MetadataTagValue? Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.ConvertBuffer( buffer, StringEncoding.Utf8 );
    if( bufferValue.Length > 0 )
    {
      return new MetadataTagValue( Type: TagType,
                                   IsArray: false,
                                   TagId: TagId,
                                   TagName: Name,
                                   Value: bufferValue );
    }

    return null;
  }
}
