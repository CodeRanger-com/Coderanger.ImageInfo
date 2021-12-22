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
internal class ExifLong : ExifValue
{
  internal ExifLong( BinaryReader reader, ExifComponent component )
    :base( reader, component )
  {
  }

  public new bool TryGetLong( out long value )
  {
    value = GetValue();
    return true;
  }

  private long GetValue()
  {
    if( !_processed )
    {
      if( Component.ComponentCount == 1 )
      {
        _convertedValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
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
        _convertedValue = DataConversion.Int64FromBuffer( dataValue, 0, Component.ByteOrder );
      }

      _processed = true;

      // Reset position
      Reader.BaseStream.Position = currentStreamPosition;
    }

    return _convertedValue;
  }

  private long _convertedValue;
  private bool _processed = false;
}

/// <summary>
/// 
/// </summary>
internal class ExifULong : ExifValue
{
  internal ExifULong( BinaryReader reader, ExifComponent component )
    : base( reader, component )
  {
  }

  public new bool TryGetULong( out ulong value )
  {
    value = GetValue();
    return true;
  }

  private ulong GetValue()
  {
    if( !_processed )
    {
      if( Component.ComponentCount == 1 )
      {
        _convertedValue = DataConversion.UInt32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
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
        _convertedValue = DataConversion.UInt64FromBuffer( dataValue, 0, Component.ByteOrder );
      }

      _processed = true;

      // Reset position
      Reader.BaseStream.Position = currentStreamPosition;
    }

    return _convertedValue;
  }

  private ulong _convertedValue;
  private bool _processed = false;
}
