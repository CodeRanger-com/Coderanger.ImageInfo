// -----------------------------------------------------------------------
// <copyright file="BinaryReaderExtensions.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

internal static class BinaryReaderExtensions
{
  internal static void Skip( this BinaryReader reader, int jumpFromCurrent )
  {
    if( jumpFromCurrent == 0 ) return;

    if( reader.BaseStream.Position + jumpFromCurrent > reader.BaseStream.Length )
    {
      throw new ImageFormatException( $"Bad offset {jumpFromCurrent} + {reader.BaseStream.Position} is greater than stream length" );
    }

    reader.BaseStream.Seek( jumpFromCurrent, SeekOrigin.Current );
  }

  /// <summary>
  /// BinaryReader always reads little endian, so this helper will ensure data is
  /// bigendian by converting it on LE platforms
  /// </summary>
  internal static int ReadBigEndianInt32( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( int ) );
    return DataConversion.Int32FromBigEndianBuffer( bytes );

    //if( BitConverter.IsLittleEndian )
    //{
    //  // On LE platform so reverse LE to BE
    //  var data = reader.ReadBytes( sizeof( int ) ).Reverse();
    //  return BitConverter.ToInt32( data.ToArray(), 0 );
    //}
    //else
    //{
    //  // On a BE platform so just read it
    //  return BitConverter.ToInt32( reader.ReadBytes( sizeof( int ) ), 0 );
    //}
  }

  internal static uint ReadBigEndianUInt32( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( uint ) );
    return DataConversion.UInt32FromBigEndianBuffer( bytes );

    //if( BitConverter.IsLittleEndian )
    //{
    //  // On LE platform so reverse LE to BE
    //  var data = reader.ReadBytes( sizeof( int ) ).Reverse();
    //  return BitConverter.ToUInt32( data.ToArray(), 0 );
    //}
    //else
    //{
    //  // On a BE platform so just read it
    //  return BitConverter.ToUInt32( reader.ReadBytes( sizeof( int ) ), 0 );
    //}
  }

  /// <summary>
  /// BinaryReader always reads little endian, so this helper will ensure data is
  /// bigendian by converting it on LE platforms
  /// </summary>
  internal static short ReadBigEndianInt16( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( short ) );
    return DataConversion.Int16FromBigEndianBuffer( bytes );

    //if( BitConverter.IsLittleEndian )
    //{
    //  // On LE platform so reverse LE to BE
    //  //var lenHi = reader.ReadByte();
    //  //var lenLo = reader.ReadByte();
    //  //return (short)( ( lenHi << 8 ) | lenLo );

    //  //var data = binaryReader.ReadBytes( sizeof( short ) ).Reverse();
    //  //return BitConverter.ToInt16( data.ToArray(), 0 );
    //}
    //else
    //{
    //  // On a BE platform so just read it
    //  return BitConverter.ToInt16( reader.ReadBytes( sizeof( short ) ), 0 );
    //}
  }

  /// <summary>
  /// BinaryReader always reads little endian, so this helper will ensure data is
  /// bigendian by converting it on LE platforms
  /// </summary>
  internal static ushort ReadBigEndianUInt16( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( ushort ) );
    return DataConversion.UInt16FromBigEndianBuffer( bytes );

    //if( BitConverter.IsLittleEndian )
    //{
    //  // On LE platform so reverse LE to BE
    //  var lenHi = reader.ReadByte();
    //  var lenLo = reader.ReadByte();
    //  return (ushort)( ( lenHi << 8 ) | lenLo );
    //}
    //else
    //{
    //  // On a BE platform so just read it
    //  return BitConverter.ToUInt16( reader.ReadBytes( sizeof( ushort ) ), 0 );
    //}
  }
}
