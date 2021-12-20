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
  //[DataRow( "high-res.jpg", 2000, 1333, 300, 300, "image/jpeg" )]
  //[DataRow( "large_test.jpg", 1280, 853, 96, 96, "image/jpeg" )]
  //[DataRow( "large_test_progressive.jpg", 1280, 853, 96, 96, "image/jpeg" )]
  //[DataRow( "us.jpg", 64, 42, 96, 96, "image/jpeg" )]
  [DataRow( "sample-image-file-very-large-exif.jpg", 8000, 6000, 72, 72, "image/jpeg" )]
  //[DataRow( "sample-image-file-large.jpg", 7200, 5400, 96, 96, "image/jpeg" )]
  //[DataRow( "sample-image-file-medium.jpg", 5600, 4200, 96, 96, "image/jpeg" )]
  //[DataRow( "sample-image-file-small.jpg", 4000, 3000, 96, 96, "image/jpeg" )]
  //[DataRow( "sample-image-file-very-small.jpg", 2560, 1920, 96, 96, "image/jpeg" )]
  public void Decode_Valid_Jpeg( string filename, int width, int height, int hdpi, int vdpi, string mime )
  {
    // Arrange
    using var stream = new FileStream( $"./Fixtures/Jpeg/{filename}", FileMode.Open, FileAccess.Read );
    var info = new ImageInfo( stream );

    // Act
    var details = info.Decode();

    // Assert
    details.Should().NotBeNull();
    if( details != null )
    {
      details.Width.Should().Be( width );
      details.Height.Should().Be( height );
      details.HorizontalResolution.Should().Be( hdpi );
      details.VerticalResolution.Should().Be( vdpi );
      details.MimeType.Should().Be( mime );
    }
  }
}
