// -----------------------------------------------------------------------
// <copyright file="IMetadataTypedValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

/// <summary>
/// Interface for all data 'types' of metadata to inherit from which will obtain
/// a tag value of the appropriate type
/// </summary>
public interface IMetadataTypedValue
{
  internal void SetValue();
  internal void AddToExistingValue( byte[] value )
  {
    // default implementation
  }
  public bool TryGetValue( out MetadataTagValue? value );
  public bool TryGetValueArray( out List<MetadataTagValue>? value );
  public bool IsArray { get; }
  public ushort TagId { get; }
  public string StringValue { get; }
}
