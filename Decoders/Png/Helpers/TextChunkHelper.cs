// -----------------------------------------------------------------------
// <copyright file="TextChunkHelper.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Png.Helpers;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal record struct TextSlice( string Value, int ByteEndPosition );

internal static class TextChunkHelper
{
  /// <summary>
  /// Extract a null terminated string from a text chunk buffer
  /// </summary>
  /// <param name="buffer">Text chunk buffer which null terminates the keyword</param>
  /// <returns>Valid word string in given encoding</returns>
  internal static TextSlice GetTerminatedStringFromBuffer( ReadOnlySpan<byte> buffer, StringEncoding encoding = StringEncoding.Ascii )
  {
    // Find position of the null byte, then chop and convert the bytes before
    int wordEnd = 0;
    while( wordEnd < 80 )
    {
      if( buffer[ wordEnd++ ] == 0 )
      {
        break;
      }
    }

    // Position is the null, so remove one for the keyword
    wordEnd--;

    var wordBuffer = buffer[ ..wordEnd ];

    return new TextSlice( Value: DataConversion.ConvertBuffer( wordBuffer, encoding ),
                          ByteEndPosition: wordEnd );
  }
}
