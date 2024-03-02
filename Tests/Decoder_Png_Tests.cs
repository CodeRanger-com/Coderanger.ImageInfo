// -----------------------------------------------------------------------
// <copyright file="Decoder_Png_Tests.cs" company="CodeRanger.com">
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
public class Decoder_Png_Tests
{
  [TestMethod]
  [DataRow( "bath.png", 4032, 3024, 72, 72, "image/png" )]
  [DataRow( "budleigh-salterton.png", 4032, 3024, 72, 72, "image/png" )]
  [DataRow( "dakar-rally.png", 6000, 4000, 300, 300, "image/png" )]
  [DataRow( "dakar-rally-bike.png", 2000, 1333, 300, 300, "image/png" )]
  [DataRow( "dakar-rally-toby-price.png", 2560, 1704, 300, 300, "image/png" )]
  [DataRow( "freediving.png", 5760, 3840, 300, 300, "image/png" )]
  public void Validate_Images( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Png/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info is not null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( hdpi );
      info.VerticalDpi.Should().Be( vdpi );
      info.MimeType.Should().Be( mime );

      // Assert tag information
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-metadata" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }

  [TestMethod]
  [DataRow( "bath.png", 4032, 3024, 72, 72, "image/png" )]
  [DataRow( "iptc.png", 70, 70, 300, 300, "image/png" )]
  [DataRow( "metadata.png", 50, 50, 96, 96, "image/png" )]
  [DataRow( "latin1-pngtext.png", 1024, 1024, -1, -1, "image/png" )]
  public void Validate_Images_With_Metadata( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Png/metadata/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info is not null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( hdpi );
      info.VerticalDpi.Should().Be( vdpi );
      info.MimeType.Should().Be( mime );

      // Assert tag information
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-metadata" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }
}
