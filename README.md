# Overview

[![Build and Publish](https://github.com/CodeRanger-com/Coderanger.ImageInfo/actions/workflows/build-publish.yml/badge.svg)](https://github.com/CodeRanger-com/Coderanger.ImageInfo/actions/workflows/build-publish.yml) [![Line Coverage Status](./coverage-badge-line.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/) [![Branch Coverage Status](./coverage-badge-branch.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/)

Coderanger.ImageInfo is a very simple cross-platform dotnet core .netstandard2.1 library that enables you to inspect images and return back dimensions and metadata.


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
