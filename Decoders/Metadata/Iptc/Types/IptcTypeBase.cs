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

  internal IptcTypeBase( MetadataType type, ushort tagId )
  {
    TagType = type;
    TagId = tagId;

    var tagDetails = MetadataTagDetailsAttribute.GetTagDetails( ReflectionIptcTag, TagId );
    if( tagDetails != null )
    {
      Name = tagDetails.Name;
      Description = tagDetails.Description;
    }
  }

  public bool TryGetValue( out MetadataTagValue? value )
  {
    ProcessData();
    value = _convertedValue;
    return true;
  }

  public bool TryGetValueArray( out List<MetadataTagValue>? value )
  {
    ProcessData();
    value = _convertedValueArray;
    return true;
  }

  public virtual bool IsArray => _convertedValueArray.Count > 1;

  public override string ToString()
  {
    if( IsArray )
    {
      return $"{Name} = {string.Join( " / ", _convertedValueArray )}";
    }
    else
    {
      return $"{Name} = {_convertedValue}";
    }
  }

  internal void ProcessData()
  {
    if( !_processed )
    {
      foreach( var value in ExtractValues() )
      {
        if( value != null )
        {
          _convertedValueArray.Add( value );
        }
      }

      if( _convertedValueArray.Count == 1 )
{
        _convertedValue = _convertedValueArray[ 0 ];
      }

      _processed = true;
    }
  }

  internal void AddToExistingValue( MetadataTagValue? value )
  {
    if( value != null )
    {
      _convertedValueArray.Add( value );
      _convertedValue = null;
    }
  }

  /// <summary>
  /// Override to process the data buffer for each type
  /// </summary>
  /// <returns></returns>
  internal abstract IEnumerable<MetadataTagValue?> ExtractValues();

  /// <summary>
  /// Tag identifier
  /// </summary>
  public ushort TagId { get; init; }

  /// <summary>
  /// Determines if this tag can accept an array of items
  /// </summary>
  internal bool Repeatable { get; }

  /// <summary>
  /// Exif tag type
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

  protected MetadataTagValue? _convertedValue;
  protected readonly List<MetadataTagValue> _convertedValueArray = new();

  private bool _processed = false;
}
