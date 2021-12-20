// -----------------------------------------------------------------------
// <copyright file="ExifRational.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal class ExifRational : IExifValue, IValue<double>
{
  internal ExifRational( int value )
  {
    _value = value;
  }

  public double GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}
