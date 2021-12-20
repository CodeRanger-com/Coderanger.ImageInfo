// -----------------------------------------------------------------------
// <copyright file="ExifShort.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal class ExifShort : IExifValue, IValue<short>
{
  internal ExifShort( int value )
  {

  }

  public short GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}

internal class ExifUShort : IExifValue, IValue<ushort>
{
  internal ExifUShort( int value )
  {

  }

  public ushort GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}
