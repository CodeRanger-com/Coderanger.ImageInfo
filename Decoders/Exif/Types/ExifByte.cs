// -----------------------------------------------------------------------
// <copyright file="ExifByte.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

/// <summary>
/// 
/// </summary>
public class ExifByte : ExifTypeValue, IExifValue
{
  internal ExifByte( BinaryReader reader, ExifComponent component )
    : base( ExifType.Byte, reader, component )
  {
  }

  public bool TryGetValue( out ExifTagValue? value )
  {
    value = GetValue();
    return true;
  }

  private ExifTagValue? GetValue()
  {
    if( !_processed )
    {
      _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: Component.DataValueBuffer[ 0 ] );
      _processed = true;
    }

    return _convertedValue;
  }

  private ExifTagValue? _convertedValue;
  private bool _processed = false;
}
