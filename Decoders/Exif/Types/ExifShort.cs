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
public class ExifShort : ExifTypeValue, IExifValue
{
  internal ExifShort( BinaryReader reader, ExifComponent component )
    : base( ExifType.Short, reader, component )
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
      var value = DataConversion.Int16FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
      _processed = true;
    }

    return _convertedValue;
  }

  private ExifTagValue? _convertedValue;
  private bool _processed = false;
}

/// <summary>
/// 
/// </summary>
public class ExifUShort : ExifTypeValue, IExifValue
{
  internal ExifUShort( BinaryReader reader, ExifComponent component )
    : base( ExifType.UShort, reader, component )
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
      var value = DataConversion.UInt16FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      _convertedValue = new ExifTagValue( Type: ExifType, TagName: Name, Value: value );
      _processed = true;
    }

    return _convertedValue;
  }

  private ExifTagValue? _convertedValue;
  private bool _processed = false;
}
