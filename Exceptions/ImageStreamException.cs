// -----------------------------------------------------------------------
// <copyright file="ImageStreamException.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Exceptions;

public class ImageStreamException : ArgumentException
{
  public ImageStreamException( string message )
    : base( message )
  {
  }
}
