// -----------------------------------------------------------------------
// <copyright file="ImageDecoderException.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Exceptions;

public class ImageDecoderException : ArgumentException
{
  public ImageDecoderException( string message )
    : base( message )
  {
  }
}
