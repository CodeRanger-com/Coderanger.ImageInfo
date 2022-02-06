# Coderanger.ImageInfo

[![Build and Publish](https://github.com/CodeRanger-com/Coderanger.ImageInfo/actions/workflows/build-publish.yml/badge.svg)](https://github.com/CodeRanger-com/Coderanger.ImageInfo/actions/workflows/build-publish.yml) [![Line Coverage Status](./coverage-badge-line.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/) [![Branch Coverage Status](./coverage-badge-branch.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/)

# Overview

Coderanger.ImageInfo is a very simple cross-platform dotnet core .netstandard2.1 library that enables you to inspect images and return back their dimensions and metadata.


##  Supported Formats

Coderanger.ImageInfo is able to decode the dimensions and resolution of the following graphic formats:

* JPEG
* PNG
* WEBP
* BMP _(does not support the storage of metadata)_
* GIF

## Metadata Extraction

Coderanger.ImageInfo can also extract metadata with the following profiles:

* EXIF
* IPTC
* PNG Text
* XMP

Metadata is stored as a Dictionary of Metadata Tag Values against a profile.

It should be noted, however, that XMP data is an exception and is currently stored as a single string Tag Value which contains the unparsed XML, so any parsing of the XMP XML needs to be done outside of this library (for the time being).

## Usage

This library is very easy to use; with just a single static method called with an image stream, and the response being either `NULL` or an `ImageDetails` record containing properties for the various items of image data:

```cs
using var imageStream = new FileStream( "image.jpeg", FileMode.Open, FileAccess.Read );
var imageInfo = ImageInfo.Get( imageStream );

if( imageInfo is not null )
{
  // If imageInfo was decodable, an object is returned with metadata properties
  Debug.WriteLine( $"Width = {imageInfo.Width}" );
  Debug.WriteLine( $"Height = {imageInfo.Height}" );
  Debug.WriteLine( $"Horizontal DPI = {imageInfo.HorizontalResolution}" );
  Debug.WriteLine( $"Vertical DPI = {imageInfo.VerticalResolution}" );
  Debug.WriteLine( $"Mime = {imageInfo.MimeType}" );

  // If there is any metadata in the image, the 'Tags' property
  // will contain the info as a dictionary of profile tag lists

  // For example, the following will output the tags in the 'Exif' profile
  if( imageInfo.Metadata?.TryGetValue( MetadataProfileType.Exif, out var tags ) ?? false && tags is not null )
  {
    foreach( var tag in tags )
    {
      if( tag is not null && tag.HasValue )
      {
        if( tag.TryGetValue( out var metadataValue ) && metadataValue is not null )
        {
          Debug.WriteLine( $"{metadataValue.TagName} = {metadataValue.Value}" );
        }
      }
    }
  }
}
```

## How can you help?

Contributions are welcome; even if it is just additional test images for subtypes of an image format so that the test suite can be increased.

This repository uses Git LFS for storing image files used in the test project; for more information on setting up LFS on your system please visit the [Git Large File Storage](https://git-lfs.github.com/) site.

Find out more on our [how to contribute](CONTRIBUTIONS.md).
