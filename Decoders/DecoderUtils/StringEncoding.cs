// -----------------------------------------------------------------------
// <copyright file="StringEncoding.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// Determines the type of encoding of the string
/// </summary>
internal enum StringEncoding
{
  None,
  Ascii,
  Ucs2,
  Utf8,
  Unicode,
  Jis,
  Undefined,
}
