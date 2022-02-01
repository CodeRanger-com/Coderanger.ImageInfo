// -----------------------------------------------------------------------
// <copyright file="Rational.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;

/// <summary>
/// Simple record type holding a rational number
/// </summary>
/// <param name="Numerator"></param>
/// <param name="Denominator"></param>
public record Rational( int Numerator, int Denominator )
{
  /// <summary>
  /// Returns a double value that represents the object
  /// </summary>
  /// <returns>Double value that represents the object</returns>
  public double ToDouble()
  {
    return (double)Numerator / Denominator;
  }

  /// <summary>
  /// Returns a string that represents the object
  /// </summary>
  /// <returns>String that represents the object</returns>
  public override string ToString()
  {
    return $"{Numerator}/{Denominator} ({ToDouble()}D)";
  }
}
