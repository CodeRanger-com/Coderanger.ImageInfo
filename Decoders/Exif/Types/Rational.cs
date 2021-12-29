// -----------------------------------------------------------------------
// <copyright file="Rational.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// Simple record type holding a rational number
/// </summary>
/// <param name="Numerator"></param>
/// <param name="Denominator"></param>
public record Rational( int Numerator, int Denominator )
{
  public double ToDouble()
  {
    return (double)Numerator / Denominator;
  }

  public override string ToString()
  {
    return $"{Numerator}/{Denominator} ({ToDouble()}D)";
  }
}
