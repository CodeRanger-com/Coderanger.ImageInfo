// -----------------------------------------------------------------------
// <copyright file="Decoder_WebP_Tests.cs" company="CodeRanger.com">
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
public class Decoder_WebP_Tests
{
  [TestMethod]
  [DataRow( "small.webp", 13, 1, "image/webp" )]
  [DataRow( "Nico-Prien-Pro-Wind-Surfer.webp", 1310, 737, "image/webp" )]
  public void Validate_Version_VP8_Images( string filename, int width, int height, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Webp/VP8/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( 0 );
      info.VerticalDpi.Should().Be( 0 );
      info.MimeType.Should().Be( mime );
      info.Metadata?.Should().BeNull();
    }
  }

  [TestMethod]
  [DataRow( "lossless_small.webp", 100, 31, "image/webp" )]
  public void Validate_Version_VP8L_Images( string filename, int width, int height, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Webp/VP8L/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( 0 );
      info.VerticalDpi.Should().Be( 0 );
      info.MimeType.Should().Be( mime );
      info.Metadata?.Should().BeNull();
    }
  }

  [TestMethod]
  [DataRow( "bath.webp", 4032, 3024, 72, 72, "image/webp" )]
  public void Validate_Version_VP8X_Images( string filename, int width, int height, int xResolution, int yResolution, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Webp/VP8X/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.Get( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( xResolution );
      info.VerticalDpi.Should().Be( yResolution );
      info.MimeType.Should().Be( mime );

      // Assert tag information
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-metadata" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }
}
