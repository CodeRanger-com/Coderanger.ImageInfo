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

  /// <summary>
  /// Adds a new value to the metadata object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
  public void AddToExistingValue( ReadOnlySpan<byte> buffer )
  {
    _metadata.Add( Create( buffer ) );
  }

  /// <summary>
  /// Sets the value of the object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
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
