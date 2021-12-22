// -----------------------------------------------------------------------
// <copyright file="DataConversion.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// Buffer conversion helper class
/// </summary>
internal static class DataConversion
{
  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 2 byte 16bit
  /// signed integer (short or Int16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 2 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static short Int16FromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return Int16FromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToInt16( buffer, offset );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 2 byte 16bit
  /// unsigned integer (ushort or UInt16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 2 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ushort UInt16FromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return UInt16FromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToUInt16( buffer, offset );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 4 byte 32bit
  /// signed integer (Int32)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 4 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static int Int32FromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return Int32FromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToInt32( buffer, offset );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 4 byte 32bit
  /// unsigned integer (UInt32)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 4 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static uint UInt32FromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return UInt32FromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToUInt32( buffer, offset );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into an 8 byte 64bit
  /// signed long (Int64)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static long Int64FromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return Int64FromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToInt64( buffer, offset );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into an 8 byte 64bit
  /// unsigned long (UInt64)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ulong UInt64FromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return UInt64FromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToUInt64( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 2 byte 16 bit signed integer (short or Int16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 2 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static short Int16FromBigEndianBuffer( byte[] buffer, int offset )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + 2 > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( buffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return (short)( ( buffer[ offset ] << 8 ) | buffer[ offset + 1 ] );
    }
    else
    {
      return BitConverter.ToInt16( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 2 byte 16 bit unsigned integer (ushort or UInt16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 2 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted UInt16 or ushort</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ushort UInt16FromBigEndianBuffer( byte[] buffer, int offset )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + 2 > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( buffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return (ushort)( ( buffer[ offset ] << 8 ) | buffer[ offset + 1 ] );
    }
    else
    {
      return BitConverter.ToUInt16( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 4 byte 32 bit signed integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 4 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static int Int32FromBigEndianBuffer( byte[] buffer, int offset )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + 4 > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( buffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return ( ( buffer[ offset ] << 24 ) | buffer[ offset + 1 ] << 16 | buffer[ offset + 2 ] << 8 | buffer[ offset + 3 ] );
    }
    else
    {
      return BitConverter.ToInt32( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 4 byte 32 bit unsigned integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 4 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted unsigned integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static uint UInt32FromBigEndianBuffer( byte[] buffer, int offset )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + 4 > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( buffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return (uint)(
          buffer[ offset ] << 24
        | buffer[ offset + 1 ] << 16
        | buffer[ offset + 2 ] << 8
        | buffer[ offset + 3 ]
      );
    }
    else
    {
      return BitConverter.ToUInt32( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 8 byte 64 bit signed integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static long Int64FromBigEndianBuffer( byte[] buffer, int offset )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + 8 > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( buffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return (
          buffer[ offset ] << 56
        | buffer[ offset + 1 ] << 48
        | buffer[ offset + 2 ] << 40
        | buffer[ offset + 3 ] << 32
        | buffer[ offset + 4 ] << 24
        | buffer[ offset + 5 ] << 16 
        | buffer[ offset + 6 ] << 8 
        | buffer[ offset + 7 ]
      );
    }
    else
    {
      return BitConverter.ToInt64( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 8 byte 64 bit unsigned integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ulong UInt64FromBigEndianBuffer( byte[] buffer, int offset )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + 8 > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( buffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return (ulong)(
          buffer[ offset ] << 56
        | buffer[ offset + 1 ] << 48
        | buffer[ offset + 2 ] << 40
        | buffer[ offset + 3 ] << 32
        | buffer[ offset + 4 ] << 24
        | buffer[ offset + 5 ] << 16 
        | buffer[ offset + 6 ] << 8 
        | buffer[ offset + 7 ]
      );
    }
    else
    {
      return BitConverter.ToUInt64( buffer, offset );
    }
  }
}
