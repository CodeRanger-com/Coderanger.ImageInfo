// -----------------------------------------------------------------------
// <copyright file="XmpData.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Xmp;

using System;
using System.Collections.Generic;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class XmpData : IMetadataTypedValue
{
  internal XmpData()
  {
    TagType = MetadataType.Xmp;
  }

  /// <summary>
  /// Simple representation of the data for debugging
  /// </summary>
  public string StringValue => (string)( _metadataValue?.Value ?? string.Empty );

  public bool IsArray => false;

  public ushort TagId => 0;

  public bool HasValue => ( (string)( _metadataValue?.Value ?? string.Empty ) ).Length > 0;

  public string TagTypeName => TagType.ToString();

  /// <summary>
  /// Type of data being held
  /// </summary>
  public MetadataType TagType { get; init; }

  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    var xmpData = DataConversion.ConvertBuffer( buffer, StringEncoding.Utf8 );
    _metadataValue = new MetadataTagValue( Type: TagType,
                                           IsArray: false,
                                           TagId: TagId,
                                           TagName: "Xmp",
                                           Value: xmpData );
  }

  public void SetValue( string value )
  {
    _metadataValue = new MetadataTagValue( Type: TagType,
                                           IsArray: false,
                                           TagId: TagId,
                                           TagName: "Xmp",
                                           Value: value );
  }

  public bool TryGetValue( out MetadataTagValue? value )
  {
    value = _metadataValue;
    return true;
  }

  public bool TryGetValueArray( out List<MetadataTagValue>? value )
  {
    value = null;
    return false;
  }

  private MetadataTagValue? _metadataValue = null;
}
