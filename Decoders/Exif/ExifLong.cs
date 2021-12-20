// -----------------------------------------------------------------------
// <copyright file="ExifLong.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal class ExifLong : IExifValue, IValue<long>
{
  internal ExifLong( int value )
  {

  }

  public long GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}

internal class ExifULong : IExifValue, IValue<ulong>
{
  internal ExifULong( int value )
  {

  }

  public ulong GetValue()
  {
    throw new NotImplementedException();
  }

  private readonly int _value;
}
