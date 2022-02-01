// -----------------------------------------------------------------------
// <copyright file="ImageStreamException.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Exceptions;

/// <summary>
/// Thrown if the image Stream is invalid or cannot be read
/// </summary>
public class ImageStreamException : ArgumentException
{
  /// <summary>
  /// Thrown if the image Stream is invalid or cannot be read
  /// </summary>
  /// <param name="message">Error message</param>
  public ImageStreamException( string message )
    : base( message )
  {
  }
}
