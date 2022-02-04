// -----------------------------------------------------------------------
// <copyright file="DecodeWebP.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// WebP file format is based on the RIFF (resource interchange file format) document format
// WebP is little endian
// Specifications:
// https://developers.google.com/speed/webp/docs/riff_container#riff_file_format
// https://developers.google.com/speed/webp/docs/webp_lossless_bitstream_specification#2_riff_header
// https://datatracker.ietf.org/doc/html/rfc6386
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.WebP;

using System.Buffers.Binary;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using Coderanger.ImageInfo.Decoders.DecoderUtils;
using Coderanger.ImageInfo.Decoders.Metadata;
using Coderanger.ImageInfo.Decoders.Metadata.Exif;
using Coderanger.ImageInfo.Decoders.Metadata.Xmp;

internal class DecodeWebP : IDecoder
{
  public static IDecoder? DetectFormat( BinaryReader reader )
  {
    // Validate the stream is long enough
    // RIFF format has a RIFF signature, 4 byte file size and 4 byte WEBP signature
    if( reader.Length() < WebPConstants.Riff.MagicNumber.Length + 4 + WebPConstants.WebP.MagicNumber.Length )
    {
      // Cant be a valid format but perhaps its another one
      return null;
    }

    var riffHeader = reader.ReadBytes( WebPConstants.Riff.MagicNumber.Length ).AsSpan();
    reader.Skip( 4 ); // File Size

    var webPHeader = reader.ReadBytes( WebPConstants.WebP.MagicNumber.Length ).AsSpan();
    if( riffHeader.SequenceEqual( WebPConstants.Riff.MagicNumber )
      && webPHeader.SequenceEqual( WebPConstants.WebP.MagicNumber ) )
    {
      return new DecodeWebP();
    }

    return null;
  }

  public ImageDetails Decode( BinaryReader reader )
  {
    // Stream will be at the position it was last left at when
    // detecting the format, so it will be at the end of the signature
    // header

    // File Header
    //   Signature     'RIFF' = 4 bytes
    //   File Size            = 4 bytes
    //   Magic Number  'WEBP' = 4 bytes (header up to here already read)
    //   Format Version       = 4 bytes
    //   Chunk Type           = 4 bytes
    //   Chunk Size           = 4 bytes
    //   Chunk Data           = .......

    const int chunkHeaderLength = 8;
    var width = 0;
    var height = 0;
    _resolutionX = 0;
    _resolutionY = 0;

    do
    {
      var chunkHeader = reader.ReadBytes( chunkHeaderLength ).AsSpan();
      Debug.WriteLine( $"Chunk header = {Encoding.ASCII.GetString( chunkHeader[ ..4 ] )}" );

      var chunkTypeValue = DataConversion.UInt32FromBuffer( chunkHeader[ ..4 ], ByteOrder.LittleEndian );
      var chunkSize = DataConversion.UInt32FromBuffer( chunkHeader[ 4.. ], ByteOrder.LittleEndian );
      var remainingInChunk = chunkSize;

      var currentPosition = reader.Position();
      if( chunkTypeValue == WebPConstants.Chunks.VP8XValue )
      {
        // VP8X Header =  32 bits / 4 bytes:
        //  0/1  = Reserved (Rsv)     = 2 bits
        //  2    = ICC profile (I)    = 1 bit
        //  3    = Alpha (L)          = 1 bit
        //  4    = EXIF metadata (E)  = 1 bit
        //  5    = XMP metadata (X)   = 1 bit
        //  6    = Animation (A)      = 1 bit
        //  7    = Reserved (R)       = 1 bit
        //  8-32 = Reserved           = 24 bits

        var headerBits = new BitArray( reader.ReadBytes( 4 ) );

        // Canvas Width - 1  = 3 bytes
        // Canvas Height - 1 = 3 bytes
        // Values are 3 bytes, but int conversion is expecting 4, so resize arrays
        var widthDimension = reader.ReadBytes( 3 );
        Array.Resize<byte>( ref widthDimension, 4 );

        var heightDimension = reader.ReadBytes( 3 );
        Array.Resize<byte>( ref heightDimension, 4 );

        width = DataConversion.Int32FromBuffer( widthDimension.AsSpan(), ByteOrder.LittleEndian ) + 1;
        height = DataConversion.Int32FromBuffer( heightDimension.AsSpan(), ByteOrder.LittleEndian ) + 1;

        if( !headerBits[ 4 ] && !headerBits[ 5 ] )
        {
          // Neither Exif or XMP chunk so jump out now
          break;
        }
      }
      else if( chunkTypeValue == WebPConstants.Chunks.VP8LValue )
      {
        var signature = reader.ReadByte();
        if( signature != WebPConstants.Chunks.VP8LSignature )
        {
          // Not valid WebP VP8L format
          throw new ArgumentException( ErrorMessage );
        }

        var dimensions = reader.ReadBytes( 4 );

        var converted = BitConverter.ToUInt32( dimensions, 0 );
        width = (int)( ( converted ) & 0x3FFF ) + 1; // mask out second 14 bits
        height = (int)( ( converted >> 14 ) & 0x3FFF ) + 1; // shift right then mask
      }
      else if( chunkTypeValue == WebPConstants.Chunks.VP8Value )
      {
        // Frame information:
        //  Frame Type      = 1 bit
        //  Version         = 3 bit
        //  Show Frame Flag = 1 bit
        //  Partition Size  = 19 bits
        // Total            = 3 bytes
        reader.Skip( 3 );

        var signature = reader.ReadBytes( 3 ).AsSpan();
        if( !signature.SequenceEqual( WebPConstants.Chunks.VP8Signature ) )
        {
          // Not valid WebP VP8 format
          throw new ArgumentException( ErrorMessage );
        }

        // Dimensions
        var dimensions = reader.ReadBytes( 4 );
        var tmp = (uint)BinaryPrimitives.ReadInt16LittleEndian( dimensions.AsSpan( 0 ) );
        width = (int)tmp & 0x3fff;
        tmp = (uint)BinaryPrimitives.ReadInt16LittleEndian( dimensions.AsSpan( 2 ) );
        height = (int)tmp & 0x3fff;
      }
      else if( chunkTypeValue == WebPConstants.Chunks.ExifValue )
      {
        var exif = DecodeExif.DecodeFromReader( reader, false );
        exif?.AddTagsToProfile( ref _metadata );

        if( _resolutionX == 0 && exif?.HorizontalDpi > 0 )
        {
          _resolutionX = exif.HorizontalDpi;
        }

        if( _resolutionY == 0 && exif?.VerticalDpi > 0 )
        {
          _resolutionY = exif.VerticalDpi;
        }
      }
      else if( chunkTypeValue == WebPConstants.Chunks.XmpValue )
      {
        // This cast will be fine, the xmp chunk is not going to be larger
        // than the size of an int so no overflow should occur
        var data = reader.ReadBytes( (int)remainingInChunk );

        XmpTagFactory.AddXmp( data.AsSpan(), ref _metadata );
      }

      var remaining = reader.Position() - currentPosition;
      remainingInChunk -= (uint)remaining;
      reader.Skip( remainingInChunk );

      // Continue if we have enough space in the file for another chunk
    } while( reader.Position() < reader.Length() - chunkHeaderLength );

    if( width > 0 && height > 0 )
    {
      return new ImageDetails( width,
                               height,
                               _resolutionX,
                               _resolutionY,
                               WebPConstants.MimeType,
                               _metadata.GetTags() );
    }

    // Not valid webp format
    throw new ArgumentException( ErrorMessage );
  }

  private int _resolutionX = 0;
  private int _resolutionY = 0;
  private Metadata _metadata = new();

  const string ErrorMessage = "Invalid WebP format";
}
