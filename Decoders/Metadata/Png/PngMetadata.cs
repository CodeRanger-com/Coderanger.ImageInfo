// -----------------------------------------------------------------------
// <copyright file="PngMetadata.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Png;

using System.Collections.Generic;

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

  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    // Not needed for PNG metadata
    throw new NotImplementedException();
  }

  public bool HasValue => _data.TextValue.Length > 0;

  public string TagTypeName => TagType.ToString();

  public bool TryGetValue( out MetadataTagValue? value )
  {
    value = new MetadataTagValue( Type: TagType, IsArray: false, TagId, _data.Keyword, Value: _data );
    return true;
  }

  public bool TryGetValueArray( out List<MetadataTagValue>? value )
  {
    value = null;
    return false;
  }

  private readonly PngText _data;
  private readonly PngKeywordDetails? _details;
}
