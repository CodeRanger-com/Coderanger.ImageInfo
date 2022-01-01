﻿// -----------------------------------------------------------------------
// <copyright file="DataConversion.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

using Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;
using System.Text;

/// <summary>
/// Buffer conversion helper class
/// </summary>
internal static class DataConversion
{
  /// <summary>
  /// Converts buffer of specific size to a string in given encoding
  /// </summary>
  /// <param name="buffer">Byte array of string data</param>
  /// <param name="count">Count of characters in buffer</param>
  /// <param name="encoding">Encoding type</param>
  /// <returns>Converted string or empty</returns>
  internal static string ConvertBuffer( byte[] buffer, int count, StringEncoding encoding )
  {
    if( buffer?.Length > 0 )
    {
      if( encoding == StringEncoding.Ascii )
      {
        // Buffer will be null terminated so remove any zero bytes from the end
        return Encoding.UTF8.GetString( buffer, 0, count ).TrimEnd( (char)0 );
      }
      else if( encoding == StringEncoding.Ucs2 )
      {
        // Buffer will be null terminated so remove any zero bytes from the end
        return Encoding.GetEncoding( "UCS-2" ).GetString( buffer, 0, count ).TrimEnd( (char)0 );
      }
      else
      {
        // UTF8 is best for unknown encoding, as it decodes ascii as well as dbcs characters
        // Buffer will be null terminated so remove any zero bytes from the end
        return Encoding.UTF8.GetString( buffer, 0, count ).TrimEnd( (char)0 );
      }
    }

    return string.Empty;
  }

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
  /// Convert data from buffer determined by the buffer order, into a 4 byte
  /// signed float (Single)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 4 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static float FloatFromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return FloatFromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToSingle( buffer, offset );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 8 byte
  /// signed double
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static double DoubleFromBuffer( byte[] buffer, int offset, TiffByteOrder bufferOrder )
  {
    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return DoubleFromBigEndianBuffer( buffer, offset );
    }
    else
    {
      return BitConverter.ToDouble( buffer, offset );
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
  internal static short Int16FromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( short ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( Int16FromBigEndianBuffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      var reversed = ReverseBytes( buffer, sizeof( short ), offset );
      return BitConverter.ToInt16( reversed );
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
  internal static ushort UInt16FromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( ushort ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( UInt16FromBigEndianBuffer ) );
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
  internal static int Int32FromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( int ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( Int32FromBigEndianBuffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      var reversed = ReverseBytes( buffer, sizeof( int ), offset );
      return BitConverter.ToInt32( reversed );
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
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static uint UInt32FromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( uint ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( UInt32FromBigEndianBuffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return (uint)( ( buffer[ offset ] << 24 ) | buffer[ offset + 1 ] << 16 | buffer[ offset + 2 ] << 8 | buffer[ offset + 3 ] );
    }
    else
    {
      return BitConverter.ToUInt32( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 4 byte signed float
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 4 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static float FloatFromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( float ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( FloatFromBigEndianBuffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      var reversed = ReverseBytes( buffer, sizeof( float ), offset );
      return BitConverter.ToSingle( reversed );
    }
    else
    {
      return BitConverter.ToSingle( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to an 8 byte signed double
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static double DoubleFromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( double ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( DoubleFromBigEndianBuffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      var reversed = ReverseBytes( buffer, sizeof( double ), offset );
      return BitConverter.ToDouble( reversed );
    }
    else
    {
      return BitConverter.ToDouble( buffer, offset );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 8 byte 64 bit signed integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes bigger than the offset given</param>
  /// <param name="offset">Offset from start of buffer to start conversion</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static long Int64FromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( long ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( Int64FromBigEndianBuffer ) );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      var reversed = ReverseBytes( buffer, sizeof( long ), offset );
      return BitConverter.ToInt64( reversed );
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
  internal static ulong UInt64FromBigEndianBuffer( byte[] buffer, int offset = 0 )
  {
    // Ensure no conversion beyond buffer bounds
    if( offset + sizeof( ulong ) > buffer.Length )
    {
      throw new ArgumentException( "Invalid buffer size and offset passed", nameof( UInt64FromBigEndianBuffer ) );
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

  private static byte[] ReverseBytes( byte[] buffer, int size, int offset = 0 )
  {
    var reversed = new byte[ size ];
    for( int i = 0; i < reversed.Length; i++ )
    {
      reversed[ reversed.Length - 1 - i ] = buffer[ offset + i ];
    }
    return reversed;
  }
}
