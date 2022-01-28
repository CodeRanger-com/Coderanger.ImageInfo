// -----------------------------------------------------------------------
// <copyright file="XmpData.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// Specifications:
// https://en.wikipedia.org/wiki/Extensible_Metadata_Platform
// https://wwwimages2.adobe.com/content/dam/acom/en/devnet/xmp/pdfs/XMP%20SDK%20Release%20cc-2016-08/XMPSpecificationPart3.pdf
// </comment>
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
    SetValue( xmpData );
  }

  public void SetValue( string value )
  {
    TrimPacket( ref value );
    _metadataValue = new MetadataTagValue( Type: TagType,
                                           IsArray: false,
                                           TagId: TagId,
                                           TagName: "Xmp",
                                           Value: value.Trim() );
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

  private static void TrimPacket( ref string data )
  {
    DeleteTag( PacketStart, ref data );
    DeleteTag( PacketEnd, ref data );
  }

  private static void DeleteTag( string tag, ref string data )
  {
    var start = data.IndexOf( tag );
    if( start != -1 )
    {
      var end = data.IndexOf( CloseTag, start );
      if( end != -1 )
      {
        data = data.Remove( start, ( end - start ) + CloseTag.Length );
      }
    }
  }

  private MetadataTagValue? _metadataValue = null;

  private const string CloseTag = "?>";
  private const string PacketStart = "<?xpacket begin";
  private const string PacketEnd = "<?xpacket end";
}
