// -----------------------------------------------------------------------
// <copyright file="IptcByte.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

/// <summary>
/// 
/// </summary>
public class IptcByte : IptcTypeBase, IMetadataTypedValue
{
  internal IptcByte( ushort tagId )
    : base( tagId, MetadataType.Byte )
  {
  }

  public void AddToExistingValue( ReadOnlySpan<byte> buffer )
  {
    _metadata.Add( Create( buffer ) );
  }

  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    _metadata.Add( Create( buffer ) );
  }

  private MetadataTagValue Create( ReadOnlySpan<byte> buffer )
  {
    return new MetadataTagValue( Type: TagType,
                                 IsArray: false,
                                 TagId: TagId,
                                 TagName: Name,
                                 Value: buffer.ToArray() );
  }
}
