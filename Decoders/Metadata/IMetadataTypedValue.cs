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
  /// <summary>
  /// The tags ID value
  /// </summary>
  /// <remarks>
  /// Generally defined by the organisation body responsible
  /// </remarks>
  public ushort TagId { get; }

  /// <summary>
  /// Determines the tags type, e.g. a string, long etc
  /// </summary>
  public MetadataType TagType { get; }

  /// <summary>
  /// Determines if the tags value is an array of types
  /// </summary>
  public bool IsArray { get; }

  /// <summary>
  /// Determines if this tag has a value
  /// </summary>
  public bool HasValue { get; }

  /// <summary>
  /// Retrieves the objects value type
  /// </summary>
  /// <param name="value">Pass a MetadataTagValue to be set with this objects value</param>
  /// <returns>Returns true if the value has been set</returns>
  public bool TryGetValue( out MetadataTagValue? value );

  /// <summary>
  /// Retrieves the objects value type array
  /// </summary>
  /// <param name="value">Pass a List of MetadataTagValue to be set with this objects array value</param>
  /// <returns>Returns true if the value has been set</returns>
  public bool TryGetValueArray( out List<MetadataTagValue>? value );

  internal string StringValue { get; }
  internal string TagTypeName => TagType.ToString();

  internal void SetValue( ReadOnlySpan<byte> buffer );
  internal void AddToExistingValue( ReadOnlySpan<byte> buffer )
  {
    // default implementation
  }
}
