// -----------------------------------------------------------------------
// <copyright file="ImageInfo.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo;

using Coderanger.ImageInfo.Decoders;
using Coderanger.ImageInfo.Decoders.Exif.Types;
using Coderanger.ImageInfo.Exceptions;

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
  /// // Create a Workbook
  /// using var imageStream = new FileStream( "image.jpeg", FileMode.Open, FileAccess.Read );
  /// var imageInfo = ImageInfo.DecodeFromStream( imageStream );
  /// 
  /// // If imageInfo was decodable, an object is returned with metadata properties
  /// Debug.WriteLine( $"Mime = {imageInfo.MimeType}" );
  /// Debug.WriteLine( $"Width = {imageInfo.Width}" );
  /// Debug.WriteLine( $"Height = {imageInfo.Height}" );
  /// 
  /// // If there is any metedata in the image, the tags property
  /// // will not be null and will contain the info
  /// if( imageInfo.ExifTags != null )
  /// {
  ///   // Iterate the tags and use the Type property to determine its base type if unknown
  ///   // ahead of time
  ///   foreach( var tag in tags.Keys )
  ///   {
  ///     if(tags.TryGetValue( tag, out var exifValue ) && exifValue != null )
  ///     {
  ///       if(exifValue.TryGetValue( out var tagValue ) && tagValue?.Value != null )
  ///       {
  ///         switch(tagValue.Type )
  ///         {
  ///           case ExifType.Byte:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(byte)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.Date:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(DateTime)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.DateTime:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(DateTime)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.Float:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(float)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.Long:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(long)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.ULong:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(ulong)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.Rational:
  ///           case ExifType.URational:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(double)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.Short:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(short)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.UShort:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(ushort)tagValue.Value}" );
  ///             break;
  /// 
  ///           case ExifType.String:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = {(string)tagValue.Value}" );
  ///             break;
  /// 
  ///           default:
  ///             Debug.WriteLine( $"\t{tagValue.TagName} (0x{tag:X}) = unknown value" );
  ///             break;
  ///         }
  ///       }
  ///     }
  ///   }
  /// }
  /// </code>
  /// </example>
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
  /// Dictionary of exif and photo tags, or null if none exist
  /// </summary>
  public Dictionary<ushort, IExifValue>? ExifTags { get; private set; }

  /// <summary>
  /// Dictionary of exif gps tags, or null if none exist
  /// </summary>
  public Dictionary<ushort, IExifValue>? GpsTags { get; private set; }

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
      ExifTags = imageDetails.ExifTags;
      GpsTags = imageDetails.GpsTags;
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
