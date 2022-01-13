// -----------------------------------------------------------------------
// <copyright file="MetadataTagEnumAttribute.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

using System;
using System.Reflection;

/// <summary>
/// Attribute to allow each tag constant to be decorated with additional information
/// </summary>
[AttributeUsage( AttributeTargets.Field, AllowMultiple = true )]
internal sealed class MetadataTagEnumAttribute : Attribute
{
  public MetadataTagEnumAttribute( ushort code, string information )
  {
    Code = code;
    Information = information;
  }

  public MetadataTagEnumAttribute( string code, string information )
  {
    CodeString = code;
    Information = information;
  }

  public ushort? Code { get; init; }
  public string? CodeString { get; init; }
  public string Information { get; init; }

  /// <summary>
  /// Gets the tag enum values from any custom attributes
  /// </summary>
  public static string? GetTagEnumValue( IMetadataTag type, ushort tag, ushort enumValue )
  {
    return FindField( type, tag, (attribute) => attribute.Code != null && attribute.Code == enumValue );
  }

  public static string? GetTagEnumValue( IMetadataTag type, ushort tag, string enumValue )
  {
    return FindField( type, tag, ( attribute ) => attribute.CodeString != null && attribute.CodeString == enumValue );
  }

  private delegate bool MatchField( MetadataTagEnumAttribute attribute );

  private static string? FindField( IMetadataTag type, ushort tag, MatchField matcher )
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
          var customAttributes = fi.GetCustomAttributes<MetadataTagEnumAttribute>();
          foreach( var detailsAttribute in customAttributes )
          {
            if( detailsAttribute != null && matcher( detailsAttribute ) )
            {
              return detailsAttribute.Information;
            }
          }
        }
      }
    }

    return null;
  }
}
