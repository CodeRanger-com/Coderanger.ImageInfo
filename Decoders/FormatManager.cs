// -----------------------------------------------------------------------
// <copyright file="FormatManager.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>Manages all the decoders</comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders;

using Coderanger.ImageInfo.Decoders.Bmp;
using Coderanger.ImageInfo.Decoders.Jpeg;
using Coderanger.ImageInfo.Decoders.Png;

internal class FormatManager
{
  internal delegate IDecoder? DetectFormatDelegate( BinaryReader reader );

  internal IEnumerable<DetectFormatDelegate> Get()
  {
    foreach( var decoder in _decoders )
    {
      yield return decoder;
    }
  }

  private readonly List<DetectFormatDelegate> _decoders = new() { 
    DecodeJpeg.DetectFormat,
    DecodePng.DetectFormat,
    DecodeBmp.DetectFormat,
  };
}
