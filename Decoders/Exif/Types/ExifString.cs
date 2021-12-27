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
public class ExifString : ExifTypeValue, IExifValue
{
  internal ExifString( ExifStringEncoding encoding, BinaryReader reader, ExifComponent component )
    : base( ExifType.String, reader, component )
  {
    _encoding = encoding;
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

      var byteCount = Component.ComponentCount * Component.ComponentSize;
      var dataValue = Reader.ReadBytes( byteCount );
      if( dataValue?.Length > 0 )
      {
        if( _encoding == ExifStringEncoding.Ascii )
        {
          // -1 for end null byte
          var value = Encoding.UTF8.GetString( dataValue, 0, byteCount - 1 );
          _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
        }
        else if( _encoding == ExifStringEncoding.Ucs2 )
        {
          // -2 for end double-byte nulls
          var value = Encoding.GetEncoding( "UCS-2" ).GetString( dataValue, 0, byteCount - 2 );
          _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
        }
        else
        {
          // UTF8 is best for unknown encoding, as it decodes ascii as well as dbcs characters
          // -1 for end null byte
          var value = Encoding.UTF8.GetString( dataValue, 0, byteCount - 1 );
          _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
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

  private readonly ExifStringEncoding _encoding;
}
