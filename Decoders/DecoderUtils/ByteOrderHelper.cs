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
  internal static ByteOrder GetTiffByteOrder( ReadOnlySpan<byte> dataSpan )
  {
    return DataConversion.UInt16FromBigEndianBuffer( dataSpan ) switch
    {
      TiffConstants.ByteOrder.BigEndianValue => ByteOrder.BigEndian,
      TiffConstants.ByteOrder.LittleEndianValue => ByteOrder.LittleEndian,
      _ => throw new ArgumentException( "Unknown byte order" ),
    };
  }
}
