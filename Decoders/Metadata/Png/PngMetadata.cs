// -----------------------------------------------------------------------
// <copyright file="PngMetadata.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Png;

using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class PngMetadata : IMetadataTypedValue
{
  // Hack: Just used for reflection in the custom description attribute
  private static readonly PngKeyword ReflectionPngKeyword = new();

  internal PngMetadata( PngText data )
  {
    _data = data;

    _details = PngKeywordDetailsAttribute.GetTagDetails( ReflectionPngKeyword, _data.Keyword );

    Name = _data.Keyword;
    Description = _details?.Description ?? string.Empty;
    TagType = MetadataType.PngText;
  }

  /// <summary>
  /// Simple representation of the data for debugging
  /// </summary>
  public string StringValue => _data.ToString();

  /// <summary>
  /// Determines if the data contains an array
  /// </summary>
  /// <remarks>
  /// PNG Metadata does not support arrays
  /// </remarks>
  public bool IsArray => false;

  /// <summary>
  /// Id value of the tag
  /// </summary>
  public ushort TagId => _details?.Id ?? PngKeyword.Custom;

  /// <summary>
  /// Type of data being held
  /// </summary>
  public MetadataType TagType { get; init; }

  /// <summary>
  /// Friendly name of metadata item
  /// </summary>
  internal string Name { get; init; }

  /// <summary>
  /// Description with explanation of metadata keyword data
  /// </summary>
  internal string Description { get; init; } = string.Empty;

  /// <summary>
  /// Sets the value of the object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    // Not needed for PNG metadata
    throw new NotImplementedException();
  }

  /// <summary>
  /// Returns true if their is a base tag value for this tag
  /// </summary>
  public bool HasValue => _data.TextValue.Length > 0;

  /// <summary>
  /// Returns the base tag's name
  /// </summary>
  public string TagTypeName => TagType.ToString();

  /// <summary>
  /// Retrieves the objects value type
  /// </summary>
  /// <param name="value">Pass a MetadataTagValue to be set with this objects value</param>
  /// <returns>Returns true if the value has been set</returns>
  public bool TryGetValue( out MetadataTagValue? value )
  {
    value = new MetadataTagValue( Type: TagType, IsArray: false, TagId, _data.Keyword, Value: _data );
    return true;
  }

  /// <summary>
  /// Retrieves the objects value type array
  /// </summary>
  /// <param name="value">Pass a List of MetadataTagValue to be set with this objects array value</param>
  /// <returns>Returns true if the value has been set</returns>
  public bool TryGetValueArray( out List<MetadataTagValue>? value )
  {
    value = null;
    return false;
  }

  private readonly PngText _data;
  private readonly PngKeywordDetails? _details;
}
