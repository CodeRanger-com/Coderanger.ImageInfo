// -----------------------------------------------------------------------
// <copyright file="Decoder_Jpeg_Tests.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Tests;

using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Decoder_Jpeg_Tests
{
  [TestMethod]
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
    }
  }
}
