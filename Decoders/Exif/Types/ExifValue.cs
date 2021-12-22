// -----------------------------------------------------------------------
// <copyright file="IExifValue.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// 
/// </summary>
internal interface IExifValue
{
  public bool TryGetByte( out byte value );
  public bool TryGetLong( out long value );
  public bool TryGetULong( out ulong value );
  public bool TryGetShort( out short value );
  public bool TryGetUShort( out ushort value );
  public bool TryGetRational( out double value );
  public bool TryGetFloat( out float value );
  public bool TryGetString( out string value );
  public bool TryGetDateTime( out DateTime? value );
}

/// <summary>
/// 
/// </summary>
public class ExifValue : IExifValue
{
  internal ExifValue( BinaryReader reader, ExifComponent component )
  {
    Reader = reader;
    Component = component;
  }

  public bool TryGetByte( out byte value )
  {
    value = default;
    return false;
  }
  public bool TryGetLong( out long value )
  {
    value = default;
    return false;
  }
  public bool TryGetULong( out ulong value )
  {
    value = default;
    return false;
  }
  public bool TryGetShort( out short value )
  {
    value = default;
    return false;
  }
  public bool TryGetUShort( out ushort value )
  {
    value = default;
    return false;
  }
  public bool TryGetRational( out double value )
  {
    value = default;
    return false;
  }
  public bool TryGetFloat( out float value )
  {
    value = default;
    return false;
  }
  public bool TryGetString( out string value )
  {
    value = string.Empty;
    return false;
  }
  public bool TryGetDateTime( out DateTime? value )
  {
    value = null;
    return false;
  }

  public ExifType GetExifType()
  {
    switch( Component.DataType )
    {
      case 1: // unsigned byte
      case 6: // signed byte
        return ExifType.Byte;

      case 2: // ascii strings    
        return ExifType.String;

      case 3: // unsigned short
        return ExifType.UShort;

      case 8: // signed short
        return ExifType.Short;

      case 4: // unsigned long
        return ExifType.ULong;

      case 9: // signed long
        return ExifType.Long;

      case 11: // single float
      case 12: // double float
        return ExifType.Float;

      case 5: // unsigned rational
      case 10: // signed rational
        return ExifType.Rational;
    }

    return ExifType.Unknown;
  }

  internal BinaryReader Reader { get; init; }
  internal ExifComponent Component { get; init; }
}
