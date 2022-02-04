// -----------------------------------------------------------------------
// <copyright file="ImageInfo.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders;
using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata;
using Coderanger.ImageInfo.Exceptions;

/// <summary>
/// Helper class with methods to decode an image or stream
/// </summary>
public sealed class ImageInfo
{
  /// <summary>
  /// Decode a given image stream
  /// </summary>
  /// <remarks>
  /// The stream is left open and its position will be wherever the
  /// decoder left it; more likely at the end in most cases
  /// </remarks>
  /// <returns>Null or a valid ImageInfo object</returns>
  /// <example>
  /// <code language="cs">
  /// using var imageStream = new FileStream( "image.jpeg", FileMode.Open, FileAccess.Read );
  /// var imageInfo = ImageInfo.Get( imageStream );
  /// 
  /// // If imageInfo was decodable, an object is returned with metadata properties
  /// Debug.WriteLine( $"Mime = {imageInfo.MimeType}" );
  /// Debug.WriteLine( $"Width = {imageInfo.Width}" );
  /// Debug.WriteLine( $"Height = {imageInfo.Height}" );
  /// 
  /// // If there is any metedata in the image, the tags property
  /// // will not be null and will contain the info as a dictionary
  /// // of profile tag lists
  /// 
  /// </code>
  /// </example>
  /// <exception cref="ImageStreamException">Thrown if image is not readable or seekable</exception>
  public static ImageInfo Get( Stream image )
  {
    if( !image.CanSeek || !image.CanRead )
    {
      throw new ImageStreamException( "Image stream must be seekable and readbable" );
    }

    var imageInfo = new ImageInfo( image );
    imageInfo.Decode();
    return imageInfo;
  }

  /// <summary>
  /// Width of image in pixels
  /// </summary>
  public long Width { get; private set; }

  /// <summary>
  /// Height of image in pixels
  /// </summary>
  public long Height { get; private set; }

  /// <summary>
  /// Resolution in Dots per Inch
  /// </summary>
  public int HorizontalDpi { get; private set; }

  /// <summary>
  /// Resolution in Dots per Inch
  /// </summary>
  public int VerticalDpi { get; private set; }

  /// <summary>
  /// Mime type as 'image/type'
  /// </summary>
  public string MimeType { get; private set; } = string.Empty;

  /// <summary>
  /// Dictionary of profiles to list of exif, gps and photo tags
  /// </summary>
  public Dictionary<MetadataProfileType, List<IMetadataTypedValue>>? Metadata { get; private set; }

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
      Metadata = imageDetails.Metadata;
    }
  }

  /// <summary>
  /// Locates the correct decoder for this image
  /// </summary>
  /// <returns></returns>
  private static IDecoder? FindDecoder( BinaryReader reader )
  {
    foreach( var formatDetectorDelegate in FormatManager.Get() )
    {
      reader.Position( 0 );
      var decoder = formatDetectorDelegate( reader );
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
