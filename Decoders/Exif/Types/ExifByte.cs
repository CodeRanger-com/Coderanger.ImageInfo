// -----------------------------------------------------------------------
// <copyright file="ExifByte.cs" company="CodeRanger.com">
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
public class ExifByte : ExifValue
{
  internal ExifByte( BinaryReader reader, ExifComponent component )
    : base( reader, component )
  {
  }

  public new bool TryGetByte( out byte value )
  {
    value = GetValue();
    return true;
  }

  private byte GetValue()
  {
    if( !_processed )
    {
      _convertedValue = Component.DataValueBuffer[ 8 ];
      _processed = true;
    }

    return _convertedValue;
  }

  private byte _convertedValue;
  private bool _processed = false;
}
