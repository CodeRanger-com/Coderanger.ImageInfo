// -----------------------------------------------------------------------
// <copyright file="ExifTagDescriptionAttribute.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif;

using System;
using System.Reflection;

internal record ExifTagDetails( string Name, string Description );

[AttributeUsage( AttributeTargets.Field, AllowMultiple = false )]
internal sealed class ExifTagDetailsAttribute : Attribute
{
  public ExifTagDetailsAttribute( string name, string description )
  {
    Name = name;
    Description = description;
  }

  public string Name { get; init; }
  public string Description { get; init; }

  /// <summary>
  /// Gets the tag description from any custom attributes
  /// </summary>
  /// <param name="tag">The tag.</param>
  /// <returns>
  /// The <see cref="string"/>.
  /// </returns>
  public static ExifTagDetails? GetTagDetails( ExifTag type, ushort tag )
  {
    var fieldInfos = type.GetType().GetFields(
      // Gets all public and static fields

      BindingFlags.Public | BindingFlags.Static |
      // This tells it to get the fields from all base types as well

      BindingFlags.FlattenHierarchy );

    // Go through the list and only pick out the constants
    foreach( var fi in fieldInfos )
    {
      // * IsLiteral determines if its value is written at compile time and
      //   not changeable
      // * IsInitOnly determines if the field can be set in the body of
      //   the constructor
      // For C# a field which is readonly keyword would have both true 
      // but a const field would have only IsLiteral equal to true
      if( fi is not null && fi.IsLiteral && !fi.IsInitOnly )
      {
        // Find the constant with the same value as that passed
        if( fi.FieldType == typeof( ushort ) )
        {
          var value = fi.GetRawConstantValue();
          if( value is null || (ushort)value != tag )
          {
            continue;
          }

          // If the field has custom attributes, return the value passed to the constructor
          var customAttributes = fi.GetCustomAttributes<ExifTagDetailsAttribute>();
          var detailsAttribute = customAttributes.FirstOrDefault();
          if( detailsAttribute != null )
          {
            return new ExifTagDetails( Name: detailsAttribute.Name, Description: detailsAttribute.Description );
          }
          else
          {
            // If the field has no custom attributes, return the field name but no description
            return new ExifTagDetails( Name: fi.Name, Description: string.Empty );
          }
        }
      }
    }

    return null;
  }
}
