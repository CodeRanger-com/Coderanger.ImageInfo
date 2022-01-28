// -----------------------------------------------------------------------
// <copyright file="DecodeBmp.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// BMP images are little endian being a Microsoft created format
// BMP does not support Exif, IPTC or XMP metadata
// Specifications:
// https://en.wikipedia.org/wiki/BMP_file_format
// http://www.dragonwins.com/domains/getteched/bmp/bmpfileformat.htm
// https://www.fileformat.info/format/bmp/egff.htm
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Bmp;

using System.IO;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal class DecodeBmp : IDecoder
{
  public static IDecoder? DetectFormat( BinaryReader reader )
  {
    // Validate the stream is long enough
    if( reader.Length() < BmpConstants.MagicNumber.Length )
    {
      // Cant be a valid format but perhaps its another one
      return null;
    }

    var header = reader.ReadBytes( BmpConstants.MagicNumber.Length );
    var headerValue = DataConversion.UInt16FromBigEndianBuffer( header.AsSpan() );
    if( headerValue == BmpConstants.MagicNumberValue )
    {
      return new DecodeBmp();
    }

    return null;
  }

  public ImageDetails Decode( BinaryReader reader )
  {
    // Stream will be at the position it was last left at when
    // detecting the format, so it will be at the end of the signature
    // header

    // File Header (14 bytes total)
    //   Signature              = 2 bytes
    //   File size (compressed) = 4 bytes
    //   Reserved               = 2 bytes
    //   Reserved               = 2 bytes
    //   Image data offset      = 4 bytes

    // Ignore the next 12 bytes of the header (signature already read)
    reader.Skip( 12 );

    // Start of the Info Header is always the size of header
    var headerSize = reader.ReadInt32();
    if( headerSize == BmpConstants.HeaderSize.OS2Version2 )
    {
      return ExtractVersion2Format( reader );
    }
    else if( headerSize == BmpConstants.HeaderSize.WinVersion3
      || headerSize == BmpConstants.HeaderSize.AdobeVersion3a
      || headerSize == BmpConstants.HeaderSize.AdobeVersion3b
      || headerSize == BmpConstants.HeaderSize.WinVersion4
      || headerSize == BmpConstants.HeaderSize.Version5 )
    {
      // All these versions use the same basic info header structure
      // up to the point where the resolution info exists and only
      // really changes after that
      return ExtractVersion3Format( reader );
    }

    throw ExceptionHelper.Throw( reader, ErrorMessageUnknown );
  }

  private ImageDetails ExtractVersion2Format( BinaryReader reader )
  {
    // v2 Info Header (12 bytes total)
    //   Size of header = 4 bytes (already read in)
    //   Image width    = 2 bytes
    //   Image height   = 2 bytes
    //   Color Planes   = 2 bytes
    //   Bits per Pixel = 2 bytes

    var buffer = reader.ReadBytes( 4 ).AsSpan();
    var width = DataConversion.UInt16FromBuffer( buffer[ ..2 ], ByteOrder.LittleEndian );
    var height = DataConversion.UInt16FromBuffer( buffer.Slice( 2, 2 ), ByteOrder.LittleEndian );

    if( width > 0 && height > 0 )
    {
      return new ImageDetails( width, height, 0, 0, BmpConstants.MimeType, null );
    }

    throw ExceptionHelper.Throw( reader, ErrorMessage );
  }

  private ImageDetails ExtractVersion3Format( BinaryReader reader )
  {
    // v3 or v5 Info Header (40 bytes total)
    //   Size of header                     = 4 bytes (already read in)
    //   Image width                        = 4 bytes
    //   Image height                       = 4 bytes
    //   Color Planes                       = 2 bytes
    //   Bits per Pixel                     = 2 bytes
    //   Compression Method                 = 4 bytes
    //   Image Size of raw data   )         = 4 bytes
    //   Horizontal Resolution in pixels/pm = 4 bytes
    //   Vertical Resolution in pixels/pm   = 4 bytes
    //   Number of colors                   = 4 bytes
    //   Number of important colors         = 4 bytes

    // Only need up to resolution which is 28 bytes; this isnt
    // much so may as well read it in one chunk instead of seeking
    var buffer = reader.ReadBytes( 28 ).AsSpan();
    var width = DataConversion.Int32FromBuffer( buffer[ ..4 ], ByteOrder.LittleEndian );
    var height = DataConversion.Int32FromBuffer( buffer.Slice( 4, 4 ), ByteOrder.LittleEndian );

    var horizontalResolution = DataConversion.Int32FromBuffer( buffer.Slice( 20, 4 ), ByteOrder.LittleEndian );
    var verticalResolution = DataConversion.Int32FromBuffer( buffer.Slice( 24, 4 ), ByteOrder.LittleEndian );

    var hDPI = UnitConvertor.ToDpi( DensityUnit.PixelsPerMeter, horizontalResolution );
    var vDPI = UnitConvertor.ToDpi( DensityUnit.PixelsPerMeter, verticalResolution );

    if( width > 0 && height > 0 )
    {
      return new ImageDetails( width, height, hDPI, vDPI, BmpConstants.MimeType, null );
    }

    throw ExceptionHelper.Throw( reader, ErrorMessage );
  }

  const string ErrorMessage = "Invalid BMP format";
  const string ErrorMessageUnknown = "Unknown BMP format version";
}
