// -----------------------------------------------------------------------
// <copyright file="TiffByteOrderHelper.cs" company="CodeRanger.com">
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
internal enum TiffByteOrder
{
  Unknown,
  LittleEndian,
  BigEndian,
}

/// <summary>
/// Helper for retrieving byte order from buffer
/// </summary>
internal static class TiffByteOrderHelper
{
  internal static TiffByteOrder GetTiffByteOrder( ReadOnlySpan<byte> dataSpan )
  {
    if( dataSpan.SequenceEqual( TiffConstants.ByteOrder.BigEndian ) )
    {
      return TiffByteOrder.BigEndian;
    }
    else if( dataSpan.SequenceEqual( TiffConstants.ByteOrder.LittleEndian ) )
    {
      return TiffByteOrder.LittleEndian;
    }

    throw new ArgumentException( "Unknown byte order in JPEG TIFF header" );
  }
}
