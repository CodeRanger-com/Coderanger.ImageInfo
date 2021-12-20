// -----------------------------------------------------------------------
// <copyright file="ExifString.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal class ExifString : IExifValue, IValue<string>
{
  internal ExifString( int value )
  {

  }

  public string GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}
