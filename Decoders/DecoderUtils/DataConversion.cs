// -----------------------------------------------------------------------
// <copyright file="DataConversion.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

using System;
using System.Text;

/// <summary>
/// Buffer conversion helper class
/// </summary>
internal static class DataConversion
{
  /// <summary>
  /// Converts buffer span to a string in given encoding
  /// </summary>
  /// <param name="buffer">Span of string data bytes</param>
  /// <param name="encoding">Encoding type</param>
  /// <returns>Converted string or empty</returns>
  internal static string ConvertBuffer( ReadOnlySpan<byte> buffer, StringEncoding encoding )
  {
    if( buffer.Length > 0 )
    {
      // Buffer will be null terminated so remove any zero bytes from the end
      if( encoding == StringEncoding.Ascii )
      {
        return Encoding.UTF8.GetString( buffer ).TrimEnd( (char)0 ).Trim();
      }
      else if( encoding == StringEncoding.Unicode )
      {
        return Encoding.Unicode.GetString( buffer ).TrimEnd( (char)0 ).Trim();
      }
      else if( encoding == StringEncoding.Ucs2 )
      {
        return Encoding.GetEncoding( "UCS-2" ).GetString( buffer ).TrimEnd( (char)0 ).Trim();
      }
      else
      {
        // UTF8 is best for unknown encoding, as it decodes ascii as well as dbcs characters
        return Encoding.UTF8.GetString( buffer ).TrimEnd( (char)0 ).Trim();
      }
    }

    return string.Empty;
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 2 byte 16bit
  /// signed integer (short or Int16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is 2 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static short Int16FromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( short ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int16FromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return Int16FromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToInt16( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 2 byte 16bit
  /// unsigned integer (ushort or UInt16)
  /// </summary>
  /// <param name="buffer">Buffer span whose size is 2 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted UInt16 or ushort</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ushort UInt16FromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( ushort ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( UInt16FromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return UInt16FromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToUInt16( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 4 byte 32bit
  /// signed integer (Int32)
  /// </summary>
  /// <param name="buffer">Buffer whose size is 4 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int32 or int</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static int Int32FromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( int ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int32FromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return Int32FromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToInt32( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 4 byte 32bit
  /// unsigned integer (UInt32)
  /// </summary>
  /// <param name="buffer">Buffer whose size is 4 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted UInt32 or uint</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static uint UInt32FromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( int ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( UInt32FromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return UInt32FromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToUInt32( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 8 byte
  /// signed float (Single)
  /// </summary>
  /// <param name="buffer">Buffer whose size is 8 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Single or float</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static float FloatFromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( float ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( FloatFromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return FloatFromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToSingle( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into a 8 byte
  /// signed double
  /// </summary>
  /// <param name="buffer">Buffer whose size is 8 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted double</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static double DoubleFromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( double ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( DoubleFromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return DoubleFromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToDouble( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into an 8 byte 64bit
  /// signed long (Int64)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted Int64 or long</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static long Int64FromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( long ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int64FromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return Int64FromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToInt64( buffer );
    }
  }

  /// <summary>
  /// Convert data from buffer determined by the buffer order, into an 8 byte 64bit
  /// unsigned long (UInt64)
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes</param>
  /// <param name="bufferOrder">The byte order of the data in the buffer</param>
  /// <returns>Converted UInt64 or ulong</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ulong UInt64FromBuffer( ReadOnlySpan<byte> buffer, TiffByteOrder bufferOrder )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( ulong ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int64FromBuffer )}" );
    }

    if( bufferOrder == TiffByteOrder.BigEndian )
    {
      return UInt64FromBigEndianBuffer( buffer );
    }
    else
    {
      return BitConverter.ToUInt64( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 2 byte 16 bit signed integer (short or Int16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is 2 bytes</param>
  /// <returns>Converted Int16 or short</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static short Int16FromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( short ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int16FromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      //var span = MemoryMarshal.CreateSpan( ref MemoryMarshal.GetReference( buffer ), buffer.Length );
      //span.Reverse();
      //return BitConverter.ToInt16( span );

      return BitConverter.ToInt16( ReverseBytes( buffer.ToArray(), sizeof( short ) ), 0 );
    }
    else
    {
      return BitConverter.ToInt16( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 2 byte 16 bit unsigned integer (ushort or UInt16)
  /// </summary>
  /// <param name="buffer">Buffer whose size is 2 bytes</param>
  /// <returns>Converted UInt16 or ushort</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ushort UInt16FromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( ushort ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( UInt16FromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      //var span = MemoryMarshal.CreateSpan( ref MemoryMarshal.GetReference( buffer ), buffer.Length );
      //span.Reverse();
      //return BitConverter.ToUInt16( span );

      return BitConverter.ToUInt16( ReverseBytes( buffer.ToArray(), sizeof( ushort ) ), 0 );

      // Reverse the bytes
      //return (ushort)( ( buffer[ 1 ] << 8 ) | buffer[ 0 ] );
    }
    else
    {
      return BitConverter.ToUInt16( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 4 byte 32 bit signed integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is 4 bytes</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static int Int32FromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( int ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int32FromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      //var span = MemoryMarshal.CreateSpan( ref MemoryMarshal.GetReference( buffer ), buffer.Length );
      //span.Reverse();
      //return BitConverter.ToInt32( span );

      return BitConverter.ToInt32( ReverseBytes( buffer.ToArray(), sizeof( int ) ), 0 );
    }
    else
    {
      return BitConverter.ToInt32( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 4 byte 32 bit unsigned integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is 4 bytes</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static uint UInt32FromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( uint ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( UInt32FromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      //var span = MemoryMarshal.CreateSpan( ref MemoryMarshal.GetReference( buffer ), buffer.Length );
      //span.Reverse();
      //return BitConverter.ToUInt32( span );

      return BitConverter.ToUInt32( ReverseBytes( buffer.ToArray(), sizeof( uint ) ), 0 );
    }
    else
    {
      return BitConverter.ToUInt32( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 4 byte signed float
  /// </summary>
  /// <param name="buffer">Buffer whose size is 4 bytes</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static float FloatFromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( buffer.Length < sizeof( float ) )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( FloatFromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      //var span = MemoryMarshal.CreateSpan( ref MemoryMarshal.GetReference( buffer ), buffer.Length );
      //span.Reverse();
      //return BitConverter.ToSingle( span );

      return BitConverter.ToSingle( ReverseBytes( buffer.ToArray(), sizeof( float ) ), 0 );
    }
    else
    {
      return BitConverter.ToSingle( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to an 8 byte signed double
  /// </summary>
  /// <param name="buffer">Buffer whose size is 8 bytes</param>
  /// <returns>Converted signed integer</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static double DoubleFromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( sizeof( double ) > buffer.Length )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( DoubleFromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      //var span = MemoryMarshal.CreateSpan( ref MemoryMarshal.GetReference( buffer ), buffer.Length );
      //span.Reverse();
      //return BitConverter.ToDouble( span );

      return BitConverter.ToDouble( ReverseBytes( buffer.ToArray(), sizeof( double ) ), 0 );
    }
    else
    {
      return BitConverter.ToDouble( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 8 byte 64 bit signed integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes</param>
  /// <returns>Converted signed Int64</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static long Int64FromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( sizeof( long ) > buffer.Length )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( Int64FromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes and convert to handle signed value
      return BitConverter.ToInt64( ReverseBytes( buffer.ToArray(), sizeof( long ) ), 0 );
    }
    else
    {
      return BitConverter.ToInt64( buffer );
    }
  }

  /// <summary>
  /// Convert a big endian buffer to a 8 byte 64 bit unsigned integer
  /// </summary>
  /// <param name="buffer">Buffer whose size is at least 8 bytes</param>
  /// <returns>Converted unsigned UInt64</returns>
  /// <exception cref="ArgumentException">Thrown if buffer is not big enough</exception>
  internal static ulong UInt64FromBigEndianBuffer( ReadOnlySpan<byte> buffer )
  {
    // Ensure no conversion beyond buffer bounds
    if( sizeof( ulong ) > buffer.Length )
    {
      throw new ArgumentException( $"Invalid buffer size passed to {nameof( UInt64FromBigEndianBuffer )}" );
    }

    if( BitConverter.IsLittleEndian )
    {
      // Reverse the bytes
      return BitConverter.ToUInt64( ReverseBytes( buffer.ToArray(), sizeof( ulong ) ), 0 );
    }
    else
    {
      return BitConverter.ToUInt64( buffer );
    }
  }

  private static byte[] ReverseBytes( byte[] buffer, int size )
  {
    var reversed = new byte[ size ];
    for( int i = 0; i < reversed.Length; i++ )
    {
      reversed[ reversed.Length - 1 - i ] = buffer[ i ];
    }
    return reversed;
  }
}
