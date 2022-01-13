// -----------------------------------------------------------------------
// <copyright file="Decoder_Jpeg_Tests.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Tests;

using System.IO;
using Coderanger.ImageInfo.Tests.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snapper;

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
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
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
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
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
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-exiftags" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }

  [TestMethod]
  [DataRow( "bath.jpg", 4032, 3024, 72, 72, "image/jpeg" )]
  public void Validate_Images_With_Iptc( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Jpeg/iptc/{filename}", FileMode.Open, FileAccess.Read );

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
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-iptctags" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }
}
