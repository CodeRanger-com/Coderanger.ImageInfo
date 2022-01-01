// -----------------------------------------------------------------------
// <copyright file="MetadataHelpers.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Tests.Helpers;

using System;
using System.Collections.Generic;
using Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;
using Coderanger.ImageInfo.Decoders.Metadata.Exif;
using Coderanger.ImageInfo.Decoders.Metadata;
using Coderanger.ImageInfo.Decoders.Metadata.Png;

internal static class MetadataHelpers
{
  internal static void Output( Dictionary<MetadataProfileType, List<IMetadataTypedValue>>? profiles )
  {
    if( profiles == null )
    {
      return;
    }

    foreach( var profile in profiles.Keys )
    {
      Console.Write( $"\n{profile} Tags" );
      if( profiles.TryGetValue( profile, out var profileTags ) && profileTags != null )
      {
        foreach( var exifValue in profileTags )
        {
          if( exifValue == null )
          {
            Console.Write( "\n\t(none)" );
            return;
          }

          if( exifValue.IsArray )
          {
            if( exifValue.TryGetValueArray( out var tagValues ) && tagValues != null )
            {
              var isFirst = true;
              foreach( var tagValue in tagValues )
              {
                OutputValue( tagValue, outputTagDetails: isFirst, isArray: true );
                isFirst = false;
              }
            }
          }
          else
          {
            if( exifValue.TryGetValue( out var tagValue ) && tagValue != null )
            {
              OutputValue( tagValue, outputTagDetails: true, isArray: false );
            }
          }
        }

        Console.WriteLine( string.Empty );
      }
      else
      {
        Console.Write( "\n\t(none)" );
      }
    }
  }

  private static void OutputValue( MetadataTagValue? tagValue, bool outputTagDetails, bool isArray )
  {
    if( tagValue == null || tagValue.Value == null )
    {
      return;
    }

    if( outputTagDetails )
    {
      Console.Write( $"\n\t{tagValue.TagName} (0x{tagValue.TagId:X}) = " );
    }

    switch( tagValue.Type )
    {
      case MetadataType.Byte:
        Console.Write( $"{(byte)tagValue.Value}" );
        break;

      case MetadataType.Date:
        Console.Write( $"{(DateOnly)tagValue.Value}" );
        break;

      case MetadataType.DateTime:
        Console.Write( $"{(DateTime)tagValue.Value}" );
        break;

      case MetadataType.Float:
        Console.Write( $"{(float)tagValue.Value}" );
        break;

      case MetadataType.Int:
        Console.Write( $"{(int)tagValue.Value}" );
        break;

      case MetadataType.UInt:
        Console.Write( $"{(uint)tagValue.Value}" );
        break;

      case MetadataType.Rational:
      case MetadataType.URational:
        var rational = (Rational)tagValue.Value;
        Console.Write( rational );
        break;

      case MetadataType.Short:
        Console.Write( $"{(short)tagValue.Value}" );
        break;

      case MetadataType.UShort:
        Console.Write( $"{(ushort)tagValue.Value}" );
        break;

      case MetadataType.String:
        Console.Write( $"{(string)tagValue.Value}" );
        break;

      case MetadataType.PngText:
        var val = (PngText)tagValue.Value;
        Console.Write( val );
        break;

      default:
        Console.Write( $"unknown value" );
        break;
    }

    if( isArray )
    {
      Console.Write( $", " );
    }
  }
}
