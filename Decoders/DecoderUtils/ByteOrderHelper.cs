// -----------------------------------------------------------------------
// <copyright file="ByteOrderHelper.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

using System;

/// <summary>
/// Classifier of data byte order type referenced
/// </summary>
internal enum ByteOrder
{
  Unknown,
  LittleEndian,
  BigEndian,
}

/// <summary>
/// Helper for retrieving byte order from buffer
/// </summary>
internal static class ByteOrderHelper
{
  internal static ByteOrder GetTiffByteOrder( byte[] data )
  {
    if( data.AsSpan( 0, 2 ).SequenceEqual( TiffConstants.ByteOrder.BigEndian ) )
    {
      return ByteOrder.BigEndian;
    }
    else if( data.AsSpan( 0, 2 ).SequenceEqual( TiffConstants.ByteOrder.LittleEndian ) )
    {
      return ByteOrder.LittleEndian;
    }

    throw new ArgumentException( "Unknown byte order in JPEG TIFF header" );
  }
}
