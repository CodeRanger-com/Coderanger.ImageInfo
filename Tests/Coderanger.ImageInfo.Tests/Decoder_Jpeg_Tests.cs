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
using Coderanger.ImageInfo.Decoders.Exif;

[TestClass]
public class Decoder_Jpeg_Tests
{
  [TestMethod]
  [DataRow( "bath.jpg", 4032, 3024, 72, 72, "image/jpeg" )]
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
  public void Validate_Images( string filename, int width, int height, int hdpi, int vdpi, string mime )
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
      info.ExifProfiles?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );

      // Output data to console
      OutputTags( info.ExifProfiles );
    }
  }

  [TestMethod]
  [DataRow( "DSCN0010.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0012.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0021.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0025.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0027.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0038.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0040.jpg", 640, 480, 300, 300, "image/jpeg" )]
  [DataRow( "DSCN0042.jpg", 640, 480, 300, 300, "image/jpeg" )]
  public void Validate_Images_With_Gps( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Jpeg/gps/{filename}", FileMode.Open, FileAccess.Read );

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
      info.ExifProfiles?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );

      // Output data to console
      OutputTags( info.ExifProfiles );
    }
  }

  [TestMethod]
  [DataRow( "fujifilm-dx10.jpg", 1024, 768, 72, 72, "image/jpeg" )]
  [DataRow( "fujifilm-finepix40i.jpg", 600, 450, 72, 72, "image/jpeg" )]
  [DataRow( "fujifilm-mx1700.jpg", 640, 480, 72, 72, "image/jpeg" )]
  [DataRow( "kodak-dc210.jpg", 640, 480, 216, 216, "image/jpeg" )]
  [DataRow( "kodak-dc240.jpg", 640, 480, 192, 192, "image/jpeg" )]
  [DataRow( "nikon-e950.jpg", 800, 600, 300, 300, "image/jpeg" )]
  [DataRow( "olympus-c960.jpg", 640, 480, 72, 72, "image/jpeg" )]
  [DataRow( "olympus-d320l.jpg", 640, 480, 144, 144, "image/jpeg" )]
  [DataRow( "ricoh-rdc5300.jpg", 896, 600, 72, 72, "image/jpeg" )]
  [DataRow( "sanyo-vpcg250.jpg", 640, 480, 72, 72, "image/jpeg" )]
  [DataRow( "sanyo-vpcsx550.jpg", 640, 480, 72, 72, "image/jpeg" )]
  [DataRow( "sony-cybershot.jpg", 640, 480, 72, 72, "image/jpeg" )]
  [DataRow( "sony-d700.jpg", 672, 512, 72, 72, "image/jpeg" )]
  [DataRow( "sony-powershota5.jpg", 1024, 768, 180, 180, "image/jpeg" )]
  public void Validate_Images_With_Exif( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Jpeg/exif.org/{filename}", FileMode.Open, FileAccess.Read );

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
      info.ExifProfiles?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );

      // Output data to console
      OutputTags( info.ExifProfiles );
    }
  }

  private static void OutputTags( Dictionary<ExifProfileType, List<IExifValue>>? profiles )
  {
    if( profiles == null )
    {
      return;
    }

    foreach( var profile in profiles.Keys )
    {
      Console.Write( $"\n{profile} Tags" );
      if( profiles.TryGetValue( profile, out var profileTags ) && profileTags != null )
      {
        foreach( var exifValue in profileTags )
        {
          if( exifValue == null )
          {
            Console.Write( "\n\t(none)" );
            return;
          }

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

        Console.WriteLine( string.Empty );
      }
      else
      {
        Console.Write( "\n\t(none)" );
      }
    }
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
        Console.Write( $"{(DateOnly)tagValue.Value}" );
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
