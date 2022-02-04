// -----------------------------------------------------------------------
// <copyright file="Decoder_Bmp_Tests.cs" company="CodeRanger.com">
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

[TestClass]
public class Decoder_Bmp_Tests
{
  [TestMethod]
  [DataRow( "qr.bmp", 29, 29, 0, 0, "image/bmp" )]
  [DataRow( "test_v2.bmp", 480, 640, 0, 0, "image/bmp" )]
  public void Validate_Version2_Images( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Bmp/Version2/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( hdpi );
      info.VerticalDpi.Should().Be( vdpi );
      info.MimeType.Should().Be( mime );

      // BMP does not support metadata
      info.Metadata?.Should().BeNull();

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }

  [TestMethod]
  [DataRow( "small.bmp", 500, 500, 96, 96, "image/bmp" )]
  [DataRow( "maze.bmp", 701, 481, 0, 0, "image/bmp" )]
  [DataRow( "land.bmp", 1024, 768, 108, 108, "image/bmp" )]
  [DataRow( "bath.bmp", 4032, 3024, 72, 72, "image/bmp" )]
  [DataRow( "budleigh-salterton.bmp", 4032, 3024, 72, 72, "image/bmp" )]
  [DataRow( "dakar-rally.bmp", 6000, 4000, 300, 300, "image/bmp" )]
  [DataRow( "dakar-rally-bike.bmp", 2000, 1333, 300, 300, "image/bmp" )]
  [DataRow( "dakar-rally-toby-price.bmp", 2560, 1704, 300, 300, "image/bmp" )]
  [DataRow( "freediving.bmp", 5760, 3840, 300, 300, "image/bmp" )]
  public void Validate_Version3_Images( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Bmp/Version3/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( hdpi );
      info.VerticalDpi.Should().Be( vdpi );
      info.MimeType.Should().Be( mime );

      // BMP does not support metadata
      info.Metadata?.Should().BeNull();

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }
}
