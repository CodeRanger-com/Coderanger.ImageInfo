// -----------------------------------------------------------------------
// <copyright file="FormatManager.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders;

using Coderanger.ImageInfo.Decoders.Jpeg;

internal class FormatManager
{
  internal IEnumerable<IDecoder> Get()
  {
    foreach( var decoder in _decoders )
    {
      yield return decoder;
    }
  }

  private readonly List<IDecoder> _decoders = new() { new DecodeJpeg() };
}
