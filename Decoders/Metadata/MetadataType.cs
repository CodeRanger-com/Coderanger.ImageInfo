// -----------------------------------------------------------------------
// <copyright file="MetadataType.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

public enum MetadataType
{
  Unknown,
  Byte,
  String,
  UShort,
  Short,
  UInt,
  Int,
  Long,
  ULong,
  Float,
  Double,
  Rational,
  URational,
  Date,
  Time,
  DateTime,
  Enum,
  PngText,
  Xmp,
}
