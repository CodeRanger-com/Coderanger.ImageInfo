// -----------------------------------------------------------------------
// <copyright file="IptcTypeBase.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc.Types;

public abstract class IptcTypeBase
{
  // Hack: Just used for reflection in the custom description attribute
  internal static readonly IptcTag ReflectionIptcTag = new();

  internal IptcTypeBase( ushort tagId, MetadataType type )
  {
    TagId = tagId;
    TagType = type;

    var tagDetails = MetadataTagDetailsAttribute.GetTagDetails( ReflectionIptcTag, TagId );
    if( tagDetails != null )
    {
      Name = tagDetails.Name;
      Description = tagDetails.Description;
    }
  }

  /// <summary>
  /// Simple representation of the data for debugging
  /// </summary>
  public string StringValue => ToString();

  /// <summary>
  /// Tag identifier
  /// </summary>
  public ushort TagId { get; init; }

  /// <summary>
  /// Type of data being held
  /// </summary>
  public MetadataType TagType { get; init; }

  /// <summary>
  /// Name of tag
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Description with explanation of tag data
  /// </summary>
  public string Description { get; init; } = string.Empty;

  /// <summary>
  /// Determines if this value is an array of data
  /// </summary>
  public bool IsArray => _metadata.Count > 1;

  public bool HasValue => _metadata.Count > 0;

  public string TagTypeName => TagType.ToString();

  public override string ToString()
  {
    return $"{Name} = {string.Join( " / ", _metadata )}";
  }

  /// <summary>
  /// Sets the metadata value if there is one and returns true
  /// </summary>
  public bool TryGetValue( out MetadataTagValue? value )
  {
    if( _metadata.Count == 0 )
    {
      value = null;
      return false;
    }

    value = _metadata[ 0 ];
    return true;
  }

  /// <summary>
  /// Sets the array of metadata values if there are any and returns true
  /// </summary>
  public bool TryGetValueArray( out List<MetadataTagValue>? value )
  {
    if( _metadata.Count == 0 )
    {
      value = null;
      return false;
    }

    value = _metadata;
    return true;
  }

  protected readonly List<MetadataTagValue> _metadata = new();
}
