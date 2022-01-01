// -----------------------------------------------------------------------
// <copyright file="PngKeywordDetailsAttribute.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Png;

using System;
using System.Reflection;

/// <summary>
/// Helper record to return keyword information to caller
/// </summary>
internal record PngKeywordDetails( ushort Id, string Name, string Description );

/// <summary>
/// Attribute to allow each keyword constant to be decorated with additional information
/// </summary>
[AttributeUsage( AttributeTargets.Field, AllowMultiple = false )]
internal sealed class PngKeywordDetailsAttribute : Attribute
{
  public PngKeywordDetailsAttribute( string name, string description )
  {
    Name = name;
    Description = description;
  }

  public string Name { get; init; }
  public string Description { get; init; }

  /// <summary>
  /// Gets the tag description from any custom attributes
  /// </summary>
  public static PngKeywordDetails? GetTagDetails( PngKeyword type, string tagName )
  {
    var fieldInfos = type.GetType().GetFields(
      // Gets all public and static fields

      BindingFlags.Public | BindingFlags.Static |
      // This tells it to get the fields from all base types as well

      BindingFlags.FlattenHierarchy );

    // Go through the list and only pick out the constants
    var loweredTagName = tagName.ToLower();
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

          if( value == null || fi.Name.ToLower() != loweredTagName )
          {
            continue;
          }

          // If the field has custom attributes, return the value passed to the constructor
          var customAttributes = fi.GetCustomAttributes<PngKeywordDetailsAttribute>();
          var detailsAttribute = customAttributes.FirstOrDefault();
          if( detailsAttribute != null )
          {
            return new PngKeywordDetails( Id: (ushort)value, Name: detailsAttribute.Name, Description: detailsAttribute.Description );
          }
          else
          {
            // If the field has no custom attributes, return the field name but no description
            return new PngKeywordDetails( Id: (ushort)value, Name: fi.Name, Description: string.Empty );
          }
        }
      }
    }

    return null;
  }
}
