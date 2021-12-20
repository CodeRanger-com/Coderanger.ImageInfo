// -----------------------------------------------------------------------
// <copyright file="ExceptionHelper.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// Thrown when an image is invalid for the format being decoded
/// </summary>
public class ImageFormatException : ArgumentException
{
  public ImageFormatException( string message, string? paramName = null ) : base( message, paramName ) { }
}

internal static class ExceptionHelper
{
  /// <summary>
  /// Simple helper to reset the reader before creating an exception to throw
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="message"></param>
  /// <returns></returns>
  internal static ImageFormatException Throw( BinaryReader reader, string message )
  {
    reader.BaseStream.Position = 0;
    return new ImageFormatException( message );
  }
}
