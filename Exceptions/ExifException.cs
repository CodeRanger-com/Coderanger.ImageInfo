// -----------------------------------------------------------------------
// <copyright file="ExifException.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Exceptions;

public class ExifException : ArgumentException
{
  public ExifException( string message )
    : base( message )
  {
  }
}
