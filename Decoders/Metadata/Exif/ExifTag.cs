// -----------------------------------------------------------------------
// <copyright file="ExifTag.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif;

/// <summary>
/// Tag constants
/// https://www.exiv2.org/tags.html
/// </summary>
public class ExifTag : IMetadataTag
{
  /// <summary>
  /// A pointer to the Exif IFD. Exif IFD has the same structure as that of the IFD specified
  /// in TIFF. ordinarily; however; it does not contain image data as in the case of TIFF
  /// </summary>
  public const ushort ExifIfd = 0x8769;

  /// <summary>
  /// A pointer to the GPS Info IFD. The Interoperability structure of the GPS Info IFD;
  /// like that of Exif IFD; has no image data
  /// </summary>
  public const ushort GPSIfd = 0x8825;

  /// <summary>
  /// Interoperability IFD is composed of tags which stores the information to ensure the
  /// Interoperability and pointed by the following tag located in Exif IFD. The Interoperability
  /// structure of Interoperability IFD is the same as TIFF defined IFD structure but does
  /// not contain the image data characteristically compared with normal TIFF IFD
  /// </summary>
  public const ushort PhotoInteroperabilityTag = 0xa005;

  /// <summary>
  /// Defined by Adobe Corporation to enable TIFF Trees within a TIFF file.
  /// </summary>
  public const ushort SubIFDs = 0x014a;



  [MetadataTagDetails( "Processing Software", "Name and version of the software used to post-process the picture" )]
  public const ushort ProcessingSoftware = 0x000b;

  [MetadataTagEnum( 0, "Full-resolution image" )]
  [MetadataTagEnum( 1, "Reduced-resolution image" )]
  [MetadataTagEnum( 2, "Single page of multi-page image" )]
  [MetadataTagEnum( 3, "Single page of multi-page reduced-resolution image" )]
  [MetadataTagEnum( 4, "Transparency mask" )]
  [MetadataTagEnum( 5, "Transparency mask of reduced-resolution image" )]
  [MetadataTagEnum( 6, "Transparency mask of multi-page image" )]
  [MetadataTagEnum( 7, "Transparency mask of reduced-resolution multi-page image" )]
  [MetadataTagEnum( 8, "Depth map" )]
  [MetadataTagEnum( 9, "Depth map of reduced-resolution image" )]
  [MetadataTagEnum( 16, "Enhanced image data" )]
  [MetadataTagEnum( 65537, "Alternate reduced-resolution image" )]
  [MetadataTagEnum( 65540, "Semantic Mask" )]
  [MetadataTagEnum( 4294967295, "Invalid" )]
  [MetadataTagDetails( "Subfile Type", "General indication of the kind of data contained in this subfile" )]
  public const ushort SubfileType = 0x00fe;

  [MetadataTagEnum( 1, "Full-resolution image" )]
  [MetadataTagEnum( 2, "Reduced-resolution image" )]
  [MetadataTagEnum( 3, "Single page of multi-page image" )]
  [MetadataTagDetails( "Old Subfile Type", "General indication of the kind of data contained in this subfile" )]
  public const ushort OldSubfileType = 0x00ff;

  [MetadataTagDetails( "Image Width", "Number of columns of image data which might not reflect its true size. In JPEG compressed data a JPEG marker is used instead of this tag" )]
  public const ushort ImageWidth = 0x0100;

  [MetadataTagDetails( "Image Height", "Number of rows of image data which might not reflect its true size. In JPEG compressed data a JPEG marker is used instead of this tag" )]
  public const ushort ImageHeight = 0x0101;

  [MetadataTagDetails( "Bits Per Sample", "Number of bits per image component" )]
  public const ushort BitsPerSample = 0x0102;

  [MetadataTagEnum( 1, "Uncompressed" )]
  [MetadataTagEnum( 2, "CCITT 1D" )]
  [MetadataTagEnum( 3, "T4/Group 3 Fax" )]
  [MetadataTagEnum( 4, "T6/Group 4 Fax" )]
  [MetadataTagEnum( 5, "LZW" )]
  [MetadataTagEnum( 6, "JPEG (old-style)" )]
  [MetadataTagEnum( 7, "JPEG" )]
  [MetadataTagEnum( 8, "Adobe Deflate" )]
  [MetadataTagEnum( 9, "JBIG B&W" )]
  [MetadataTagEnum( 10, "JBIG Color" )]
  [MetadataTagEnum( 99, "JPEG" )]
  [MetadataTagEnum( 262, "Kodak 262" )]
  [MetadataTagEnum( 32766, "Next" )]
  [MetadataTagEnum( 32767, "Sony ARW Compressed" )]
  [MetadataTagEnum( 32769, "Packed RAW" )]
  [MetadataTagEnum( 32770, "Samsung SRW Compressed" )]
  [MetadataTagEnum( 32771, "CCIRLEW" )]
  [MetadataTagEnum( 32772, "Samsung SRW Compressed 2" )]
  [MetadataTagEnum( 32773, "PackBits" )]
  [MetadataTagEnum( 32809, "Thunderscan" )]
  [MetadataTagEnum( 32867, "Kodak KDC Compressed" )]
  [MetadataTagEnum( 32895, "IT8CTPAD" )]
  [MetadataTagEnum( 32896, "IT8LW" )]
  [MetadataTagEnum( 32897, "IT8MP" )]
  [MetadataTagEnum( 32898, "IT8BL" )]
  [MetadataTagEnum( 32908, "PixarFilm" )]
  [MetadataTagEnum( 32909, "PixarLog" )]
  [MetadataTagEnum( 32946, "Deflate" )]
  [MetadataTagEnum( 32947, "DCS" )]
  [MetadataTagEnum( 33003, "Aperio JPEG 2000 YCbCr" )]
  [MetadataTagEnum( 33005, "Aperio JPEG 2000 RGB" )]
  [MetadataTagEnum( 34661, "JBIG" )]
  [MetadataTagEnum( 34676, "SGILog" )]
  [MetadataTagEnum( 34677, "SGILog24" )]
  [MetadataTagEnum( 34712, "JPEG 2000" )]
  [MetadataTagEnum( 34713, "Nikon NEF Compressed" )]
  [MetadataTagEnum( 34715, "JBIG2 TIFF FX" )]
  [MetadataTagEnum( 34718, "Microsoft Document Imaging (MDI) Binary Level Codec" )]
  [MetadataTagEnum( 34719, "Microsoft Document Imaging (MDI) Progressive Transform Codec" )]
  [MetadataTagEnum( 34720, "Microsoft Document Imaging (MDI) Vector" )]
  [MetadataTagEnum( 34887, "ESRI Lerc" )]
  [MetadataTagEnum( 34892, "Lossy JPEG" )]
  [MetadataTagEnum( 34925, "LZMA2" )]
  [MetadataTagEnum( 34926, "Zstd" )]
  [MetadataTagEnum( 34927, "WebP" )]
  [MetadataTagEnum( 34933, "PNG" )]
  [MetadataTagEnum( 34934, "JPEG XR" )]
  [MetadataTagEnum( 65000, "Kodak DCR Compressed" )]
  [MetadataTagEnum( 65535, "Pentax PEF Compressed" )]
  [MetadataTagDetails( "Compression", "Compression scheme used for the image data" )]
  public const ushort Compression = 0x0103;

  [MetadataTagEnum( 0, "WhiteIsZero" )]
  [MetadataTagEnum( 1, "BlackIsZero" )]
  [MetadataTagEnum( 2, "RGB" )]
  [MetadataTagEnum( 3, "RGB Palette" )]
  [MetadataTagEnum( 4, "Transparency Mask" )]
  [MetadataTagEnum( 5, "CMYK" )]
  [MetadataTagEnum( 6, "YCbCr" )]
  [MetadataTagEnum( 8, "CIELab" )]
  [MetadataTagEnum( 9, "ICCLab" )]
  [MetadataTagEnum( 10, "ITULab" )]
  [MetadataTagEnum( 32803, "Color Filter Array" )]
  [MetadataTagEnum( 32844, "Pixar LogL" )]
  [MetadataTagEnum( 32845, "Pixar LogLuv" )]
  [MetadataTagEnum( 32892, "Sequential Color Filter" )]
  [MetadataTagEnum( 34892, "Linear Raw" )]
  [MetadataTagEnum( 51177, "Depth Map" )]
  [MetadataTagEnum( 52527, "Semantic Mask" )]
  [MetadataTagDetails( "Photometric Interpretation", "The pixel composition" )]
  public const ushort PhotometricInterpretation = 0x0106;

  [MetadataTagEnum( 1, "No dithering or halftoning" )]
  [MetadataTagEnum( 2, "Ordered dither or halftone" )]
  [MetadataTagEnum( 3, "Randomized dither" )]
  [MetadataTagDetails( "Thresholding", "For black and white TIFF files that represent shades of gray, the technique used to convert from gray to black and white pixels" )]
  public const ushort Thresholding = 0x0107;

  [MetadataTagDetails( "Cell Width", "Width of the dithering or halftoning matrix used to create a dithered or halftoned bilevel file" )]
  public const ushort CellWidth = 0x0108;

  [MetadataTagDetails( "Cell Length", "Length of the dithering or halftoning matrix used to create a dithered or halftoned bilevel file" )]
  public const ushort CellLength = 0x0109;

  [MetadataTagEnum( 1, "Normal" )]
  [MetadataTagEnum( 2, "Reversed" )]
  [MetadataTagDetails( "Fill Order", "Logical order of bits within a byte" )]
  public const ushort FillOrder = 0x010a;

  [MetadataTagDetails( "Document Name", "Name of the document from which this image was scanned" )]
  public const ushort DocumentName = 0x010d;

  [MetadataTagDetails( "Image Description", "Character string giving the title of the image. It may be a comment such as '1988 company picnic' for example. Two-bytes character codes cannot be used; instead, the Exif tag 'UserComment' is to be used" )]
  public const ushort ImageDescription = 0x010e;

  [MetadataTagDetails( "Camera Make", "Manufacturer of the digital still camera (DSC), scanner, video digitizer or other equipment that generated the image. When this is left blank, it is treated as unknown" )]
  public const ushort Make = 0x010f;

  [MetadataTagDetails( "Camera Model", "Model name or number of the digital still camera (DSC), scanner, video digitizer or other equipment that generated the image. When this is left blank, it is treated as unknown" )]
  public const ushort Model = 0x0110;

  [MetadataTagDetails( "Strip Offsets", "For each strip, the byte offset of that strip" )]
  public const ushort StripOffsets = 0x0111;

  [MetadataTagEnum( 1, "Horizontal (normal)" )]
  [MetadataTagEnum( 2, "Mirror horizontal" )]
  [MetadataTagEnum( 3, "Rotate 180" )]
  [MetadataTagEnum( 4, "Mirror vertical" )]
  [MetadataTagEnum( 5, "Mirror horizontal and rotate 270 CW" )]
  [MetadataTagEnum( 6, "Rotate 90 CW" )]
  [MetadataTagEnum( 7, "Mirror horizontal and rotate 90 CW" )]
  [MetadataTagEnum( 8, "Rotate 270 CW" )]
  public const ushort Orientation = 0x0112;

  [MetadataTagDetails( "Samples Per Pixel", "Number of components per pixel; since this standard applies to RGB and YCbCr images, the value set for this tag is 3" )]
  public const ushort SamplesPerPixel = 0x0115;

  [MetadataTagDetails( "Rows Per Strip", "Number of rows in the image per strip when an image is divided into strips" )]
  public const ushort RowsPerStrip = 0x0116;

  [MetadataTagDetails( "Strip Byte Counts", "Total number of bytes in each strip" )]
  public const ushort StripByteCounts = 0x0117;

  [MetadataTagDetails( "Horizontal Resolution", "Number of pixels per ResolutionUnit" )]
  public const ushort XResolution = 0x011a;

  [MetadataTagDetails( "Vertical Resolution", "Number of pixels per ResolutionUnit" )]
  public const ushort YResolution = 0x011b;

  [MetadataTagEnum( 1, "Chunky" )]
  [MetadataTagEnum( 2, "Planar" )]
  [MetadataTagDetails( "Planar Configuration", "Indicates whether pixel components are recorded in a chunky or planar format" )]
  public const ushort PlanarConfiguration = 0x011c;

  [MetadataTagEnum( 1, "0.1" )]
  [MetadataTagEnum( 2, "0.001" )]
  [MetadataTagEnum( 3, "0.0001" )]
  [MetadataTagEnum( 4, "1e-05" )]
  [MetadataTagEnum( 5, "1e-06" )]
  [MetadataTagDetails( "Gray Response Unit", "Precision of the information contained in the GrayResponseCurve" )]
  public const ushort GrayResponseUnit = 0x0122;

  [MetadataTagDetails( "Gray Response Curve", "The optical density of each possible pixel value for grayscale data" )]
  public const ushort GrayResponseCurve = 0x0123;

  [MetadataTagDetails( "T4 Options", "T.4 encoding options; Bit '0' denotes 2-Dimensional encoding, Bit '1' denotes Uncompressed, Bit '2' denotes Fill bits added" )]
  public const ushort T4Options = 0x0124;

  [MetadataTagDetails( "T6 Options", "T.6 encoding options; Bit '1' denotes Uncompressed" )]
  public const ushort T6Options = 0x0125;

  /// <summary>
  /// The unit for measuring <XResolution> and <YResolution>. The same unit is used for
  /// both <XResolution> and <YResolution>. If the image resolution is unknown; 2 (inches)
  /// is designated.
  /// </summary>
  [MetadataTagEnum( 1, "None" )]
  [MetadataTagEnum( 2, "inches" )]
  [MetadataTagEnum( 3, "cm" )]
  [MetadataTagDetails( "Resolution Unit", "Unit for measuring Horizontal and Vertical resolutions" )]
  public const ushort ResolutionUnit = 0x0128;

  [MetadataTagDetails( "Page Number", "Page number of the page from which this image was scanned" )]
  public const ushort PageNumber = 0x0129;

  [MetadataTagDetails( "Transfer Function", "Transfer function for the image described in tabular style" )]
  public const ushort TransferFunction = 0x012d;

  [MetadataTagDetails( "Software", "Records the name and version of the software or firmware of the camera or image input device used to generate the image" )]
  public const ushort Software = 0x0131;

  [MetadataTagDetails( "Date Time", "Date and time of image creation or when last modified" )]
  public const ushort DateTime = 0x0132;

  [MetadataTagDetails( "Artist", "Records the name of the camera owner, photographer or image creator" )]
  public const ushort Artist = 0x013b;

  [MetadataTagDetails( "Host Computer", "Records information about the host computer used to generate the image" )]
  public const ushort HostComputer = 0x013c;

  [MetadataTagEnum( 1, "None" )]
  [MetadataTagEnum( 2, "Horizontal differencing" )]
  [MetadataTagEnum( 3, "Floating point" )]
  [MetadataTagEnum( 34892, "Horizontal difference X2" )]
  [MetadataTagEnum( 34893, "Horizontal difference X4" )]
  [MetadataTagEnum( 34894, "Floating point X2" )]
  [MetadataTagEnum( 34895, "Floating point X4" )]
  [MetadataTagDetails( "Predictor", "Mathematical operator that is applied to the image data before an encoding scheme is applied" )]
  public const ushort Predictor = 0x013d;

  [MetadataTagDetails( "White Point", "Chromaticity of the white point of the image" )]
  public const ushort WhitePoint = 0x013e;

  [MetadataTagDetails( "Primary Chromaticities", "Chromaticity of the three primary colors of the image" )]
  public const ushort PrimaryChromaticities = 0x013f;

  [MetadataTagDetails( "Color Map", "Defines a Red-Green-Blue color map, or lookup table, for palette-color images where a pixel value is used to index into the map" )]
  public const ushort ColorMap = 0x0140;

  [MetadataTagDetails( "Halftone Hints", "Conveys to the halftone function the range of gray levels within a colorimetrically-specified image that should retain tonal detail" )]
  public const ushort HalftoneHints = 0x0141;

  [MetadataTagDetails( "Tile Width", "Number of columns/pixels in each tile" )]
  public const ushort TileWidth = 0x0142;

  [MetadataTagDetails( "Tile Height", "Number of rows in each tile" )]
  public const ushort TileLength = 0x0143;

  [MetadataTagDetails( "Tile Offsets", "Byte offset of each tile as compressed and stored on disk" )]
  public const ushort TileOffsets = 0x0144;

  [MetadataTagDetails( "Tile Byte Counts", "Number of (compressed) bytes in the tile" )]
  public const ushort TileByteCounts = 0x0145;

  [MetadataTagEnum( 1, "CMYK" )]
  [MetadataTagEnum( 2, "Not CMYK" )]
  [MetadataTagDetails( "Ink Set", "Set of inks used in a separated (PhotometricInterpretation) image" )]
  public const ushort InkSet = 0x014c;

  [MetadataTagDetails( "Ink Names", "Name of each ink used in a separated (PhotometricInterpretation) image" )]
  public const ushort InkNames = 0x014d;

  [MetadataTagDetails( "Number Of Inks", "Number of inks, usually equal to SamplesPerPixel" )]
  public const ushort NumberOfInks = 0x014e;

  [MetadataTagDetails( "Dot Range", "Component values that correspond to a 0% dot and 100% dot" )]
  public const ushort DotRange = 0x0150;

  [MetadataTagDetails( "Target Printer", "Description of the printing environment for which this separation is intended" )]
  public const ushort TargetPrinter = 0x0151;

  [MetadataTagEnum( 0, "Unspecified" )]
  [MetadataTagEnum( 1, "Associated Alpha" )]
  [MetadataTagEnum( 3, "Associated Alpha" )]
  [MetadataTagDetails( "Extra Samples", "Specifies that each pixel has extra components whose interpretation is defined by one of the values listed in SampleFormat" )]
  public const ushort ExtraSamples = 0x0152;

  [MetadataTagEnum( 1, "Unsigned" )]
  [MetadataTagEnum( 2, "Signed" )]
  [MetadataTagEnum( 3, "Float" )]
  [MetadataTagEnum( 4, "Undefined" )]
  [MetadataTagEnum( 5, "Complex int" )]
  [MetadataTagEnum( 6, "Complex float" )]
  [MetadataTagDetails( "Sample Format", "Specifies how to interpret each data sample in a pixel" )]
  public const ushort SampleFormat = 0x0153;

  [MetadataTagDetails( "Minimum Sample Value", "Specifies the minimum sample value" )]
  public const ushort SMinSampleValue = 0x0154;

  [MetadataTagDetails( "Maximum Sample Value", "Specifies the maximum sample value" )]
  public const ushort SMaxSampleValue = 0x0155;

  [MetadataTagDetails( "Transfer Range", "Expands the range of the TransferFunction" )]
  public const ushort TransferRange = 0x0156;

  [MetadataTagDetails( "Clip Path", "Intended to mirror the essentials of PostScript's path creation functionality" )]
  public const ushort ClipPath = 0x0157;

  [MetadataTagDetails( "X Clip Path Units", "Number of units that span the width of the image" )]
  public const ushort XClipPathUnits = 0x0158;

  [MetadataTagDetails( "Y Clip Path Units", "Number of units that span the height of the image" )]
  public const ushort YClipPathUnits = 0x0159;

  [MetadataTagEnum( 0, "Not indexed" )]
  [MetadataTagEnum( 1, "Indexed" )]
  [MetadataTagDetails( "Indexed", "Indexed images are images where the 'pixels' do not represent color values but an index into a separate color map" )]
  public const ushort Indexed = 0x015a;

  [MetadataTagDetails( "JPEG Tables", "May be used to encode the JPEG quantization and Huffman tables for subsequent use by the JPEG decompression process" )]
  public const ushort JPEGTables = 0x015b;

  [MetadataTagEnum( 0, "Higher resolution image does not exist" )]
  [MetadataTagEnum( 1, "Higher resolution image exists" )]
  [MetadataTagDetails( "OPI Proxy", "Gives information concerning whether this image is a low-resolution proxy of a high-resolution image" )]
  public const ushort OPIProxy = 0x015f;

  [MetadataTagDetails( "JPEG Process", "Indicates the process used to produce the compressed data" )]
  public const ushort JPEGProc = 0x0200;

  [MetadataTagDetails( "JPEG Interchange Format", "Offset to the start byte of the JPEG compressed thumbnail data" )]
  public const ushort JPEGInterchangeFormat = 0x0201;

  [MetadataTagDetails( "JPEG Interchange Format Length", "Number of bytes of JPEG compressed thumbnail data" )]
  public const ushort JPEGInterchangeFormatLength = 0x0202;

  [MetadataTagDetails( "JPEG Restart Interval", "Indicates the length of the restart interval used in the compressed image data" )]
  public const ushort JPEGRestartInterval = 0x0203;

  [MetadataTagDetails( "JPEG Lossless Predictors", "Points to a list of lossless predictor/selection values, one per component" )]
  public const ushort JPEGLosslessPredictors = 0x0205;

  [MetadataTagDetails( "JPEG Point Transforms", "Points to a list of point transform, one per component" )]
  public const ushort JPEGPointTransforms = 0x0206;

  [MetadataTagDetails( "JPEG Quantization Tables", "Points to a list of offsets to the quantization tables, one per component" )]
  public const ushort JPEGQTables = 0x0207;

  [MetadataTagDetails( "JPEG DC Huffman Tables", "Points to a list of offsets to the DC Huffman or lossless Huffman tables, one per component" )]
  public const ushort JPEGDCTables = 0x0208;

  [MetadataTagDetails( "JPEG Huffman AC Tables", "Points to a list of offsets to the Huffman AC tables, one per component" )]
  public const ushort JPEGACTables = 0x0209;

  [MetadataTagDetails( "YCbCr Coefficients", "Matrix coefficients for transformation from RGB to YCbCr image data" )]
  public const ushort YCbCrCoefficients = 0x0211;

  [MetadataTagDetails( "YCbCr SubSampling", "Sampling ratio of chrominance components in relation to the luminance component" )]
  public const ushort YCbCrSubSampling = 0x0212;

  [MetadataTagEnum( 1, "Centered" )]
  [MetadataTagEnum( 2, "Co-sited" )]
  [MetadataTagDetails( "YCbCr Positioning", "Position of chrominance components in relation to the luminance component" )]
  public const ushort YCbCrPositioning = 0x0213;

  [MetadataTagDetails( "Reference Black White", "Reference black point value and reference white point value" )]
  public const ushort ReferenceBlackWhite = 0x0214;

  [MetadataTagDetails( "XML Packet", "XMP metadata" )]
  public const ushort XMLPacket = 0x02bc;

  [MetadataTagDetails( "Rating", "Windows rating value" )]
  public const ushort Rating = 0x4746;

  [MetadataTagDetails( "Rating Percent", "Windows rating percentage value" )]
  public const ushort RatingPercent = 0x4749;

  [MetadataTagDetails( "Vignetting Correction Parameters", "Sony vignetting correction parameters" )]
  public const ushort VignettingCorrParams = 0x7032;

  [MetadataTagDetails( "Chromatic Aberration Correction Parameters", "Sony chromatic aberration correction parameters" )]
  public const ushort ChromaticAberrationCorrParams = 0x7035;

  [MetadataTagDetails( "Distortion Correction Parameters", "Sony distortion correction parameters" )]
  public const ushort DistortionCorrParams = 0x7037;

  [MetadataTagDetails( "Image ID", "Full pathname of the original, high-resolution image; or any other identifying string that uniquely identifies the original image" )]
  public const ushort ImageID = 0x800d;

  [MetadataTagDetails( "CFA Repeat Pattern Dim", "Representing the minimum rows and columns to define the repeating patterns of the color filter array (CFA)" )]
  public const ushort CFARepeatPatternDim = 0x828d;

  [MetadataTagDetails( "CFA Pattern", "The color filter array (CFA) geometric pattern of the image sensor when a one-chip color area sensor is used" )]
  public const ushort CFAPattern = 0x828e;

  [MetadataTagDetails( "Battery Level", "Value of the battery level as a fraction or string" )]
  public const ushort BatteryLevel = 0x828f;

  [MetadataTagDetails( "Copyright", "Indicates both the photographer and editor copyrights; it is the copyright notice of the person or organization claiming rights to the image" )]
  public const ushort Copyright = 0x8298;

  [MetadataTagDetails( "Exposure Time", "Exposure time in seconds" )]
  public const ushort ExposureTime = 0x829a;

  [MetadataTagDetails( "F Number", "Camera F number" )]
  public const ushort FNumber = 0x829d;

  [MetadataTagDetails( "IPTC/NAA Record", "Contains the IPTC/NAA record" )]
  public const ushort IPTCNAA = 0x83bb;

  [MetadataTagDetails( "Image Resources", "Contains information embedded by the Adobe Photoshop application" )]
  public const ushort ImageResources = 0x8649;

  [MetadataTagDetails( "Inter Color Profile", "Contains an InterColor Consortium (ICC) format color space characterization/profile" )]
  public const ushort InterColorProfile = 0x8773;

  [MetadataTagEnum( 0, "Not Defined" )]
  [MetadataTagEnum( 1, "Manual" )]
  [MetadataTagEnum( 2, "Program AE" )]
  [MetadataTagEnum( 3, "Aperture-priority AE" )]
  [MetadataTagEnum( 4, "Shutter speed priority AE" )]
  [MetadataTagEnum( 5, "Creative (Slow speed)" )]
  [MetadataTagEnum( 6, "Action (High speed)" )]
  [MetadataTagEnum( 7, "Portrait" )]
  [MetadataTagEnum( 8, "Landscape" )]
  [MetadataTagEnum( 9, "Bulb" )]
  [MetadataTagDetails( "Exposure Program", "Class of the program used by the camera to set exposure when the picture is taken" )]
  public const ushort ExposureProgram = 0x8822;

  [MetadataTagDetails( "Spectral Sensitivity", "Indicates the spectral sensitivity of each channel of the camera used" )]
  public const ushort SpectralSensitivity = 0x8824;

  [MetadataTagDetails( "ISO Speed Ratings", "Indicates the ISO Speed and ISO Latitude of the camera or input device as specified in ISO 12232" )]
  public const ushort ISOSpeedRatings = 0x8827;

  [MetadataTagDetails( "OECF", "Indicates the Opto-Electric Conversion Function (OECF) specified in ISO 14524" )]
  public const ushort OECF = 0x8828;

  [MetadataTagDetails( "Interlace", "Indicates the field number of multifield images" )]
  public const ushort Interlace = 0x8829;

  [MetadataTagDetails( "Time Zone Offset", "Encodes the time zone of the camera clock (relative to Greenwich Mean Time) used to create the DataTimeOriginal tag-value when the picture was taken" )]
  public const ushort TimeZoneOffset = 0x882a;

  [MetadataTagDetails( "Self Timer Mode", "Number of seconds image capture was delayed from button press" )]
  public const ushort SelfTimerMode = 0x882b;

  [MetadataTagDetails( "Date Time Original", "Date and time when the original image data was generated" )]
  public const ushort DateTimeOriginal = 0x9003;

  [MetadataTagDetails( "Compressed Bits Per Pixel", "Specific to compressed data and states the compressed bits per pixel" )]
  public const ushort CompressedBitsPerPixel = 0x9102;

  [MetadataTagDetails( "Shutter Speed", "Camera shutter speed used for this image" )]
  public const ushort ShutterSpeedValue = 0x9201;

  [MetadataTagDetails( "Lens Aperture", "Camera lens aperture used for this image" )]
  public const ushort ApertureValue = 0x9202;

  [MetadataTagDetails( "Brightness", "Camera brightness value" )]
  public const ushort BrightnessValue = 0x9203;

  [MetadataTagDetails( "Exposure Bias", "Camera exposure bias setting" )]
  public const ushort ExposureBiasValue = 0x9204;

  [MetadataTagDetails( "Max Exposure", "Smallest F number of the lens" )]
  public const ushort MaxApertureValue = 0x9205;

  [MetadataTagDetails( "Subject Distance", "Distance to the subject (given in meters)" )]
  public const ushort SubjectDistance = 0x9206;

  [MetadataTagEnum( 0, "Unknown" )]
  [MetadataTagEnum( 1, "Average" )]
  [MetadataTagEnum( 2, "Center-weighted average" )]
  [MetadataTagEnum( 3, "Spot" )]
  [MetadataTagEnum( 4, "Multi-spot" )]
  [MetadataTagEnum( 5, "Multi-segment" )]
  [MetadataTagEnum( 6, "Partial" )]
  [MetadataTagEnum( 255, "Other" )]
  [MetadataTagDetails( "Metering Mode", "Metering mode" )]
  public const ushort MeteringMode = 0x9207;

  [MetadataTagEnum( 0, "Unknown" )]
  [MetadataTagEnum( 1, "Daylight" )]
  [MetadataTagEnum( 2, "Fluorescent" )]
  [MetadataTagEnum( 3, "Tungsten (Incandescent)" )]
  [MetadataTagEnum( 4, "Flash" )]
  [MetadataTagEnum( 9, "Fine Weather" )]
  [MetadataTagEnum( 10, "Cloudy" )]
  [MetadataTagEnum( 11, "Shade" )]
  [MetadataTagEnum( 12, "Daylight Fluorescent" )]
  [MetadataTagEnum( 13, "Day White Fluorescent" )]
  [MetadataTagEnum( 14, "Cool White Fluorescent" )]
  [MetadataTagEnum( 15, "White Fluorescent" )]
  [MetadataTagEnum( 16, "Warm White Fluorescent" )]
  [MetadataTagEnum( 17, "Standard Light A" )]
  [MetadataTagEnum( 18, "Standard Light B" )]
  [MetadataTagEnum( 19, "Standard Light C" )]
  [MetadataTagEnum( 20, "D55" )]
  [MetadataTagEnum( 21, "D65" )]
  [MetadataTagEnum( 22, "D75" )]
  [MetadataTagEnum( 23, "D50" )]
  [MetadataTagEnum( 24, "ISO Studio Tungsten" )]
  [MetadataTagEnum( 255, "Other" )]
  [MetadataTagDetails( "Light Source", "Kind of light source" )]
  public const ushort LightSource = 0x9208;

  [MetadataTagEnum( 0, "No Flash" )]
  [MetadataTagEnum( 1, "Fired" )]
  [MetadataTagEnum( 5, "Fired, Return not detected" )]
  [MetadataTagEnum( 7, "Fired, Return detected" )]
  [MetadataTagEnum( 8, "On, Did not fire" )]
  [MetadataTagEnum( 9, "On, Fired" )]
  [MetadataTagEnum( 13, "On, Return not detected" )]
  [MetadataTagEnum( 15, "On, Return detected" )]
  [MetadataTagEnum( 16, "Off, Did not fire" )]
  [MetadataTagEnum( 20, "Off, Did not fire, Return not detected" )]
  [MetadataTagEnum( 24, "Auto, Did not fire" )]
  [MetadataTagEnum( 25, "Auto, Fired" )]
  [MetadataTagEnum( 29, "Auto, Fired, Return not detected" )]
  [MetadataTagEnum( 31, "Auto, Fired, Return detected" )]
  [MetadataTagEnum( 32, "No flash function" )]
  [MetadataTagEnum( 48, "Off, No flash function" )]
  [MetadataTagEnum( 65, "Fired, Red-eye reduction" )]
  [MetadataTagEnum( 69, "Fired, Red-eye reduction, Return not detected" )]
  [MetadataTagEnum( 71, "Fired, Red-eye reduction, Return detected" )]
  [MetadataTagEnum( 73, "On, Red-eye reduction" )]
  [MetadataTagEnum( 77, "On, Red-eye reduction, Return not detected" )]
  [MetadataTagEnum( 79, "On, Red-eye reduction, Return detected" )]
  [MetadataTagEnum( 80, "Off, Red-eye reduction" )]
  [MetadataTagEnum( 88, "Auto, Did not fire, Red-eye reduction" )]
  [MetadataTagEnum( 89, "Auto, Fired, Red-eye reduction" )]
  [MetadataTagEnum( 93, "Auto, Fired, Red-eye reduction, Return not detected" )]
  [MetadataTagEnum( 95, "Auto, Fired, Red-eye reduction, Return detected" )]
  [MetadataTagDetails( "Flash", "Indicates the status of flash when the image was shot" )]
  public const ushort Flash = 0x9209;

  [MetadataTagDetails( "Focal Length", "Actual focal length of the lens in mm" )]
  public const ushort FocalLength = 0x920a;

  [MetadataTagDetails( "Flash Energy", "Amount of flash energy (BCPS) used" )]
  public const ushort FlashEnergy = 0x920b;

  [MetadataTagDetails( "Spatial Frequency Response", "Spatial Frequency Response (SFR) of the camera" )]
  public const ushort SpatialFrequencyResponse = 0x920c;

  [MetadataTagDetails( "Noise", "Noise measurement values" )]
  public const ushort Noise = 0x920d;

  [MetadataTagDetails( "Focal Plane X Resolution", "Number of pixels per FocalPlaneResolutionUnit in ImageWidth direction for main image" )]
  public const ushort FocalPlaneXResolution = 0x920e;

  [MetadataTagDetails( "Focal Plane Y Resolution", "Number of pixels per FocalPlaneResolutionUnit in ImageHeight direction for main image" )]
  public const ushort FocalPlaneYResolution = 0x920f;

  [MetadataTagEnum( 1, "None" )]
  [MetadataTagEnum( 2, "inches" )]
  [MetadataTagEnum( 3, "cm" )]
  [MetadataTagEnum( 4, "mm" )]
  [MetadataTagEnum( 5, "um" )]
  [MetadataTagDetails( "Focal Plane Resolution Unit", "Unit of measurement for FocalPlaneXResolution and FocalPlaneYResolution" )]
  public const ushort FocalPlaneResolutionUnit = 0x9210;

  [MetadataTagDetails( "Image Number", "Number assigned to an image, e.g. in a chained image burst" )]
  public const ushort ImageNumber = 0x9211;

  [MetadataTagEnum( "C", "Confidential" )]
  [MetadataTagEnum( "R", "Restricted" )]
  [MetadataTagEnum( "S", "Secret" )]
  [MetadataTagEnum( "T", "Top Secret" )]
  [MetadataTagEnum( "U", "Unclassified" )]
  [MetadataTagDetails( "Security Classification", "Security classification assigned to the image" )]
  public const ushort SecurityClassification = 0x9212;

  [MetadataTagDetails( "Image History", "Historical record of changes made to the image" )]
  public const ushort ImageHistory = 0x9213;

  [MetadataTagDetails( "Subject Location", "Indicates the location and area of the main subject in the overall scene" )]
  public const ushort SubjectLocation = 0x9214;

  [MetadataTagDetails( "Exposure Index", "Encodes the camera exposure index setting when image was captured" )]
  public const ushort ExposureIndex = 0x9215;

  [MetadataTagDetails( "TIFF/EP Standard ID", "Contains four ASCII characters representing the TIFF/EP standard version of a TIFF/EP file; eg '1', '0', '0', '0'" )]
  public const ushort TIFFEPStandardID = 0x9216;

  [MetadataTagDetails( "Sensing Method", "Type of image sensor" )]
  public const ushort SensingMethod = 0x9217;

  [MetadataTagDetails( "XP Title", "Title tag used by Windows" )]
  public const ushort XPTitle = 0x9c9b;

  [MetadataTagDetails( "XP Comment", "Comment tag used by Windows" )]
  public const ushort XPComment = 0x9c9c;

  [MetadataTagDetails( "XP Author", "Author tag used by Windows" )]
  public const ushort XPAuthor = 0x9c9d;

  [MetadataTagDetails( "XP Keywords", "Keywords tag used by Windows" )]
  public const ushort XPKeywords = 0x9c9e;

  [MetadataTagDetails( "XP Subject ", "Subject tag used by Windows" )]
  public const ushort XPSubject = 0x9c9f;

  [MetadataTagDetails( "Print Image Matching", "" )]
  public const ushort PrintImageMatching = 0xc4a5;

  [MetadataTagDetails( "DNG Version", "Encodes the DNG four-tier version number. For files compliant with version 1.1.0.0 of the DNG specification, this tag should contain the bytes: 1, 1, 0, 0" )]
  public const ushort DNGVersion = 0xc612;

  /// <summary>
  /// Readers should not attempt to read a file if this tag specifies a version number 
  /// that is higher than the version number of the specification the reader was based 
  /// on. In addition to checking the version tags; readers should, for all tags, 
  /// check the types, counts, and values to verify it is able to correctly read the file.
  /// </summary>
  [MetadataTagDetails( "DNG Backward Version", "Specifies the oldest version of the Digital Negative specification for which a file is compatible" )]
  public const ushort DNGBackwardVersion = 0xc613;

  /// <summary>
  /// This name should include the manufacturer's name to avoid conflicts; and should
  /// not be localized; even if the camera name itself is localized for different
  /// markets (see LocalizedCameraModel). This string may be used by reader software
  /// to index into per-model preferences and replacement profiles.
  /// </summary>
  [MetadataTagDetails( "Unique Camera Model", "Defines a unique, non-localized name for the camera model that created the image in the raw file" )]
  public const ushort UniqueCameraModel = 0xc614;

  /// <summary>
  /// Similar to the UniqueCameraModel field; except the name can be localized for
  /// different markets to match the localization of the camera name.
  /// </summary>
  [MetadataTagDetails( "Localized Camera Model", "Defines a unique, localized name for the camera model that created the image in the raw file" )]
  public const ushort LocalizedCameraModel = 0xc615;

  [MetadataTagDetails( "CFA Plane Color", "Provides a mapping between the values in the CFAPattern tag and the plane numbers in LinearRaw space" )]
  public const ushort CFAPlaneColor = 0xc616;

  [MetadataTagDetails( "CFA Layout", "Describes the spatial layout of the CFA" )]
  public const ushort CFALayout = 0xc617;

  /// <summary>
  ///  This tag is typically used to increase compression ratios by storing the raw data
  ///  in a non-linear; more visually uniform space with fewer total encoding levels.
  ///  If SamplesPerPixel is not equal to one; this single table applies to all the
  ///  samples for each pixel.
  /// </summary>
  [MetadataTagDetails( "Linearization Table", "Describes a lookup table that maps stored values into linear values" )]
  public const ushort LinearizationTable = 0xc618;

  [MetadataTagDetails( "Black Level Repeat Dim", "Specifies repeat pattern size for the BlackLevel tag" )]
  public const ushort BlackLevelRepeatDim = 0xc619;

  /// <summary>
  /// The origin of this pattern is the top-left corner of the ActiveArea rectangle.
  /// The values are stored in row-column-sample scan order.
  /// </summary>
  [MetadataTagDetails( "Black Level", "Specifies the zero light (a.k.a. thermal black or black current) encoding level as a repeating pattern" )]
  public const ushort BlackLevel = 0xc61a;

  /// <summary>
  /// If SamplesPerPixel is not equal to one; this single table applies to all the
  /// samples for each pixel.
  /// </summary>
  [MetadataTagDetails( "Black Level Delta H", "Specifies the difference between the zero light encoding level for each column and the baseline zero light encoding level" )]
  public const ushort BlackLevelDeltaH = 0xc61b;

  /// <summary>
  /// If SamplesPerPixel is not equal to one; this single table applies to all the
  /// samples for each pixel.
  /// </summary>
  [MetadataTagDetails( "Black Level Delta V", "Specifies the difference between the zero light encoding level for each row and the baseline zero light encoding level" )]
  public const ushort BlackLevelDeltaV = 0xc61c;

  /// <summary>
  /// Saturation is caused either by the sensor itself becoming highly non-linear
  /// in response; or by the camera's analog to digital converter clipping.
  /// </summary>
  [MetadataTagDetails( "White Level", "Specifies the fully saturated encoding level for the raw sample values" )]
  public const ushort WhiteLevel = 0xc61d;

  [MetadataTagDetails( "Default Scale", "Specifies the default scale factors for each direction to convert the image to square pixels" )]
  public const ushort DefaultScale = 0xc61e;

  [MetadataTagDetails( "Default Crop Origin", "Specifies the origin of the final image area, in raw image coordinates (i.e. before the DefaultScale has been applied), relative to the top-left corner of the ActiveArea rectangle, cropping out interpolation artifacts near the edges" )]
  public const ushort DefaultCropOrigin = 0xc61f;

  [MetadataTagDetails( "Default Crop Size", "Specifies the size of the final image area, in raw image coordinates, cropping out interpolation artifacts near the edges" )]
  public const ushort DefaultCropSize = 0xc620;

  [MetadataTagDetails( "Color Matrix 1", "Defines a transformation matrix that converts XYZ values to reference camera native color space values, under the first calibration illuminant" )]
  public const ushort ColorMatrix1 = 0xc621;

  [MetadataTagDetails( "Color Matrix2 ", "Defines a transformation matrix that converts XYZ values to reference camera native color space values, under the second calibration illuminant" )]
  public const ushort ColorMatrix2 = 0xc622;

  [MetadataTagDetails( "Camera Calibration 1", "Defines a calibration matrix that transforms reference camera native space values to individual camera native space values under the first calibration illuminant" )]
  public const ushort CameraCalibration1 = 0xc623;

  [MetadataTagDetails( "Camera Calibration 2", "Defines a calibration matrix that transforms reference camera native space values to individual camera native space values under the second calibration illuminant" )]
  public const ushort CameraCalibration2 = 0xc624;

  [MetadataTagDetails( "Reduction Matrix 1", "Defines a dimensionality reduction matrix for use as the first stage in converting color camera native space values to XYZ values; under the first calibration illuminant" )]
  public const ushort ReductionMatrix1 = 0xc625;

  [MetadataTagDetails( "Reduction Matrix 2", "Defines a dimensionality reduction matrix for use as the first stage in converting color camera native space values to XYZ values, under the second calibration illuminant" )]
  public const ushort ReductionMatrix2 = 0xc626;

  [MetadataTagDetails( "Analog Balance", "Defines the gain, either analog (recommended) or digital (not recommended), that has been applied to the stored raw values" )]
  public const ushort AnalogBalance = 0xc627;

  [MetadataTagDetails( "AsShot Neutral", "Specifies the selected white balance at time of capture, encoded as the coordinates of a perfectly neutral color in linear reference space values" )]
  public const ushort AsShotNeutral = 0xc628;

  [MetadataTagDetails( "AsShot White XY", "Specifies the selected white balance at time of capture, encoded as x-y chromaticity coordinates" )]
  public const ushort AsShotWhiteXY = 0xc629;

  /// <summary>
  /// Positive values result in brighter default results, while negative values result in darker default results
  /// </summary>
  [MetadataTagDetails( "Baseline Exposure", "Specifies by how much (in EV units) to move the zero point" )]
  public const ushort BaselineExposure = 0xc62a;

  [MetadataTagDetails( "Baseline Noise", "Specifies the relative noise level of the camera model at a baseline ISO value of 100, compared to a reference camera model" )]
  public const ushort BaselineNoise = 0xc62b;

  [MetadataTagDetails( "Baseline Sharpness", "Specifies the relative amount of sharpening required for this camera model, compared to a reference camera model" )]
  public const ushort BaselineSharpness = 0xc62c;

  [MetadataTagDetails( "Bayer Green Split", "Specifies, in arbitrary units, how closely the values of the green pixels in the blue/green rows track the values of the green pixels in the red/green rows" )]
  public const ushort BayerGreenSplit = 0xc62d;

  [MetadataTagDetails( "Linear Response Limit", "Specifies the fraction of the encoding range above which the response may become significantly non-linear" )]
  public const ushort LinearResponseLimit = 0xc62e;

  [MetadataTagDetails( "Camera Serial Number", "Contains the serial number of the camera or camera body that captured the image" )]
  public const ushort CameraSerialNumber = 0xc62f;

  [MetadataTagDetails( "Lens Info", "Contains information about the lens that captured the image" )]
  public const ushort LensInfo = 0xc630;

  [MetadataTagDetails( "Chroma Blur Radius", "Contains a hint how much chroma blur should be applied to the image" )]
  public const ushort ChromaBlurRadius = 0xc631;

  [MetadataTagDetails( "Anti-Alias Strength", "Contains a hint about the stength of the camera's anti-alias filter" )]
  public const ushort AntiAliasStrength = 0xc632;

  [MetadataTagDetails( "Shadow Scale", "Controls the sensitivity of the 'Shadows' slider in Adobe Camera Raw" )]
  public const ushort ShadowScale = 0xc633;

  [MetadataTagDetails( "DNG Private Data", "Specifies camera manufacturer specific private data" )]
  public const ushort DNGPrivateData = 0xc634;

  [MetadataTagDetails( "Maker Note Safety", "Specifies if the DNG reader is safe to preserve the EXIF MakerNote tag along with the rest of the EXIF data" )]
  public const ushort MakerNoteSafety = 0xc635;

  [MetadataTagDetails( "Calibration Illuminant 1", "Used for the first set of color calibration tags (ColorMatrix2, CameraCalibration2, ReductionMatrix2)" )]
  public const ushort CalibrationIlluminant1 = 0xc65a;

  [MetadataTagDetails( "Calibration Illuminant 2", "Used for an optional second set of color calibration tags (ColorMatrix2, CameraCalibration2, ReductionMatrix2)" )]
  public const ushort CalibrationIlluminant2 = 0xc65b;

  [MetadataTagDetails( "Best Quality Scale", "Specifies the amount by which the values of the DefaultScale tag need to be multiplied to achieve the best quality image size" )]
  public const ushort BestQualityScale = 0xc65c;

  [MetadataTagDetails( "Raw Data Unique ID", "Contains a 16-byte unique identifier for the raw image data in the DNG file" )]
  public const ushort RawDataUniqueID = 0xc65d;

  [MetadataTagDetails( "Original Raw File Name", "Contains the file name of that original raw file" )]
  public const ushort OriginalRawFileName = 0xc68b;

  [MetadataTagDetails( "Original Raw File Data", "Contains the compressed contents of that original raw file" )]
  public const ushort OriginalRawFileData = 0xc68c;

  [MetadataTagDetails( "Active Area", "Contains a rectangle which defines the active (non-masked) pixels of the sensor" )]
  public const ushort ActiveArea = 0xc68d;

  [MetadataTagDetails( "Masked Areas", "Contains a list of non-overlapping rectangle coordinates of fully masked pixels, which can be optionally used by DNG readers to measure the black encoding level" )]
  public const ushort MaskedAreas = 0xc68e;

  [MetadataTagDetails( "AsShot ICC Profile", "Contains an ICC profile that provides the camera manufacturer with a way to specify a default color rendering from camera color space coordinates (linear reference values) into the ICC profile connection space" )]
  public const ushort AsShotICCProfile = 0xc68f;

  [MetadataTagDetails( "AsShot Pre Profile Matrix", "Specifies a matrix that should be applied to the camera color space coordinates before processing the values through the ICC profile specified in the AsShotICCProfile tag" )]
  public const ushort AsShotPreProfileMatrix = 0xc690;

  [MetadataTagDetails( "Current ICC Profile", "Contains data which has the same purpose and usage as the AsShotICCProfile and AsShotPreProfileMatrix tag pair, except they are for use by raw file editors rather than camera manufacturers" )]
  public const ushort CurrentICCProfile = 0xc691;

  [MetadataTagDetails( "Current Pre Profile Matrix", "Contains data which has the same purpose and usage as the AsShotICCProfile and AsShotPreProfileMatrix tag pair, except they are for use by raw file editors rather than camera manufacturers" )]
  public const ushort CurrentPreProfileMatrix = 0xc692;

  [MetadataTagDetails( "Colorimetric Reference", "Describes the colorimetric reference for the CIE XYZ values" )]
  public const ushort ColorimetricReference = 0xc6bf;

  [MetadataTagDetails( "Camera Calibration Signature", "Contains a string associated with the CameraCalibration1 and CameraCalibration2 tags" )]
  public const ushort CameraCalibrationSignature = 0xc6f3;

  [MetadataTagDetails( "Profile Calibration Signature", "Contains the camera profile tags" )]
  public const ushort ProfileCalibrationSignature = 0xc6f4;

  [MetadataTagDetails( "Extra Camera Profiles", "List of file offsets to extra Camera Profile IFDs" )]
  public const ushort ExtraCameraProfiles = 0xc6f5;

  [MetadataTagDetails( "As Shot Profile Name", "Contains the name of the 'as shot' camera profile" )]
  public const ushort AsShotProfileName = 0xc6f6;

  [MetadataTagDetails( "Noise Reduction Applied", "Indicates how much noise reduction has been applied to the raw data on a scale of 0.0 to 1.0" )]
  public const ushort NoiseReductionApplied = 0xc6f7;

  [MetadataTagDetails( "Profile Name", "Contains the name of the camera profile" )]
  public const ushort ProfileName = 0xc6f8;

  [MetadataTagDetails( "Profile Hue/Saturation Mapping Dimensions", "Specifies the number of input samples in each dimension of the hue/saturation/value mapping tables" )]
  public const ushort ProfileHueSatMapDims = 0xc6f9;

  [MetadataTagDetails( "Profile Hue/Saturation Mapping Data 1", "Contains the data for the first hue/saturation/value mapping table" )]
  public const ushort ProfileHueSatMapData1 = 0xc6fa;

  [MetadataTagDetails( "Profile Hue/Saturation Mapping Data 2", "Contains the data for the second hue/saturation/value mapping table" )]
  public const ushort ProfileHueSatMapData2 = 0xc6fb;

  [MetadataTagDetails( "Profile Tone Curve", "Contains a default tone curve that can be applied while processing the image as a starting point for user adjustments" )]
  public const ushort ProfileToneCurve = 0xc6fc;

  [MetadataTagDetails( "Profile Embed Policy", "Contains information about the usage rules for the associated camera profile" )]
  public const ushort ProfileEmbedPolicy = 0xc6fd;

  [MetadataTagDetails( "Profile Copyright", "Contains the copyright information for the camera profile" )]
  public const ushort ProfileCopyright = 0xc6fe;

  [MetadataTagDetails( "Forward Matrix 1", "Defines a matrix that maps white balanced camera colors to XYZ D50 colors" )]
  public const ushort ForwardMatrix1 = 0xc714;

  [MetadataTagDetails( "Forward Matrix 2", "Defines a matrix that maps white balanced camera colors to XYZ D50 colors" )]
  public const ushort ForwardMatrix2 = 0xc715;

  [MetadataTagDetails( "Preview Application Name", "Contains the name of the application that created the preview stored in the IFD" )]
  public const ushort PreviewApplicationName = 0xc716;

  [MetadataTagDetails( "Preview Application Version", "Contains the version number of the application that created the preview stored in the IFD" )]
  public const ushort PreviewApplicationVersion = 0xc717;

  [MetadataTagDetails( "Preview Settings Name", "Contains the name of the conversion settings used for the preview stored in the IFD" )]
  public const ushort PreviewSettingsName = 0xc718;

  [MetadataTagDetails( "Preview Settings Digest", "Specifies unique ID of the conversion settings used to render the preview stored in the IFD" )]
  public const ushort PreviewSettingsDigest = 0xc719;

  [MetadataTagDetails( "Preview Color Space", "Specifies the color space in which the rendered preview in this IFD is stored" )]
  public const ushort PreviewColorSpace = 0xc71a;

  [MetadataTagDetails( "Preview Date Time", "Contains the name of the date/time at which the preview stored in the IFD was rendered" )]
  public const ushort PreviewDateTime = 0xc71b;

  [MetadataTagDetails( "Raw Image Digest", "Contains an MD5 digest of the raw image data" )]
  public const ushort RawImageDigest = 0xc71c;

  [MetadataTagDetails( "Original Raw File Digest", "Contains an MD5 digest of the data stored in the OriginalRawFileData tag" )]
  public const ushort OriginalRawFileDigest = 0xc71d;

  [MetadataTagDetails( "Sub Tile Block Size", "Specifies that the pixels within a tile should be grouped first into rectangular blocks of the specified size" )]
  public const ushort SubTileBlockSize = 0xc71e;

  [MetadataTagDetails( "Row Interleave Factor", "Specifies that rows of the image are stored in interleaved order" )]
  public const ushort RowInterleaveFactor = 0xc71f;

  [MetadataTagDetails( "Profile LookTable Dimensions", "Specifies the number of input samples in each dimension of a default LookTable" )]
  public const ushort ProfileLookTableDims = 0xc725;

  [MetadataTagDetails( "Profile LookTable Data", "Contains a default LookTable that can be applied while processing the image as a starting point for user adjustment" )]
  public const ushort ProfileLookTableData = 0xc726;

  [MetadataTagDetails( "Opcode List 1", "Specifies the list of opcodes that should be applied to the raw image, as read directly from the file" )]
  public const ushort OpcodeList1 = 0xc740;

  [MetadataTagDetails( "Opcode List 2", "Specifies the list of opcodes that should be applied to the raw image, just after it has been mapped to linear reference values" )]
  public const ushort OpcodeList2 = 0xc741;

  [MetadataTagDetails( "Opcode List 3", "Specifies the list of opcodes that should be applied to the raw image, just after it has been demosaiced" )]
  public const ushort OpcodeList3 = 0xc74e;

  [MetadataTagDetails( "Noise Profile", "Describes the amount of noise in a raw image" )]
  public const ushort NoiseProfile = 0xc761;

  [MetadataTagDetails( "Time Codes", "Contains an ordered array of time codes" )]
  public const ushort TimeCodes = 0xc763;

  [MetadataTagDetails( "Frame Rate", "Specifies the video frame rate in number of image frames per second" )]
  public const ushort FrameRate = 0xc764;

  [MetadataTagDetails( "T Stop", "Specifies the T-Stop of the actual lens" )]
  public const ushort TStop = 0xc772;

  [MetadataTagDetails( "Reel Name", "Specifies a name for a sequence of images, where each image in the sequence has a unique image identifier (including but not limited to file name, frame number, date time or time code)" )]
  public const ushort ReelName = 0xc789;

  [MetadataTagDetails( "Camera Label", "Specifies a text label for how the camera is used or assigned in this clip" )]
  public const ushort CameraLabel = 0xc7a1;

  [MetadataTagDetails( "Original Default Final Size", "Specifies the default final size of the larger original file from which this proxy was generated" )]
  public const ushort OriginalDefaultFinalSize = 0xc791;

  [MetadataTagDetails( "Original Best Quality Final Size", "Specifies the best quality final size of the larger original file from which this proxy was generated" )]
  public const ushort OriginalBestQualityFinalSize = 0xc792;

  [MetadataTagDetails( "Original Default Crop Size", "Specifies the DefaultCropSize of the larger original file from which this proxy was generated" )]
  public const ushort OriginalDefaultCropSize = 0xc793;

  [MetadataTagDetails( "Profile Hue/Saturation Mapping Encoding", "Provides a way for color profiles to specify how indexing into a 3D HueSatMap is performed during raw conversion" )]
  public const ushort ProfileHueSatMapEncoding = 0xc7a3;

  [MetadataTagDetails( "Profile LookTable Encoding", "Provides a way for color profiles to specify how indexing into a 3D LookTable is performed during raw conversion" )]
  public const ushort ProfileLookTableEncoding = 0xc7a4;

  [MetadataTagDetails( "Baseline Exposure Offset", "Provides a way for color profiles to increase or decrease exposure during raw conversion" )]
  public const ushort BaselineExposureOffset = 0xc7a5;

  /// <summary>
  /// If set to Auto; the raw converter should perform black subtraction during rendering.
  /// If set to None; the raw converter should not perform any black subtraction during rendering.
  /// </summary>
  [MetadataTagDetails( "Default Black Render", "Provides a hint to the raw converter regarding how to handle the black point (e.g. flare subtraction) during rendering" )]
  public const ushort DefaultBlackRender = 0xc7a6;

  [MetadataTagDetails( "New Raw Image Digest", "Specifies a modified MD5 digest of the raw image data" )]
  public const ushort NewRawImageDigest = 0xc7a7;

  [MetadataTagDetails( "Raw To Preview Gain", "The gain (what number the sample values are multiplied by) between the main raw IFD and the preview IFD containing this tag" )]
  public const ushort RawToPreviewGain = 0xc7a8;

  /// <summary>
  /// The values must satisfy: 0.0 <= top < bottom <= 1.0, 0.0 <= left < right <= 1.0.
  /// The default values of (top = 0; left = 0; bottom = 1; right = 1) correspond exactly
  /// to the default crop rectangle (as specified by the DefaultCropOrigin and
  /// DefaultCropSize tags).
  /// </summary>
  [MetadataTagDetails( "Default User Crop", "Specifies a default user crop rectangle in relative coordinates" )]
  public const ushort DefaultUserCrop = 0xc7b5;

  /// <summary>
  /// Can be unknown (apart from nearer distances being closer to zero, and farther
  /// distances being closer to the maximum value), linear (values vary linearly from
  /// zero representing DepthNear to the maximum value representing DepthFar) or inverse
  /// (values are stored inverse linearly, with zero representing DepthNear
  /// and the maximum value representing DepthFar).
  /// </summary>
  [MetadataTagDetails( "Depth Format", "Specifies the encoding of any depth data in the file" )]
  public const ushort DepthFormat = 0xc7e9;

  /// <summary>
  /// 0/0 means unknown
  /// </summary>
  [MetadataTagDetails( "Depth Near", "Specifies distance from the camera represented by the zero value in the depth map" )]
  public const ushort DepthNear = 0xc7ea;

  /// <summary>
  /// 0/0 means unknown. 1/0 means infinity; which is valid for unknown and inverse depth formats
  /// </summary>
  [MetadataTagDetails( "Depth Far", "Specifies distance from the camera represented by the maximum value in the depth map" )]
  public const ushort DepthFar = 0xc7eb;

  [MetadataTagEnum( 0, "Unknown" )]
  [MetadataTagEnum( 1, "Meters" )]
  [MetadataTagDetails( "Depth Units", "Specifies the measurement units for the DepthNear and DepthFar tags" )]
  public const ushort DepthUnits = 0xc7ec;

  [MetadataTagEnum( 0, "Unknown" )]
  [MetadataTagEnum( 1, "Optical Axis" )]
  [MetadataTagEnum( 2, "Optical Ray" )]
  [MetadataTagDetails( "Depth Measure Type", "Specifies the measurement geometry for the depth map" )]
  public const ushort DepthMeasureType = 0xc7ed;

  [MetadataTagDetails( "Enhance Params", "Documents how the enhanced image data was processed" )]
  public const ushort EnhanceParams = 0xc7ee;

  [MetadataTagDetails( "Profile Gain Table Map", "Contains spatially varying gain tables that can be applied while processing the image as a starting point for user adjustments" )]
  public const ushort ProfileGainTableMap = 0xcd2d;

  [MetadataTagDetails( "Semantic Name", "Identifies the semantic mask" )]
  public const ushort SemanticName = 0xcd2e;

  [MetadataTagDetails( "Semantic Instance ID", "Identifies a specific instance in a semantic mask" )]
  public const ushort SemanticInstanceID = 0xcd30;

  [MetadataTagDetails( "Calibration Illuminant 3", "Defines values for the optional third set of color calibration tags (ColorMatrix3, CameraCalibration3, ReductionMatrix3)" )]
  public const ushort CalibrationIlluminant3 = 0xcd31;

  [MetadataTagDetails( "Camera Calibration 3", "Defines a calibration matrix that transforms reference camera native space values to individual camera native space values under the third calibration illuminant" )]
  public const ushort CameraCalibration3 = 0xcd32;

  [MetadataTagDetails( "Color Matrix 3", "Defines a transformation matrix that converts XYZ values to reference camera native color space" )]
  public const ushort ColorMatrix3 = 0xcd33;

  [MetadataTagDetails( "Forward Matrix 3", "Defines a matrix that maps white balanced camera colors to XYZ D50 colors" )]
  public const ushort ForwardMatrix3 = 0xcd34;

  [MetadataTagDetails( "Illuminant Data 1", "Specifies the data for the third illuminant when CalibrationIlluminant1 is set to 255" )]
  public const ushort IlluminantData1 = 0xcd35;

  [MetadataTagDetails( "Illuminant Data 2", "Specifies the data for the third illuminant when CalibrationIlluminant2 is set to 255" )]
  public const ushort IlluminantData2 = 0xcd36;

  [MetadataTagDetails( "Illuminant Data 3", "Specifies the data for the third illuminant when CalibrationIlluminant3 is set to 255" )]
  public const ushort IlluminantData3 = 0xcd37;

  [MetadataTagDetails( "Profile Hue/Saturation Mapping Data 3", "Contains the data for the third hue/saturation/value mapping table" )]
  public const ushort ProfileHueSatMapData3 = 0xcd39;

  [MetadataTagDetails( "Reduction Matrix 3", "Defines a dimensionality reduction matrix for use as the first stage in converting color camera native space values to XYZ values" )]
  public const ushort ReductionMatrix3 = 0xcd3a;


  //
  // Photo section

  [MetadataTagEnum( 1, "Standard output sensitivity" )]
  [MetadataTagEnum( 2, "Recommended exposure index" )]
  [MetadataTagEnum( 3, "ISO speed" )]
  [MetadataTagEnum( 4, "Standard output sensitivity and recommended exposure index" )]
  [MetadataTagEnum( 5, "Standard output sensitivity and ISO speed" )]
  [MetadataTagEnum( 6, "Recommended exposure index and ISO speed" )]
  [MetadataTagEnum( 7, "Standard output sensitivity, recommended exposure index and ISO speed" )]
  [MetadataTagDetails( "Photo Sensitivity Type", "Indicates which one of the parameters of ISO12232 is written in the ISOSpeedRatings tag" )]
  public const ushort PhotoSensitivityType = 0x8830;

  [MetadataTagDetails( "Photo Standard Output Sensitivity", "Indicates the standard output sensitivity value of a camera or input device defined in ISO 12232" )]
  public const ushort PhotoStandardOutputSensitivity = 0x8831;

  [MetadataTagDetails( "Photo Recommended Exposure Index", "Indicates the recommended exposure index value of a camera or input device defined in ISO 12232" )]
  public const ushort PhotoRecommendedExposureIndex = 0x8832;

  [MetadataTagDetails( "Photo ISO Speed", "Indicates the ISO speed value of a camera or input device that is defined in ISO 12232" )]
  public const ushort PhotoISOSpeed = 0x8833;

  [MetadataTagDetails( "Photo ISO Speed Latitude yyy", "Indicates the ISO speed latitude yyy value of a camera or input device that is defined in ISO 12232" )]
  public const ushort PhotoISOSpeedLatitudeyyy = 0x8834;

  [MetadataTagDetails( "Photo ISO Speed Latitude zzz", "Indicates the ISO speed latitude zzz value of a camera or input device that is defined in ISO 12232" )]
  public const ushort PhotoISOSpeedLatitudezzz = 0x8835;

  [MetadataTagDetails( "Photo Exif Version", "Records version of this standard which is supported" )]
  public const ushort PhotoExifVersion = 0x9000;

  [MetadataTagDetails( "Photo Date Time Digitized", "Records date and time when the image was stored as digital data" )]
  public const ushort PhotoDateTimeDigitized = 0x9004;

  [MetadataTagDetails( "Photo Offset Time", "Records time difference from UTC including daylight saving time of DateTime tag" )]
  public const ushort PhotoOffsetTime = 0x9010;

  [MetadataTagDetails( "Photo Offset Time Original", "Records time difference from UTC including daylight saving time of DateTimeOriginal tag" )]
  public const ushort PhotoOffsetTimeOriginal = 0x9011;

  [MetadataTagDetails( "Photo Offset Time Digitized", "Records time difference from UTC including daylight saving time of DateTimeDigitized tag" )]
  public const ushort PhotoOffsetTimeDigitized = 0x9012;

  [MetadataTagDetails( "Photo Components Configuration", "Records information specific to compressed data" )]
  public const ushort PhotoComponentsConfiguration = 0x9101;

  [MetadataTagDetails( "Photo Maker Note", "Records additional desired information from the creator of the Exif data" )]
  public const ushort PhotoMakerNote = 0x927c;

  [MetadataTagDetails( "Photo User Comment", "Records additional keywords or comments on the image besides those in ImageDescription" )]
  public const ushort PhotoUserComment = 0x9286;

  [MetadataTagDetails( "Photo Sub Sec Time", "Records fractions of seconds for DateTime" )]
  public const ushort PhotoSubSecTime = 0x9290;

  [MetadataTagDetails( "Photo Sub Sec Time Original", "Records fractions of seconds for DateTimeOriginal" )]
  public const ushort PhotoSubSecTimeOriginal = 0x9291;

  [MetadataTagDetails( "Photo Sub Sec Time Digitized", "Records fractions of seconds for DateTimeDigitized" )]
  public const ushort PhotoSubSecTimeDigitized = 0x9292;

  [MetadataTagDetails( "Photo Temperature", "Temperature as the ambient situation at the shot in degrees C" )]
  public const ushort PhotoTemperature = 0x9400;

  [MetadataTagDetails( "Photo Humidity", "Humidity as the ambient situation at the shot in %" )]
  public const ushort PhotoHumidity = 0x9401;

  [MetadataTagDetails( "Photo Pressure", "Pressure as the ambient situation at the shot in hPa" )]
  public const ushort PhotoPressure = 0x9402;

  [MetadataTagDetails( "Photo Water Depth", "Water depth as the ambient situation at the shot in m" )]
  public const ushort PhotoWaterDepth = 0x9403;

  [MetadataTagDetails( "Photo Acceleration", "Acceleration as the ambient situation at the shot in mGal (10e-5 m/s^2)" )]
  public const ushort PhotoAcceleration = 0x9404;

  [MetadataTagDetails( "Photo Camera Elevation Angle", "Records the angle of the orientation of the camera (imaging optical axis) as the ambient situation at the shot in degrees" )]
  public const ushort PhotoCameraElevationAngle = 0x9405;

  [MetadataTagDetails( "Photo Flashpix Version", "FlashPix format version supported by a FPXR file" )]
  public const ushort PhotoFlashpixVersion = 0xa000;

  [MetadataTagDetails( "Photo Color Space", "Records the color space information" )]
  public const ushort PhotoColorSpace = 0xa001;

  [MetadataTagDetails( "Photo Pixel X Dimension", "Records the valid width of the meaningful image when it has been compressed" )]
  public const ushort PhotoPixelXDimension = 0xa002;

  [MetadataTagDetails( "Photo Pixel Y Dimension", "Records the valid height of the meaningful image when it has been compressed" )]
  public const ushort PhotoPixelYDimension = 0xa003;

  [MetadataTagDetails( "Photo Related Sound File", "Records the name of an audio file (without path) related to the image data" )]
  public const ushort PhotoRelatedSoundFile = 0xa004;

  [MetadataTagDetails( "Photo Flash Energy", "Indicates the strobe energy at the time the image is captured; as measured in Beam Candle Power Seconds (BCPS)" )]
  public const ushort PhotoFlashEnergy = 0xa20b;

  [MetadataTagDetails( "Photo Spatial Frequency Response", "Records the camera or input device spatial frequency table and SFR values in the direction of image width, image height and diagonal direction" )]
  public const ushort PhotoSpatialFrequencyResponse = 0xa20c;

  [MetadataTagDetails( "Photo Focal Plane X Resolution", "Indicates the number of pixels in the image width (X) direction per FocalPlaneResolutionUnit on the camera focal plane" )]
  public const ushort PhotoFocalPlaneXResolution = 0xa20e;

  [MetadataTagDetails( "Photo Focal Plane Y Resolution", "Indicates the number of pixels in the image height (Y) direction per FocalPlaneResolutionUnit on the camera focal plane" )]
  public const ushort PhotoFocalPlaneYResolution = 0xa20f;

  [MetadataTagEnum( 1, "None" )]
  [MetadataTagEnum( 2, "inches" )]
  [MetadataTagEnum( 3, "cm" )]
  [MetadataTagEnum( 4, "mm" )]
  [MetadataTagEnum( 5, "um" )]
  [MetadataTagDetails( "Photo Focal Plane Resolution Unit", "Indicates the unit for measuring FocalPlaneXResolution and FocalPlaneYResolution. This value is the same as the ResolutionUnit tag" )]
  public const ushort PhotoFocalPlaneResolutionUnit = 0xa210;

  [MetadataTagDetails( "Photo Subject Location", "Indicates the location of the main subject in the scene. The value represents the pixel at the center of the main subject relative to the left edge, prior to rotation processing. The first value indicates the X and second indicates the Y" )]
  public const ushort PhotoSubjectLocation = 0xa214;

  [MetadataTagDetails( "Photo Exposure Index", "Indicates the exposure index selected on the camera or input device at the time the image is captured" )]
  public const ushort PhotoExposureIndex = 0xa215;

  [MetadataTagEnum( 1, "Not defined" )]
  [MetadataTagEnum( 2, "One-chip color area" )]
  [MetadataTagEnum( 3, "Two-chip color area" )]
  [MetadataTagEnum( 4, "Three-chip color area" )]
  [MetadataTagEnum( 5, "Color sequential area" )]
  [MetadataTagEnum( 7, "Trilinear" )]
  [MetadataTagEnum( 8, "Color sequential linear" )]
  [MetadataTagDetails( "Photo Sensing Method", "Indicates the image sensor type on the camera or input device" )]
  public const ushort PhotoSensingMethod = 0xa217;

  [MetadataTagEnum( 1, "Film Scanner" )]
  [MetadataTagEnum( 2, "Reflection Print Scanner" )]
  [MetadataTagEnum( 3, "Digital Camera" )]
  [MetadataTagDetails( "Photo File Source", "Indicates the image source" )]
  public const ushort PhotoFileSource = 0xa300;

  [MetadataTagDetails( "Photo Scene Type", "Indicates the type of scene. If a DSC recorded the image it will be set to 1, indicating that the image was directly photographed" )]
  public const ushort PhotoSceneType = 0xa301;

  [MetadataTagDetails( "Photo CFA Pattern", "Indicates the color filter array (CFA) geometric pattern of the image sensor when a one-chip color area sensor is used" )]
  public const ushort PhotoCFAPattern = 0xa302;

  [MetadataTagEnum( 0, "Normal" )]
  [MetadataTagEnum( 1, "Custom" )]
  [MetadataTagEnum( 2, "HDR (no original saved)" )]
  [MetadataTagEnum( 3, "HDR (original saved)" )]
  [MetadataTagEnum( 4, "Original (for HDR)" )]
  [MetadataTagEnum( 6, "Panorama" )]
  [MetadataTagEnum( 7, "Portrait HDR" )]
  [MetadataTagEnum( 8, "Portrait" )]
  [MetadataTagDetails( "Photo Custom Rendered", "Indicates the use of special processing on image data; such as rendering geared to output" )]
  public const ushort PhotoCustomRendered = 0xa401;

  /// <summary>
  /// This tag indicates the exposure mode set when the image was shot. In auto-bracketing mode; the camera shoots a series of frames of the same scene at different exposure settings.
  /// </summary>
  [MetadataTagEnum( 0, "Auto" )]
  [MetadataTagEnum( 1, "Manual" )]
  [MetadataTagEnum( 2, "Auto bracket" )]
  [MetadataTagDetails( "Photo Exposure Mode", "Indicates the exposure mode set when the image was shot" )]
  public const ushort PhotoExposureMode = 0xa402;

  [MetadataTagEnum( 0, "Auto" )]
  [MetadataTagEnum( 1, "Manual" )]
  [MetadataTagDetails( "Photo White Balance", "Indicates the white balance mode set when the image was shot" )]
  public const ushort PhotoWhiteBalance = 0xa403;

  [MetadataTagDetails( "Photo Digital Zoom Ratio", "Indicates the digital zoom ratio when the image was shot; 0 indeicates that Digital Zoom was not used" )]
  public const ushort PhotoDigitalZoomRatio = 0xa404;

  [MetadataTagDetails( "Photo Focal Length In 35mm Film", "Indicates the equivalent focal length, assuming a 35mm film camera, in mm" )]
  public const ushort PhotoFocalLengthIn35mmFilm = 0xa405;

  [MetadataTagEnum( 0, "Standard" )]
  [MetadataTagEnum( 1, "Landscape" )]
  [MetadataTagEnum( 2, "Portrait" )]
  [MetadataTagEnum( 3, "Night" )]
  [MetadataTagEnum( 4, "Other" )]
  [MetadataTagDetails( "Photo Scene Capture Type", "Indicates the type of scene that was shot, or the mode in which the image was shot" )]
  public const ushort PhotoSceneCaptureType = 0xa406;

  [MetadataTagEnum( 0, "None" )]
  [MetadataTagEnum( 1, "Low gain up" )]
  [MetadataTagEnum( 2, "High gain up" )]
  [MetadataTagEnum( 3, "Low gain down" )]
  [MetadataTagEnum( 4, "High gain down" )]
  [MetadataTagDetails( "Photo Gain Control", "Indicates the degree of overall image gain adjustment" )]
  public const ushort PhotoGainControl = 0xa407;

  [MetadataTagEnum( 0, "Normal" )]
  [MetadataTagEnum( 1, "Low" )]
  [MetadataTagEnum( 2, "High" )]
  [MetadataTagDetails( "Photo Contrast", "Indicates the direction of contrast processing applied by the camera when the image was shot" )]
  public const ushort PhotoContrast = 0xa408;

  [MetadataTagEnum( 0, "Normal" )]
  [MetadataTagEnum( 1, "Low" )]
  [MetadataTagEnum( 2, "High" )]
  [MetadataTagDetails( "Photo Saturation", "Indicates the direction of saturation processing applied by the camera when the image was shot" )]
  public const ushort PhotoSaturation = 0xa409;

  [MetadataTagEnum( 0, "Normal" )]
  [MetadataTagEnum( 1, "Soft" )]
  [MetadataTagEnum( 2, "Hard" )]
  [MetadataTagDetails( "Photo Sharpness", "Indicates the direction of sharpness processing applied by the camera when the image was shot" )]
  public const ushort PhotoSharpness = 0xa40a;

  [MetadataTagDetails( "Photo Device Setting Description", "Indicates information on the picture-taking conditions of a particular camera model" )]
  public const ushort PhotoDeviceSettingDescription = 0xa40b;

  [MetadataTagEnum( 0, "Unknown" )]
  [MetadataTagEnum( 1, "Macro" )]
  [MetadataTagEnum( 2, "Close" )]
  [MetadataTagEnum( 2, "Distant" )]
  [MetadataTagDetails( "Photo Subject Distance Range", "Indicates the distance to the subject" )]
  public const ushort PhotoSubjectDistanceRange = 0xa40c;

  [MetadataTagDetails( "Photo Image Unique ID", "Indicates an identifier assigned uniquely to each image" )]
  public const ushort PhotoImageUniqueID = 0xa420;

  [MetadataTagDetails( "Photo Camera Owner Name", "Records the owner of a camera used" )]
  public const ushort PhotoCameraOwnerName = 0xa430;

  [MetadataTagDetails( "Photo Body Serial Number", "Records the serial number of the body of the camera that was used" )]
  public const ushort PhotoBodySerialNumber = 0xa431;

  [MetadataTagDetails( "Photo Lens Specification", "Notes minimum focal length, maximum focal length, minimum F number in the minimum focal length and minimum F number in the maximum focal length, for the lens that was used" )]
  public const ushort PhotoLensSpecification = 0xa432;

  [MetadataTagDetails( "Photo Lens Make", "Records the lens manufacturer" )]
  public const ushort PhotoLensMake = 0xa433;

  [MetadataTagDetails( "Photo Lens Model", "Records the lens's model name and model number" )]
  public const ushort PhotoLensModel = 0xa434;

  [MetadataTagDetails( "Photo Lens Serial Number", "Records the serial number of the interchangeable lens that was used" )]
  public const ushort PhotoLensSerialNumber = 0xa435;

  [MetadataTagDetails( "Photo Composite Image", "Indicates whether the recorded image is a composite image or not" )]
  public const ushort PhotoCompositeImage = 0xa460;

  [MetadataTagDetails( "Photo Source Image Number Of Composite Image", "Indicates the number of the source images captured for a composite Image" )]
  public const ushort PhotoSourceImageNumberOfCompositeImage = 0xa461;

  [MetadataTagDetails( "PhotoSource Exposure Times Of Composite Image", "Records the parameters relating to exposure time of the exposures for generating the image" )]
  public const ushort PhotoSourceExposureTimesOfCompositeImage = 0xa462;

  [MetadataTagDetails( "Photo Gamma", "Indicates the value of coefficient gamma" )]
  public const ushort PhotoGamma = 0xa500;


  //
  // GPS section

  /// <summary>
  /// This tag is mandatory when <GPSInfo> tag is present. (Note: The <GPSVersionID> tag
  /// is given in bytes; unlike the <ExifVersion> tag. When the version is 2.0.0.0;
  /// the tag value is 02000000.H).
  /// </summary>
  [MetadataTagDetails( "GPS Version ID", "Indicates the version of the GPS Info IFD" )]
  public const ushort GPSVersionID = 0x0000;

  [MetadataTagEnum( "N", "North Latitude" )]
  [MetadataTagEnum( "S", "South Latitude" )]
  [MetadataTagDetails( "GPS Latitude Reference", "Indicates whether the latitude is North or South latitude" )]
  public const ushort GPSLatitudeRef = 0x0001;

  [MetadataTagDetails( "GPS Latitude", "Indicates the latitude as three Rational values giving the degress, minutes and seconds respectively" )]
  public const ushort GPSLatitude = 0x0002;

  [MetadataTagEnum( "W", "West Longitude" )]
  [MetadataTagEnum( "E", "East Longitude" )]
  [MetadataTagDetails( "GPS Longitude Reference", "Indicates whether the longitude is East or West longitude" )]
  public const ushort GPSLongitudeRef = 0x0003;

  [MetadataTagDetails( "GPS Longitude", "Indicates the longitude as three Rational values giving the degress, minutes and seconds respectively" )]
  public const ushort GPSLongitude = 0x0004;

  [MetadataTagEnum( 0, "Altitude above sea level" )]
  [MetadataTagEnum( 1, "Altitude below sea level" )]
  [MetadataTagDetails( "GPS Altitude Reference", "Indicates the altitude reference used for the GPSAltitude value" )]
  public const ushort GPSAltitudeRef = 0x0005;

  [MetadataTagDetails( "GPS Altitude", "Indicates the altitude based on the reference in GPSAltitudeRef and expressed in meters" )]
  public const ushort GPSAltitude = 0x0006;

  [MetadataTagDetails( "GPS TimeStamp", "Indicates the UTC time expressed as three Rational values giving the hour, minute and second" )]
  public const ushort GPSTimeStamp = 0x0007;

  [MetadataTagDetails( "GPS Satellites", "Indicates the GPS satellites used for measurements; the tag can be used to describe the number of satellites, their ID number, angle of elevation, azimuth, SNR and other information" )]
  public const ushort GPSSatellites = 0x0008;

  [MetadataTagEnum( "A", "Measurement is in-progress" )]
  [MetadataTagEnum( "V", "Measurement is Interoperability" )]
  [MetadataTagDetails( "GPS Status", "Indicates the status of the GPS receiver when the image is recorded" )]
  public const ushort GPSStatus = 0x0009;

  [MetadataTagEnum( "2", "Two-dimensional measurement" )]
  [MetadataTagEnum( "3", "Three-dimensional measurement" )]
  [MetadataTagDetails( "GPS Measurement Mode", "Indicates the GPS measurement mode" )]
  public const ushort GPSMeasureMode = 0x000a;

  [MetadataTagDetails( "GPS Data Degree of Precision", "Indicates the GPS data degree of precision. An HDOP value is written during two-dimensional measurement; and PDOP during three-dimensional measurement" )]
  public const ushort GPSDOP = 0x000b;

  [MetadataTagEnum( "K", "Kilometers per hour" )]
  [MetadataTagEnum( "M", "Miles per hour" )]
  [MetadataTagEnum( "N", "Knots" )]
  [MetadataTagDetails( "GPS Speed Reference", "Indicates the unit used to express the GPS receiver speed of movement" )]
  public const ushort GPSSpeedRef = 0x000c;

  [MetadataTagDetails( "GPS Speed", "Indicates the speed of GPS receiver movement" )]
  public const ushort GPSSpeed = 0x000d;

  [MetadataTagEnum( "T", "True Direction" )]
  [MetadataTagEnum( "M", "Magnetic Direction" )]
  [MetadataTagDetails( "GPS Track Reference", "Indicates the reference for giving the direction of GPS receiver movement" )]
  public const ushort GPSTrackRef = 0x000e;

  [MetadataTagDetails( "GPS Track", "Indicates the direction of GPS receiver movement" )]
  public const ushort GPSTrack = 0x000f;

  [MetadataTagEnum( "T", "True Direction" )]
  [MetadataTagEnum( "M", "Magnetic Direction" )]
  [MetadataTagDetails( "GPS Image Direction Reference", "Indicates the reference for giving the direction of the image when it is captured" )]
  public const ushort GPSImgDirectionRef = 0x0010;

  [MetadataTagDetails( "GPS Image Direction", "Indicates the direction of the image when it was captured" )]
  public const ushort GPSImgDirection = 0x0011;

  [MetadataTagDetails( "GPS Map Datum", "Indicates the geodetic survey data used by the GPS receiver" )]
  public const ushort GPSMapDatum = 0x0012;

  [MetadataTagEnum( "N", "North Latitude" )]
  [MetadataTagEnum( "S", "South Latitude" )]
  [MetadataTagDetails( "GPS Destination Latitude Reference", "Indicates whether the latitude of the destination point is North or South latitude" )]
  public const ushort GPSDestLatitudeRef = 0x0013;

  [MetadataTagDetails( "GPS Destination Latitude", "Indicates the latitude of the destination point as three Rational values giving the degress, minutes and seconds respectively" )]
  public const ushort GPSDestLatitude = 0x0014;

  [MetadataTagEnum( "W", "West Longitude" )]
  [MetadataTagEnum( "E", "East Longitude" )]
  [MetadataTagDetails( "GPS Destination Longitude Reference", "Indicates whether the longitude of the destination point is East or West longitude" )]
  public const ushort GPSDestLongitudeRef = 0x0015;

  [MetadataTagDetails( "GPS Destination Longitude", "Indicates the longitude of the destination point as three Rational values giving the degress, minutes and seconds respectively" )]
  public const ushort GPSDestLongitude = 0x0016;

  [MetadataTagEnum( "T", "True Direction" )]
  [MetadataTagEnum( "M", "Magnetic Direction" )]
  [MetadataTagDetails( "GPS Destination Bearing Reference", "Indicates the reference used for giving the bearing to the destination point" )]
  public const ushort GPSDestBearingRef = 0x0017;

  [MetadataTagDetails( "GPS Destination Bearing", "Indicates the bearing to the destination point. The range of values is from 0.00 to 359.99" )]
  public const ushort GPSDestBearing = 0x0018;

  [MetadataTagEnum( "K", "Kilometers per hour" )]
  [MetadataTagEnum( "M", "Miles per hour" )]
  [MetadataTagEnum( "N", "Knots" )]
  [MetadataTagDetails( "GPS Destination Distance Reference", "Indicates the unit used to express the distance to the destination point" )]
  public const ushort GPSDestDistanceRef = 0x0019;

  [MetadataTagDetails( "GPS Destination Distance", "Indicates the distance to the destination point" )]
  public const ushort GPSDestDistance = 0x001a;

  [MetadataTagDetails( "GPS Processing Method", "Recording the name of the method used for location finding" )]
  public const ushort GPSProcessingMethod = 0x001b;

  [MetadataTagDetails( "GPS Area Information", "Recording the name of the GPS area" )]
  public const ushort GPSAreaInformation = 0x001c;

  [MetadataTagDetails( "GPS Date Stamp", "Recording date and time information relative to UTC (Coordinated Universal Time)" )]
  public const ushort GPSDateStamp = 0x001d;

  [MetadataTagDetails( "GPS Differential", "Indicates whether differential correction is applied to the GPS receiver" )]
  public const ushort GPSDifferential = 0x001e;

  [MetadataTagDetails( "GPS Horizontal Positioning Error", "Indicates horizontal positioning errors in meters" )]
  public const ushort GPSHPositioningError = 0x001f;


  //
  // Interoperability Section
  // Added 0x2000 to avoid clash with other tags

  /// <summary>
  /// Use "R98" for stating ExifR98 Rules. Four bytes used including the termination
  /// code (NULL). See the separate volume of Recommended Exif Interoperability Rules
  /// (ExifR98) for other tags used for ExifR98
  /// </summary>
  [MetadataTagDetails( "Interoperability Index", "Indicates the identification of the Interoperability rule; 'R98' denotes ExifR98 Rules" )]
  public const ushort InteroperabilityIndex = 0x0001 + ExifConstants.InteroperabilityOffsetFix;

  [MetadataTagDetails( "Interoperability Version", "Indicates the Interoperability version" )]
  public const ushort InteroperabilityVersion = 0x0002 + ExifConstants.InteroperabilityOffsetFix;

  [MetadataTagDetails( "Related Image File Format", "File format of image file" )]
  public const ushort RelatedImageFileFormat = 0x1000 + ExifConstants.InteroperabilityOffsetFix;

  [MetadataTagDetails( "Related Image Width", "Related image width" )]
  public const ushort RelatedImageWidth = 0x1001 + ExifConstants.InteroperabilityOffsetFix;

  [MetadataTagDetails( "Related Image Height", "Related image height" )]
  public const ushort RelatedImageLength = 0x1002 + ExifConstants.InteroperabilityOffsetFix;
}
