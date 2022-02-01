// -----------------------------------------------------------------------
// <copyright file="ImageFormatException.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Exceptions;

/// <summary>
/// Thrown when an image is invalid for the format being decoded
/// </summary>
public class ImageFormatException : ArgumentException
{
  /// <summary>
  /// Thrown when an image is invalid for the format being decoded
  /// </summary>
  /// <param name="message">Error message</param>
  /// <param name="paramName">Name of parameter causing the exception</param>
  public ImageFormatException( string message, string? paramName = null ) : base( message, paramName ) { }
}

