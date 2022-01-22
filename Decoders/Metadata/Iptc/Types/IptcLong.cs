// -----------------------------------------------------------------------
// <copyright file="IptcLong.cs" company="CodeRanger.com">
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
public class IptcLong : IptcTypeBase, IMetadataTypedValue
{
  internal IptcLong( ushort tagId )
    : base( tagId, MetadataType.Long )
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

  private MetadataTagValue Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.Int64FromBigEndianBuffer( buffer );
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
public class IptcULong : IptcTypeBase, IMetadataTypedValue
{
  internal IptcULong( ushort tagId )
    : base( tagId, MetadataType.ULong )
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

  private MetadataTagValue Create( ReadOnlySpan<byte> buffer )
  {
    var bufferValue = DataConversion.UInt64FromBigEndianBuffer( buffer );
    return new MetadataTagValue( Type: TagType,
                                 IsArray: false,
                                 TagId: TagId,
                                 TagName: Name,
                                 Value: bufferValue );
  }
}
