// -----------------------------------------------------------------------
// <copyright file="IptcShort.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class IptcShort : IptcTypeBase, IMetadataTypedValue
{
  internal IptcShort( ushort tagId )
    : base( tagId, MetadataType.Short )
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

  private MetadataTagValue Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.Int16FromBigEndianBuffer( buffer );
    return new MetadataTagValue( Type: TagType,
                                 IsArray: false,
                                 TagId: TagId,
                                 TagName: Name,
                                 Value: bufferValue );
  }
}

/// <summary>
/// 
/// </summary>
public class IptcUShort : IptcTypeBase, IMetadataTypedValue
{
  internal IptcUShort( ushort tagId )
    : base( tagId, MetadataType.UShort )
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

  private MetadataTagValue Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.UInt16FromBigEndianBuffer( buffer );
    return new MetadataTagValue( Type: TagType,
                                 IsArray: false,
                                 TagId: TagId,
                                 TagName: Name,
                                 Value: bufferValue );
  }
}
