// -----------------------------------------------------------------------
// <copyright file="DecoderHelper.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

internal static class DecoderHelper
{
  internal static bool MatchesMagic( byte[] buffer, byte[] magicBytes )
  {
    return buffer.Length >= magicBytes.Length
          && buffer.AsSpan( 0, magicBytes.Length ).SequenceEqual( magicBytes );
  }
}
