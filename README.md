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

# How can you help?

Contributions are welcome; even if it is just additional test images for subtypes of an image format so that the test suite can be increased.

## Contributions

This project employs the semantic release semantics for commit messages, it is Angular-esque in that there is a type, section and message to each commit message summary in the format `type(Section): Message Summary`

Use the second line of the commit message to describe in further detail the changes which have been made and why.

The release workflow will look at all the commit messages since the last tagged release and will generate a new SemVer release version number based on the highest `type` discovered.

The different types available will reflect the part of the SemVer release number generated: `<major>.<minor>.<patch>` but also how the `CHANGELOG.md` file is automatically created.

### Types Supported

**Major:**
* breaking
* backwards-incompatible
* major

**Minor:**
* feat
* feature
* minor
* perf

**Patch:**
* fix
* patch
* refactor
* revert
* style
* build
* docs
* test
* other

**No version bump:**
* chore
* skip ci

You can also add `skip ci` anywhere in the message for it to be ignored as a semantic release commit.


### Some examples

```
feature(Memory): Converted to use Span<T>
To avoid unnecessary allocations and thus garbage collection pressure
```

```
patch(WebP Fix): Handling of VP8X image resolution
Previously, was retrieving the image resolution incorrectly
```

```
breaking(Public API): Method name changed
To avoid confusion, the public api to retrieve image info has
been modified to better reflect its usage.
```

```
chore(Workflows): Modified pull request workflow
Added checking of commit message semantics
```
