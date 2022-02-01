// -----------------------------------------------------------------------
// <copyright file="BinaryReaderExtensions.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

using Coderanger.ImageInfo.Exceptions;

internal static class BinaryReaderExtensions
{
  /// <summary>
  /// Helper to skip a number of bytes
  /// </summary>
  /// <param name="reader">BinaryReader object to extend</param>
  /// <param name="jumpFromCurrent">Count of bytes to skip from current position</param>
  /// <exception cref="ImageFormatException"></exception>
  internal static void Skip( this BinaryReader reader, long jumpFromCurrent )
  {
    if( jumpFromCurrent == 0 ) return;

    if( reader.Position() + jumpFromCurrent > reader.Length() )
    {
      throw new ImageFormatException( $"Bad offset {jumpFromCurrent} + {reader.Position()} is greater than stream length", nameof( jumpFromCurrent ) );
    }

    reader.BaseStream.Seek( jumpFromCurrent, SeekOrigin.Current );
  }

  /// <summary>
  /// Returns position of current base stream
  /// </summary>
  /// <param name="reader">BinaryReader object to extend</param>
  /// <returns>Value of current stream position</returns>
  internal static long Position( this BinaryReader reader ) => reader.BaseStream.Position;

  /// <summary>
  /// Sets the position of current base stream
  /// </summary>
  /// <param name="reader">BinaryReader object to extend</param>
  /// <param name="position">Position to move to from the beginning</param>
  internal static void Position( this BinaryReader reader, long position )
  {
    reader.BaseStream.Position = position;
  }

  /// <summary>
  /// Returns length of current binary reader
  /// </summary>
  /// <param name="reader">BinaryReader object to extend</param>
  /// <returns>Value of current length</returns>
  internal static long Length( this BinaryReader reader ) => reader.BaseStream.Length;

  /// <summary>
  /// BinaryReader always reads little endian, so this helper will ensure data is
  /// bigendian by converting it on LE platforms
  /// </summary>
  internal static int ReadBigEndianInt32( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( int ) );
    return DataConversion.Int32FromBigEndianBuffer( bytes );
  }

  internal static uint ReadBigEndianUInt32( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( uint ) );
    return DataConversion.UInt32FromBigEndianBuffer( bytes );
  }

  /// <summary>
  /// BinaryReader always reads little endian, so this helper will ensure data is
  /// bigendian by converting it on LE platforms
  /// </summary>
  internal static short ReadBigEndianInt16( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( short ) );
    return DataConversion.Int16FromBigEndianBuffer( bytes );
  }

  /// <summary>
  /// BinaryReader always reads little endian, so this helper will ensure data is
  /// bigendian by converting it on LE platforms
  /// </summary>
  internal static ushort ReadBigEndianUInt16( this BinaryReader reader )
  {
    var bytes = reader.ReadBytes( sizeof( ushort ) );
    return DataConversion.UInt16FromBigEndianBuffer( bytes );
  }
}
