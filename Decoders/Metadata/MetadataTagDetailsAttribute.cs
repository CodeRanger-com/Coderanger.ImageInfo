// -----------------------------------------------------------------------
// <copyright file="MetadataTagDetailsAttribute.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

using System;
using System.Reflection;

/// <summary>
/// Helper record to return keyword information to caller
/// </summary>
internal record MetadataTagDetails( string Name, string Description );

/// <summary>
/// Attribute to allow each tag constant to be decorated with additional information
/// </summary>
[AttributeUsage( AttributeTargets.Field, AllowMultiple = false )]
internal sealed class MetadataTagDetailsAttribute : Attribute
{
  public MetadataTagDetailsAttribute( string name, string description )
  {
    Name = name;
    Description = description;
  }

  public string Name { get; init; }
  public string Description { get; init; }

  /// <summary>
  /// Gets the tag description from any custom attributes
  /// </summary>
  public static MetadataTagDetails? GetTagDetails( IMetadataTag type, ushort tag )
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
          var customAttributes = fi.GetCustomAttributes<MetadataTagDetailsAttribute>();
          var detailsAttribute = customAttributes.FirstOrDefault();
          if( detailsAttribute != null )
          {
            return new MetadataTagDetails( Name: detailsAttribute.Name, Description: detailsAttribute.Description );
          }
          else
          {
            // If the field has no custom attributes, return the field name but no description
            return new MetadataTagDetails( Name: fi.Name, Description: string.Empty );
          }
        }
      }
    }

    return null;
  }
}
