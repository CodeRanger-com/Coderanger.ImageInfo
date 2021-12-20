// -----------------------------------------------------------------------
// <copyright file="UnitConvertor.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

internal static class UnitConvertor
{
  private const float InchesPerMetre = 39.3701F;
  private const float InchesPerCentiMetre = InchesPerMetre / 100;
  private const int DefaultDpi = 96;

  internal static int ToDpi( DensityUnit unit, double pixels )
  {
    if( pixels == 1 ) return DefaultDpi;

    return unit switch
    {
      DensityUnit.PixelsPerInch => Convert.ToInt32( pixels ),
      DensityUnit.PixelsPerMeter => Convert.ToInt32( Math.Round( pixels / InchesPerMetre ) ),
      DensityUnit.PixelsPerCentimeter => Convert.ToInt32( Math.Round( pixels / InchesPerCentiMetre ) ),
      _ => Convert.ToInt32( pixels ),
    };
  }
}
