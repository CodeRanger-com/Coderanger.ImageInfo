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
    return DataConversion.UInt16FromBigEndianBuffer( dataSpan ) switch
    {
      TiffConstants.ByteOrder.BigEndianValue => TiffByteOrder.BigEndian,
      TiffConstants.ByteOrder.LittleEndianValue => TiffByteOrder.LittleEndian,
      _ => throw new ArgumentException( "Unknown byte order in JPEG TIFF header" ),
    };
  }
}
