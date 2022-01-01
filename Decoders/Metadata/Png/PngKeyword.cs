// -----------------------------------------------------------------------
// <copyright file="PngKeyword.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Png;

/// <summary>
/// Officially supported keywords defined as constants
/// https://www.w3.org/TR/PNG/#11keywords
/// </summary>
public class PngKeyword : IMetadataTag
{
  /// <summary>
  /// Image title
  /// </summary>
  [PngKeywordDetails( "Title", "Short (one line) title or caption for image" )]
  public const ushort Title = 0x0001;

  /// <summary>
  /// Image creator
  /// </summary>
  [PngKeywordDetails( "Author", "Name of image's creator" )]
  public const ushort Author = 0x0002;

  /// <summary>
  /// Image description
  /// </summary>
  [PngKeywordDetails( "Description", "Possibly long description of image" )]
  public const ushort Description = 0x0003;

  /// <summary>
  /// Creation Time
  /// </summary>
  [PngKeywordDetails( "Creation Time", "Time of original image creation" )]
  public const ushort CreationTime = 0x0004;

  /// <summary>
  /// Image Warning
  /// </summary>
  [PngKeywordDetails( "Warning", "Warning of nature of content" )]
  public const ushort Warning = 0x0005;

  /// <summary>
  /// Image copyright
  /// </summary>
  [PngKeywordDetails( "Copyright", "Copyright notice" )]
  public const ushort Copyright = 0x0006;

  /// <summary>
  /// Disclaimer
  /// </summary>
  [PngKeywordDetails( "Disclaimer", "Legal disclaimer" )]
  public const ushort Disclaimer = 0x0007;

  /// <summary>
  /// Device Source
  /// </summary>
  [PngKeywordDetails( "Source", "Device used to create the image" )]
  public const ushort Source = 0x0008;

  /// <summary>
  /// Software
  /// </summary>
  [PngKeywordDetails( "Software", "Software used to create the image" )]
  public const ushort Software = 0x0009;

  /// <summary>
  /// Image comments
  /// </summary>
  [PngKeywordDetails( "Comment", "Miscellaneous comment" )]
  public const ushort Comment = 0x000A;

  /// <summary>
  /// Custom keyword, the name will be derived from the keyword given in
  /// the metadata instead of from this placeholder tag
  /// </summary>
  [PngKeywordDetails( "Custom", "Custom or private keyword" )]
  public const ushort Custom = 0x00FF;
}
