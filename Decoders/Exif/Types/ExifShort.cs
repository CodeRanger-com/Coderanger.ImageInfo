// -----------------------------------------------------------------------
// <copyright file="ExifShort.cs" company="CodeRanger.com">
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
public class ExifShort : ExifValue
{
  internal ExifShort( BinaryReader reader, ExifComponent component )
    : base( reader, component )
  {
  }

  public new bool TryGetShort( out short value )
  {
    value = GetValue();
    return true;
  }

  private short GetValue()
  {
    if( !_processed )
    {
      _convertedValue = DataConversion.Int16FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      _processed = true;
    }

    return _convertedValue;
  }

  private short _convertedValue;
  private bool _processed = false;
}

/// <summary>
/// 
/// </summary>
public class ExifUShort : ExifValue
{
  internal ExifUShort( BinaryReader reader, ExifComponent component )
    : base( reader, component )
  {
  }

  public new bool TryGetUShort( out ushort value )
  {
    value = GetValue();
    return true;
  }

  private ushort GetValue()
  {
    if( !_processed )
    {
      _convertedValue = DataConversion.UInt16FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      _processed = true;
    }

    return _convertedValue;
  }

  private ushort _convertedValue;
  private bool _processed = false;
}
