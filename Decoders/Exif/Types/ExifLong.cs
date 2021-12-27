// -----------------------------------------------------------------------
// <copyright file="ExifLong.cs" company="CodeRanger.com">
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
internal class ExifLong : ExifTypeValue, IExifValue
{
  internal ExifLong( BinaryReader reader, ExifComponent component )
    :base( ExifType.Long, reader, component )
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
      if( Component.ComponentCount == 1 )
      {
        // Its a 4 byte (int32) value but it is expected to be an 8 byte value
        // so its safe to just cast it
        var value = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
        _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: (long)value );
        _processed = true;
        return _convertedValue;
      }

      // Store current reader position for restoring later
      var currentStreamPosition = Reader.BaseStream.Position;

      // Rational type is 8 bytes, so size * 8 is the data size
      // Total data length is larger than 4bytes, so next 4bytes contains an offset to data
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );
      if( Component.ComponentCount * Component.ComponentSize == 8 )
      {
        var value = DataConversion.Int64FromBuffer( dataValue, 0, Component.ByteOrder );
        _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
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

/// <summary>
/// 
/// </summary>
internal class ExifULong : ExifTypeValue, IExifValue
{
  internal ExifULong( BinaryReader reader, ExifComponent component )
    : base( ExifType.ULong, reader, component )
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
      if( Component.ComponentCount == 1 )
      {
        // Its a 4 byte (int32) value but it is expected to be an 8 byte value
        // so its safe to just cast it
        var value = DataConversion.UInt32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
        _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: (ulong)value );
        _processed = true;
        return _convertedValue;
      }

      // Store current reader position for restoring later
      var currentStreamPosition = Reader.BaseStream.Position;

      // Rational type is 8 bytes, so size * 8 is the data size
      // Total data length is larger than 4bytes, so next 4bytes contains an offset to data
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );
      if( Component.ComponentCount * Component.ComponentSize == 8 )
      {
        var value = DataConversion.UInt64FromBuffer( dataValue, 0, Component.ByteOrder );
        _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
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
