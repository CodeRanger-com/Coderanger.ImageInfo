// -----------------------------------------------------------------------
// <copyright file="ImageDecoderException.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Exceptions;

/// <summary>
/// Thrown when an image cannot be decoded
/// </summary>
public class ImageDecoderException : ArgumentException
{
  /// <summary>
  /// Thrown when an image cannot be decoded
  /// </summary>
  /// <param name="message">Error message</param>
  public ImageDecoderException( string message )
    : base( message )
  {
  }
}
