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
  }

  /// <summary>
  /// PNG Metadata does not support arrays
  /// </summary>
  public bool IsArray => false;

  public ushort TagId => _details?.Id ?? PngKeyword.Custom;

  public string StringValue => _data.ToString();

  /// <summary>
  /// Friendly name of metadata item
  /// </summary>
  public string Name { get; init; }

  /// <summary>
  /// Description with explanation of metadata keyword data
  /// </summary>
  public string Description { get; init; } = string.Empty;

  // Not needed for PNG metadata
  void IMetadataTypedValue.SetValue()
  {
    throw new NotImplementedException();
  }

  public bool TryGetValue( out MetadataTagValue? value )
  {
    value = new MetadataTagValue( MetadataType.PngText, false, TagId, _data.Keyword, _data );
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
