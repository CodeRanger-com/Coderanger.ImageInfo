// -----------------------------------------------------------------------
// <copyright file="ImageInfo.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders;
using Coderanger.ImageInfo.Decoders.Exif;
using Coderanger.ImageInfo.Decoders.Exif.Types;
using Coderanger.ImageInfo.Exceptions;

public sealed class ImageInfo
{
  /// <summary>
  /// Decode a given image stream
  /// </summary>
  /// <remarks>
  /// The stream is left open and its position will be wherever the
  /// decoder left it
  /// </remarks>
  /// <returns>Null or a valid ImageInfo object</returns>
  /// <exception cref="ImageStreamException">Thrown if image is not readable or seekable</exception>
  public static ImageInfo DecodeFromStream( Stream image )
  {
    if( !image.CanSeek || !image.CanRead )
    {
      throw new ImageStreamException( "Image stream must be seekable and readbable" );
    }

    var imageInfo = new ImageInfo( image );
    imageInfo.Decode();
    return imageInfo;
  }

  public long Width { get; private set; }
  public long Height { get; private set; }
  public int HorizontalDpi { get; private set; }
  public int VerticalDpi { get; private set; }
  public string MimeType { get; private set; } = string.Empty;
  public Dictionary<ushort, ExifValue> ExifTags { get; private set; } = new();

  /// <summary>
  /// Initiate an instance of the class with the given stream for decoding
  /// </summary>
  /// <param name="image">Stream of data which may or may not contain a known image</param>
  private ImageInfo( Stream image )
  {
    _image = image;
  }

  /// <summary>
  /// Decode the image stream
  /// </summary>
  /// <exception cref="ImageDecoderException">Thrown if image is not readable or seekable, or could not be decoded</exception>
  private void Decode()
  {
    // Dont dispose the BinaryReader wrapper as it will close the underlying stream
    var reader = new BinaryReader( _image );

    var decoder = FindDecoder( reader );
    if( decoder != null )
    {
      var imageDetails = decoder.Decode( reader );
      if( imageDetails == null )
      {
        throw new ImageDecoderException( "Image stream could not be decoded" );
      }

      Width = imageDetails.Width;
      Height = imageDetails.Height;
      HorizontalDpi = imageDetails.HorizontalResolution;
      VerticalDpi = imageDetails.VerticalResolution;
      MimeType = imageDetails.MimeType;
      ExifTags.Clear();
      if( imageDetails.Tags.Count > 0 )
      {
        ExifTags = imageDetails.Tags;
      }
    }
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
