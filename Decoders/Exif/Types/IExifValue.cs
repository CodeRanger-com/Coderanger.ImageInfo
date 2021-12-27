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
  public bool TryGetValue( out ExifTagValue? value );
  public ushort Tag { get; }
}
