// -----------------------------------------------------------------------
// <copyright file="DensityUnit.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

internal enum DensityUnit : byte
{
  AspectRatio = 0,

  PixelsPerInch = 1,

  PixelsPerCentimeter = 2,

  PixelsPerMeter = 3
}
