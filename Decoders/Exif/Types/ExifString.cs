// -----------------------------------------------------------------------
// <copyright file="ExifString.cs" company="CodeRanger.com">
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
public class ExifString : ExifValue
{
  internal ExifString( ExifStringEncoding encoding, BinaryReader reader, ExifComponent component )
    : base( reader, component )
  {
    _encoding = encoding;
  }

  public new bool TryGetString( out string value )
  {
    value = GetValue();
    return true;
  }

  private string GetValue()
  {
    if( !_processed )
    {
      // Store current reader position for restoring later
      var currentStreamPosition = Reader.BaseStream.Position;

      // Rational type is 8 bytes, so size * 8 is the data size
      // Total data length is larger than 4bytes, so next 4bytes contains an offset to data
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var byteCount = ( Component.ComponentCount * Component.ComponentSize );
      var dataValue = Reader.ReadBytes( byteCount );
      if( dataValue != null )
      {
        if( _encoding == ExifStringEncoding.Ascii )
        {
          // -1 for end null byte
          _convertedValue = Encoding.ASCII.GetString( dataValue, 0, byteCount - 1 );
        }
        else
        {
          // -2 for end null bytes
          _convertedValue = Encoding.GetEncoding( "UCS-2" ).GetString( dataValue, 0, byteCount - 2 );
        }
      }

      _processed = true;

      // Reset position
      Reader.BaseStream.Position = currentStreamPosition;
    }

    return _convertedValue;
  }

  private bool _processed = false;
  private string _convertedValue = string.Empty;
  private readonly ExifStringEncoding _encoding;
}
