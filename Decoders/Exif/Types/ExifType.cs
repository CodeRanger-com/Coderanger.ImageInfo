// -----------------------------------------------------------------------
// <copyright file="ExifType.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

public enum ExifType
{
  Unknown,
  Byte,
  String,
  UShort,
  Short,
  ULong,
  Long,
  Float,
  Rational,
  DateTime,
}
