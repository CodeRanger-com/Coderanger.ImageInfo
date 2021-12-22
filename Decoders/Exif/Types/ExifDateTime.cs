// -----------------------------------------------------------------------
// <copyright file="ExifDateTime.cs" company="CodeRanger.com">
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
public class ExifDateTime : ExifValue
{
  internal ExifDateTime( BinaryReader reader, ExifComponent component )
    : base( reader, component )
  {
  }

  public new bool TryGetDateTime( out DateTime? value )
  {
    value = GetValue();
    return true;
  }

  private DateTime? GetValue()
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
        // 2020:01:02 09:19:25
        if( DateTime.TryParseExact( value, "yyyy:MM:dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out var dt ) )
        {
          _convertedValue = dt;
        }
      }

      _processed = true;

      // Reset position
      Reader.BaseStream.Position = currentStreamPosition;
    }

    return _convertedValue;
  }

  private bool _processed = false;
  private DateTime? _convertedValue = null;
}
