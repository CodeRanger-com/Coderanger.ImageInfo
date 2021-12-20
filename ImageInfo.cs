// -----------------------------------------------------------------------
// <copyright file="ImageInfo.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders;

public sealed class ImageInfo
{
  /// <summary>
  /// Initiate an instance of the class with the given stream for decoding
  /// </summary>
  /// <param name="image">Stream of data which may or may not contain a known image</param>
  public ImageInfo( Stream image )
  {
    _image = image;
  }

  /// <summary>
  /// Decode a given image stream
  /// </summary>
  /// <remarks>
  /// The stream is left open and its position
  /// will be wherever the decoder left it
  /// </remarks>
  /// <returns>Null or a valid ImageDetails object</returns>
  public ImageDetails? Decode()
  {
    // Dont dispose the BinaryReader wrapper as it will close the underlying stream
    var reader = new BinaryReader( _image );

    var decoder = FindDecoder( reader );
    if( decoder != null )
    {
      return decoder.Decode( reader );
    }

    return null;
  }

  /// <summary>
  /// Locates the correct decoder for this image
  /// </summary>
  /// <returns></returns>
  private static IDecoder? FindDecoder( BinaryReader reader )
  {
    foreach( var formatDetector in FormatManager.Get() )
    {
      reader.BaseStream.Position = 0;
      var decoder = formatDetector.DetectFormat( reader );
      if( decoder != null )
      {
        return decoder;
      }
    }

    return null;
  }

  private readonly Stream _image;
  internal readonly static FormatManager FormatManager = new();
}
