// -----------------------------------------------------------------------
// <copyright file="ExifByte.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal class ExifByte : IExifValue, IValue<byte>
{
  internal ExifByte( int value )
  {

  }

  public byte GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}
