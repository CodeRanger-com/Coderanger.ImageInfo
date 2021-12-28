// -----------------------------------------------------------------------
// <copyright file="IExifValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// 
/// </summary>
public interface IExifValue
{
  internal void SetValue();
  public bool TryGetValue( out ExifTagValue? value );
  public bool TryGetValueArray( out List<ExifTagValue>? value );
  public bool IsArray { get; }
  public ushort Tag { get; }
  public string StringValue { get; }
}
