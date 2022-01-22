// -----------------------------------------------------------------------
// <copyright file="XmpTagFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Xmp;

internal static class XmpTagFactory
{
  internal static XmpData Create()
  {
    return new XmpData();
  }
}
