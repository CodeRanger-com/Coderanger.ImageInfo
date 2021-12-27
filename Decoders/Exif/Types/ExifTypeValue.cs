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
public class ExifTypeValue
{
  // Just used for reflection in the custom description attribute
  internal static readonly ExifTag ExifTag = new();

  internal ExifTypeValue( ExifType type, BinaryReader reader, ExifComponent component )
  {
    Tag = component.Tag;
    ExifType = type;
    Reader = reader;
    Component = component;

    var tagDetails = ExifTagDetailsAttribute.GetDescription( ExifTag, Tag );
    if( tagDetails != null )
    {
      Name = tagDetails.Name;
      Description = tagDetails.Description;
    }
  }

  public ushort Tag { get; init; }
  public ExifType ExifType { get; init; }
  public string Name { get; init; } = string.Empty;
  public string Description { get; init; } = string.Empty;

  internal BinaryReader Reader { get; init; }
  internal ExifComponent Component { get; init; }
}
