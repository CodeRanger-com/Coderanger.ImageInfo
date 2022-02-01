# Overview

[![Build and Publish](https://github.com/CodeRanger-com/Coderanger.Excel/actions/workflows/build-publish.yml/badge.svg)](https://github.com/CodeRanger-com/Coderanger.Excel/actions/workflows/build-publish.yml) [![Line Coverage Status](./coverage-badge-line.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/) [![Branch Coverage Status](./coverage-badge-branch.svg)](https://github.com/danpetitt/open-cover-badge-generator-action/)

Coderanger.Excel is a cross-platform dotnet core .netstandard2.1 library that enables you to generate Microsoft Excel (or compatible) xlsx documents.

The library API has been created for ease of use using a fluent style interface.

Full documentation at https://github.com/CodeRanger-com/Coderanger.Excel



```cs
using var imageStream = new FileStream( "image.jpeg", FileMode.Open, FileAccess.Read );
var imageInfo = ImageInfo.DecodeFromStream( imageStream );

// If imageInfo was decodable, an object is returned with metadata properties
Debug.WriteLine( $"Mime = {imageInfo.MimeType}" );
Debug.WriteLine( $"Width = {imageInfo.Width}" );
Debug.WriteLine( $"Height = {imageInfo.Height}" );

// If there is any metedata in the image, the tags property
// will not be null and will contain the info as a dictionary
// of profile tag lists
```
