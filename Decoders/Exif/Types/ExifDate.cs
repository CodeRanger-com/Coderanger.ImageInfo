// -----------------------------------------------------------------------
// <copyright file="ExifDate.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

using System.Text;
using Coderanger.ImageInfo.Decoders.DecoderUtils;


/// <summary>
/// 
/// </summary>
public class ExifDate : ExifTypeValue, IExifValue
{
  internal ExifDate( BinaryReader reader, ExifComponent component )
    : base( ExifType.Date, reader, component )
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
      // Store current reader position for restoring later
      var currentStreamPosition = Reader.BaseStream.Position;

      // Rational type is 8 bytes, so size * 8 is the data size
      // Total data length is larger than 4bytes, so next 4bytes contains an offset to data
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      // -1 for end null byte
      var byteCount = ( Component.ComponentCount * Component.ComponentSize ) - 1;
      var dataValue = Reader.ReadBytes( byteCount );
      if( dataValue != null )
      {
        var value = Encoding.ASCII.GetString( dataValue );
        if( DateOnly.TryParseExact( value, "yyyy:MM:dd", null, System.Globalization.DateTimeStyles.None, out var dt ) )
        {
          // Dates are stored as ASCII strings, but we can do better
          _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: dt );
        }
      }

      _processed = true;

      // Reset position
      Reader.BaseStream.Position = currentStreamPosition;
    }

    return _convertedValue;
  }

  private ExifTagValue? _convertedValue;
  private bool _processed = false;
}
