// -----------------------------------------------------------------------
// <copyright file="ExifTypeValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// 
/// </summary>
public abstract class ExifTypeValue
{
  // Just used for reflection in the custom description attribute
  private static readonly ExifTag ReflectionExifTag = new();

  internal ExifTypeValue( ExifType type, BinaryReader reader, ExifComponent component )
  {
    Tag = component.Tag;
    ExifType = type;
    Reader = reader;
    Component = component;

    var tagDetails = ExifTagDetailsAttribute.GetDescription( ReflectionExifTag, Tag );
    if( tagDetails != null )
    {
      Name = tagDetails.Name;
      Description = tagDetails.Description;
    }
  }

  public bool TryGetValue( out ExifTagValue? value )
  {
    ProcessData();
    value = _convertedValue;
    return true;
  }

  public bool TryGetValueArray( out List<ExifTagValue>? value )
  {
    ProcessData();
    value = _convertedValueArray;
    return true;
  }

  public virtual bool IsArray => Component.ComponentCount > 1;

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
      // Store current reader position for restoring later
      var currentStreamPosition = Reader.BaseStream.Position;

      foreach( var value in ExtractValues() )
      {
        _convertedValueArray.Add( value );
      }

      if( !IsArray && _convertedValueArray.Count > 0 )
      {
        _convertedValue = _convertedValueArray[ 0 ];
      }

      // Reset position
      Reader.BaseStream.Position = currentStreamPosition;

      _processed = true;
    }
  }

  /// <summary>
  /// Override to process the data buffer for each type
  /// </summary>
  /// <returns></returns>
  internal abstract IEnumerable<ExifTagValue> ExtractValues();

  /// <summary>
  /// Tag identifier
  /// </summary>
  public ushort Tag { get; init; }

  /// <summary>
  /// Exif tag type
  /// </summary>
  public ExifType ExifType { get; init; }

  /// <summary>
  /// Name of tag
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Description with explanation of tag data
  /// </summary>
  public string Description { get; init; } = string.Empty;

  internal BinaryReader Reader { get; init; }
  internal ExifComponent Component { get; init; }

  protected ExifTagValue? _convertedValue;
  protected readonly List<ExifTagValue> _convertedValueArray = new();

  protected const byte BufferByteSize = 4;

  private bool _processed = false;
}
