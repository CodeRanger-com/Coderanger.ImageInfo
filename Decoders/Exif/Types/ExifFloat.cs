// -----------------------------------------------------------------------
// <copyright file="ExifFloat.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class ExifFloat : ExifTypeValue, IExifValue
{
  internal ExifFloat( BinaryReader reader, ExifComponent component )
    : base( ExifType.Float, reader, component )
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

      var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );
      if( Component.ComponentCount * Component.ComponentSize == 8 )
      {
        var enumer = DataConversion.Int32FromBuffer( dataValue, 0, Component.ByteOrder );
        var denom = DataConversion.Int32FromBuffer( dataValue, 4, Component.ByteOrder );

        _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: enumer / denom );
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
