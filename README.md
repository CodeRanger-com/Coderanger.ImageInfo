# Overview

[![Build and Publish](https://github.com/CodeRanger-com/Coderanger.Excel/actions/workflows/build-publish.yml/badge.svg)](https://github.com/CodeRanger-com/Coderanger.Excel/actions/workflows/build-publish.yml) [![Line Coverage Status](./coverage-badge-line.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/) [![Branch Coverage Status](./coverage-badge-branch.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/)

Coderanger.Excel is a cross-platform dotnet core .netstandard2.1 library that enables you to generate Microsoft Excel (or compatible) xlsx documents.

The library API has been created for ease of use using a fluent style interface.

Full documentation at https://github.com/CodeRanger-com/Coderanger.Excel



```cs
using var imageStream = new FileStream( "image.jpeg", FileMode.Open, FileAccess.Read );
var imageInfo = ImageInfo.Get( imageStream );

// If imageInfo was decodable, an object is returned with metadata properties
Debug.WriteLine( $"Mime = {imageInfo.MimeType}" );
Debug.WriteLine( $"Width = {imageInfo.Width}" );
Debug.WriteLine( $"Height = {imageInfo.Height}" );

// If there is any metadata in the image, the `Tags` property
// will contain the info as a dictionary of profile tag lists

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

```
