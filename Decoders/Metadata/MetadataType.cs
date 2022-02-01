// -----------------------------------------------------------------------
// <copyright file="MetadataType.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

/// <summary>
/// 
/// </summary>
public enum MetadataType
{
  /// <summary>
  /// Unknown type of data object
  /// </summary>
  Unknown,

  /// <summary>
  /// Metadata value contains a byte, or an array of values
  /// </summary>
  Byte,

  /// <summary>
  /// Metadata value contains a string, or an array of values
  /// </summary>
  String,

  /// <summary>
  /// Metadata value contains an unsigned short or Int16, or an array of values
  /// </summary>
  UShort,

  /// <summary>
  /// Metadata value contains a signed short or Int16, or an array of values
  /// </summary>
  Short,

  /// <summary>
  /// Metadata value contains an unsigned Int32, or an array of values
  /// </summary>
  UInt,

  /// <summary>
  /// Metadata value contains a signed Int32, or an array of values
  /// </summary>
  Int,

  /// <summary>
  /// Metadata value contains a signed Long, or an array of values
  /// </summary>
  Long,

  /// <summary>
  /// Metadata value contains an unsigned Long, or an array of values
  /// </summary>
  ULong,

  /// <summary>
  /// Metadata value contains a Float, or an array of values
  /// </summary>
  Float,

  /// <summary>
  /// Metadata value contains a Double, or an array of values
  /// </summary>
  Double,

  /// <summary>
  /// Metadata value contains an signed Rational, or an array of values
  /// </summary>
  Rational,

  /// <summary>
  /// Metadata value contains an unsigned Rational, or an array of values
  /// </summary>
  URational,

  /// <summary>
  /// Metadata value contains a Date only within a DateTime, or an array of values
  /// </summary>
  Date,

  /// <summary>
  /// Metadata value contains a Time only within a DateTime, or an array of values
  /// </summary>
  Time,

  /// <summary>
  /// Metadata value contains a DateTime, or an array of values
  /// </summary>
  DateTime,

  /// <summary>
  /// Metadata value contains an enumerated value, or an array of values
  /// </summary>
  Enum,

  /// <summary>
  /// Metadata value contains a PNG Text value object
  /// </summary>
  PngText,

  /// <summary>
  /// Metadata value contains an XMP metadata string
  /// </summary>
  Xmp,
}
