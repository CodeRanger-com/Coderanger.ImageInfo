// -----------------------------------------------------------------------
// <copyright file="ExifFloat.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal class ExifFloat : IExifValue, IValue<float>
{
  internal ExifFloat( int value )
  {
    _value = value;
  }

  public float GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}
