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

  private MetadataTagValue? Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.ConvertBuffer( buffer, StringEncoding.Ascii );

    if( bufferValue.Length < 6 )
    {
      return null;
    }

    var hours = bufferValue[ ..2 ];
    var mins = bufferValue.Substring( 2, 2 );
    var secs = bufferValue.Substring( 4, 2 );

    var time = new TimeSpan( int.Parse( hours ), int.Parse( mins ), int.Parse( secs ) );
    return new MetadataTagValue( Type: TagType,
                                  IsArray: false,
                                  TagId: TagId,
                                  TagName: Name,
                                  Value: time );
  }
}
