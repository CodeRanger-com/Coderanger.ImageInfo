// -----------------------------------------------------------------------
// <copyright file="ExifTypeFactory.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif;

internal static class ExifTypeFactory
{
  internal static IExifValue? Create( short type, int value )
  {
    switch( type )
    {
      // 1 byte per component
      case 1: // unsigned byte
      case 6: // signed byte
        return new ExifByte( value );

      case 2: // ascii strings    
        return new ExifString( value );

      // 2 bytes per component
      case 3: // unsigned short
        return new ExifUShort( value );

      case 8: // signed short
        return new ExifShort( value );

      // 4 bytes per component
      case 4: // unsigned long
        return new ExifULong( value );

      case 9: // signed long
        return new ExifLong( value );

      case 11: // single float
        return new ExifFloat( value );

      // 8 bytes
      case 5: // unsigned rational
      case 10: // signed rational
      case 12: // double float
        return new ExifRational( value );
    }

    return null;
  }
}
