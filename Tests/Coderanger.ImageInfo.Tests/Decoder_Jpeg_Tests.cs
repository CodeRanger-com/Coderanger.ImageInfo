// -----------------------------------------------------------------------
// <copyright file="Decoder_Jpeg_Tests.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Tests;

using System.IO;
using Coderanger.ImageInfo.Decoders.Exif.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Snapper;

[TestClass]
public class Decoder_Jpeg_Tests
{
  [TestMethod]
  [DataRow( "budleigh-salterton.jpg", 4032, 3024, 72, 72, "image/jpeg" )]
  [DataRow( "high-res.jpg", 2000, 1333, 300, 300, "image/jpeg" )]
  [DataRow( "large_test.jpg", 1280, 853, 96, 96, "image/jpeg" )]
  [DataRow( "large_test_progressive.jpg", 1280, 853, 96, 96, "image/jpeg" )]
  [DataRow( "us.jpg", 64, 42, 96, 96, "image/jpeg" )]
  [DataRow( "sample-image-file-very-large-exif.jpg", 8000, 6000, 72, 72, "image/jpeg" )]
  [DataRow( "sample-image-file-large.jpg", 7200, 5400, 96, 96, "image/jpeg" )]
  [DataRow( "sample-image-file-medium.jpg", 5600, 4200, 96, 96, "image/jpeg" )]
  [DataRow( "sample-image-file-small.jpg", 4000, 3000, 96, 96, "image/jpeg" )]
  [DataRow( "sample-image-file-very-small.jpg", 2560, 1920, 96, 96, "image/jpeg" )]
  [DataRow( "bad_image_end.jpg", 3000, 3000, 72, 72, "image/jpeg" )]
  [DataRow( "freediving.jpeg", 5760, 3840, 300, 300, "image/jpeg" )]
  [DataRow( "dakar-rally.jpg", 6000, 4000, 300, 300, "image/jpeg" )]
  [DataRow( "dakar-rally-bike.jpg", 2000, 1333, 300, 300, "image/jpeg" )]
  [DataRow( "dakar-rally-toby-price.jpg", 2560, 1704, 300, 300, "image/jpeg" )]
  [DataRow( "sample-clouds-400x300.jpg", 400, 300, 350, 350, "image/jpeg" )]
  [DataRow( "sample-birch-400x300.jpg", 400, 300, 350, 350, "image/jpeg" )]
  [DataRow( "sample-city-park-400x300.jpg", 400, 300, 350, 350, "image/jpeg" )]
  public void Decode_Valid_Jpeg( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Jpeg/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.DecodeFromStream( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( hdpi );
      info.VerticalDpi.Should().Be( vdpi );
      info.MimeType.Should().Be( mime );

      // Assert tag information
      info.ExifTags?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );
      info.GpsTags?.ShouldMatchChildSnapshot( $"{filename}-gpstags" );

      // Output data to console
      OutputTags( "EXIF Tags", info.ExifTags );
      OutputTags( "EXIF GPS Tags", info.GpsTags );
    }
  }

  private static void OutputTags( string title, Dictionary<ushort, IExifValue>? tags )
  {
    Console.Write( $"\n{title}" );
    if( tags == null )
    {
      Console.Write( "\n\t(none)" );
      return;
    }

    foreach( var tag in tags.Keys )
    {
      if( tags.TryGetValue( tag, out var exifValue ) && exifValue != null )
      {
        if( exifValue.IsArray )
        {
          if( exifValue.TryGetValueArray( out var tagValues ) && tagValues != null )
          {
            var isFirst = true;
            foreach( var tagValue in tagValues )
            {
              Decoder_Jpeg_Tests.OutputValue( tagValue, outputTagDetails: isFirst, isArray: true );
              isFirst = false;
            }
          }
        }
        else
        {
          if( exifValue.TryGetValue( out var tagValue ) && tagValue != null )
          {
            Decoder_Jpeg_Tests.OutputValue( tagValue, outputTagDetails: true, isArray: false );
          }
        }
      }
    }

    Console.WriteLine( string.Empty );
  }

  private static void OutputValue( ExifTagValue? tagValue, bool outputTagDetails, bool isArray )
  {
    if( tagValue == null || tagValue.Value == null )
    {
      return;
    }

    if( outputTagDetails )
    {
      Console.Write( $"\n\t{tagValue.TagName} (0x{tagValue.TagId:X}) = " );
    }

    switch( tagValue.Type )
    {
      case ExifType.Byte:
        Console.Write( $"{(byte)tagValue.Value}" );
        break;

      case ExifType.Date:
        Console.Write( $"{(DateTime)tagValue.Value}" );
        break;

      case ExifType.DateTime:
        Console.Write( $"{(DateTime)tagValue.Value}" );
        break;

      case ExifType.Float:
        Console.Write( $"{(float)tagValue.Value}" );
        break;

      case ExifType.Int:
        Console.Write( $"{(int)tagValue.Value}" );
        break;

      case ExifType.UInt:
        Console.Write( $"{(uint)tagValue.Value}" );
        break;

      case ExifType.Rational:
      case ExifType.URational:
        var rational = (Rational)tagValue.Value;
        Console.Write( rational );
        break;

      case ExifType.Short:
        Console.Write( $"{(short)tagValue.Value}" );
        break;

      case ExifType.UShort:
        Console.Write( $"{(ushort)tagValue.Value}" );
        break;

      case ExifType.String:
        Console.Write( $"{(string)tagValue.Value}" );
        break;

      default:
        Console.Write( $"unknown value" );
        break;
    }

    if( isArray )
    {
      Console.Write( $", " );
    }
  }
}
