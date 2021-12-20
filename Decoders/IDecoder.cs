// -----------------------------------------------------------------------
// <copyright file="IDecoder.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders;

/// <summary>
/// Decoding interface for any image decoders to derive from
/// </summary>
internal interface IDecoder
{
  /// <summary>
  /// Determines if the stream passed can be decoded by this decoder
  /// </summary>
  /// <param name="reader">Wrapper around a readable stream</param>
  /// <returns>This decoder if it can be used to decode the stream</returns>
  IDecoder? DetectFormat( BinaryReader reader );

  /// <summary>
  /// Decodes the stream passed and returns an ImageDetails object
  /// </summary>
  /// <param name="reader">Wrapper around a readable stream</param>
  /// <returns>An ImageDetails containing image information retrieved from the decoded stream, or an exception is thrown</returns>
  /// <exception cref="ArgumentException">Throws if any part of the stream is invalid for this image format</exception>
  ImageDetails Decode( BinaryReader reader );
}
