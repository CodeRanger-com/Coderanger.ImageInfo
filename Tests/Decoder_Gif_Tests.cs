// -----------------------------------------------------------------------
// <copyright file="Decoder_Gif_Tests.cs" company="CodeRanger.com">
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
public class Decoder_Gif_Tests
{
  [TestMethod]
  [DataRow( "test.gif", 480, 640, "image/gif" )]
  public void Validate_Version_87a_Images( string filename, int width, int height, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Gif/Version87a/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.DecodeFromStream( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( 0 );
      info.VerticalDpi.Should().Be( 0 );
      info.MimeType.Should().Be( mime );

      // Gif v87a does not support metadata
      info.Metadata?.Should().BeNull();
    }
  }

  [TestMethod]
  [DataRow( "test.gif", 480, 640, "image/gif" )]
  [DataRow( "bath.gif", 4032, 3024, "image/gif" )]
  [DataRow( "budleigh-salterton.gif", 4032, 3024, "image/gif" )]
  [DataRow( "dakar-rally.gif", 6000, 4000, "image/gif" )]
  [DataRow( "dakar-rally-bike.gif", 2000, 1333, "image/gif" )]
  [DataRow( "dakar-rally-toby-price.gif", 2560, 1704, "image/gif" )]
  [DataRow( "freediving.gif", 5760, 3840, "image/gif" )]
  public void Validate_Version_89a_Images( string filename, int width, int height, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Gif/Version89a/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.DecodeFromStream( stream );

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
  [DataRow( "bath.gif", 1000, 750, "image/gif" )]
  [DataRow( "test.gif", 480, 640, "image/gif" )]
  public void Validate_Images_With_Xmp( string filename, int width, int height, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Gif/Metadata/{filename}", FileMode.Open, FileAccess.Read );

    // Act
    var info = ImageInfo.DecodeFromStream( stream );

    // Assert
    info.Should().NotBeNull();
    if( info != null )
    {
      info.Width.Should().Be( width );
      info.Height.Should().Be( height );
      info.HorizontalDpi.Should().Be( 0 );
      info.VerticalDpi.Should().Be( 0 );
      info.MimeType.Should().Be( mime );

      // Assert tag information
      info.Metadata?.ShouldMatchChildSnapshot( $"{filename}-metadata" );

      // Output data to console
      MetadataHelpers.Output( info.Metadata );
    }
  }
}
