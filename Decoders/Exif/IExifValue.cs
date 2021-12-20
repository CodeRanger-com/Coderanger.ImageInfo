// -----------------------------------------------------------------------
// <copyright file="IExifValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal interface IValue<T>
{
  public T GetValue();
}

internal interface IExifValue
{
}
