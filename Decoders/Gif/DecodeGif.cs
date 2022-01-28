// -----------------------------------------------------------------------
// <copyright file="DecodeGif.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// GIF 87a or 89a images are little-endian
// Specifications:
// https://en.wikipedia.org/wiki/GIF#File_format
// https://web.archive.org/web/20160304075538/http://qalle.net/gif89a.php
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Gif;

using System.IO;
using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata;
using Coderanger.ImageInfo.Decoders.Metadata.Xmp;

internal class DecodeGif : IDecoder
{
  public static IDecoder? DetectFormat( BinaryReader reader )
  {
    // Validate the stream is long enough
    if( reader.Length() < GifConstants.MagicNumber.Length )
    {
      // Cant be a valid format but perhaps its another one
      return null;
    }

    var header = reader.ReadBytes( GifConstants.MagicNumber.Length );
    if( header.SequenceEqual( GifConstants.MagicNumber ) )
    {
      return new DecodeGif();
    }

    return null;
  }

  public ImageDetails Decode( BinaryReader reader )
  {
    // Stream will be at the position it was last left at when
    // detecting the format, so it will be at the end of the signature
    // header

    // File Header
    //   Signature      = 3 bytes (signature already read)
    //   Version        = 3 bytes
    //   Image width    = 2 bytes
    //   Image height   = 2 bytes

    var header = reader.ReadBytes( 7 ).AsSpan();

    var version = header[ ..3 ].ToArray();
    if( version.SequenceEqual( GifConstants.MagicNumberVersion87 ) )
    {
      return ParseDimensions( reader, header );
    }
    else if( version.SequenceEqual( GifConstants.MagicNumberVersion89 ) )
    {
      var imageInfo = ParseDimensions( reader, header );
      if( imageInfo != null )
      {
        var xmpData = ExtractXmp( reader );
        if( xmpData != null )
        {
          var metadata = new Metadata();
          metadata.AddTag( MetadataProfileType.Xmp, xmpData );

          return new ImageDetails( imageInfo.Width,
                                   imageInfo.Height,
                                   0,
                                   0,
                                   GifConstants.MimeType,
                                   metadata.GetTags() );
        }

        return imageInfo;
      }
    }

    throw ExceptionHelper.Throw( reader, ErrorMessageUnknown );
  }

  private static ImageDetails ParseDimensions( BinaryReader reader, ReadOnlySpan<byte> header )
  {
    var width = DataConversion.UInt16FromBuffer( header.Slice( 3, 2 ), ByteOrder.LittleEndian );
    var height = DataConversion.UInt16FromBuffer( header.Slice( 5, 2 ), ByteOrder.LittleEndian );

    if( width > 0 && height > 0 )
    {
      return new ImageDetails( width, height, 0, 0, GifConstants.MimeType, null );
    }

    throw ExceptionHelper.Throw( reader, ErrorMessage );
  }

  private static IMetadataTypedValue? ExtractXmp( BinaryReader reader )
  {
    // An XMP data block is held within an application extension block
    // with a signature prior to the xml package

    // Xmp Extension Block
    //  Extension Identifier             '0x21' = 1 byte
    //  Extension Label                  '0xFF' = 1 byte
    //  Block Size                       '0x0B' = 1 byte
    //  App Identifier               'XMP Data' = 8 bytes
    //  App Auth Code                     'XMP' = 3 bytes
    //  Xmp Packet                              = ....
    //  Magic Trailer '0x01_FF_FE...0x01_00_00' = 258 bytes

    // Seek until we find the start of our extension block
    var xmpExtensionIdentifierLength = 1 // GifConstants.ExtensionBlock.Label
                                     + 1 // GifConstants.ExtensionBlock.BlockSize.Length
                                     + GifConstants.ExtensionBlock.XmpApplication.Identifier.Length;
    var maxPossibleLength = reader.Length() - xmpExtensionIdentifierLength;

    var foundXmpBlock = false;
    var xmp = new List<byte>();
    var startBlockOffset = 0L;
    var endBlockOffset = 0L;
    while( endBlockOffset == 0 && reader.Position() < maxPossibleLength )
    {
      var data = reader.ReadByte();
      if( !foundXmpBlock && data == GifConstants.ExtensionBlock.Identifier )
      {
        var block = reader.ReadBytes( xmpExtensionIdentifierLength ).AsSpan();
        if( block[ 0 ] == GifConstants.ExtensionBlock.Label 
          && block[ 1 ] == GifConstants.ExtensionBlock.BlockSize
          && block[ 2.. ].SequenceEqual( GifConstants.ExtensionBlock.XmpApplication.Identifier ) )
        {
          startBlockOffset = reader.Position();
          foundXmpBlock = true;
        }
      }
      else if( foundXmpBlock && data == GifConstants.ExtensionBlock.End )
      {
        endBlockOffset = reader.Position();
        break;
      }
      else if( foundXmpBlock && startBlockOffset > 0 )
      {
        xmp.Add( data );
      }
    }

    if( foundXmpBlock && endBlockOffset > startBlockOffset && xmp.Count > 0 )
    {
      var xmpData = XmpTagFactory.Create();
      xmpData.SetValue( xmp.ToArray().AsSpan() );
      return xmpData;
    }

    return null;
  }

  const string ErrorMessage = "Invalid Gif format";
  const string ErrorMessageUnknown = "Invalid Gif format, unsupported version";
}
