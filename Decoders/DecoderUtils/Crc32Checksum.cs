// -----------------------------------------------------------------------
// <copyright file="Crc32Checksum.cs"></copyright>
// <comment>
// Code taken from Mike Marynowski on StackOverflow:
// https://stackoverflow.com/a/53018253/14608904
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

internal static partial class Crc32Checksum
{
  /// <summary>
  /// Calculates a CRC32 value for the data given.
  /// </summary>
  /// <param name="data">Data contents</param>
  /// <param name="offset">Byte offset to start reading</param>
  /// <param name="count">Number of bytes to read</param>
  /// <returns>The computed CRC32 value.</returns>
  internal static int Calculate( byte[] data, int offset, int count )
      => Calculate( 0, data, offset, count );

  /// <summary>
  /// Calculates a new CRC32 value given additional data for the current CRC value.
  /// </summary>
  /// <param name="currentCrc">The current CRC value to start with</param>
  /// <param name="data">Additional data contents</param>
  /// <param name="offset">Byte offset to start reading</param>
  /// <param name="count">Number of bytes to read</param>
  /// <returns>The computed CRC32 value.</returns>
  internal static int Calculate( int currentCrc, byte[] data, int offset, int count )
  {
    unchecked
    {
      uint crc = ~(uint)currentCrc;

      for( int i = offset, end = offset + count; i < end; i++ )
        crc = ( crc >> 8 ) ^ CrcTable[ ( crc ^ data[ i ] ) & 0xFF ];

      return (int)~crc;
    }
  }
}
