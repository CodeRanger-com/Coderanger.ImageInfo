// -----------------------------------------------------------------------
// <copyright file="PngText.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Png;

/// <summary>
/// Record describing PNG Metadata values
/// </summary>
/// <param name="Keyword">Keyword type of data</param>
/// <param name="TextValue">Textual value for keyword</param>
/// <param name="Language">Optional language of text value</param>
/// <param name="TranslatedKeyword">Optional translation of keyword in given language</param>
public record PngText( string Keyword, string TextValue, string Language = "", string TranslatedKeyword = "" )
{
  /// <summary>
  /// Returns a string that represents the object
  /// </summary>
  /// <returns>String that represents the object</returns>
  public override string ToString()
  {
    if( string.IsNullOrEmpty( TranslatedKeyword ) )
    {
      return $"{Keyword} = {TextValue}";
    }
    else
    {
      if( string.IsNullOrEmpty( Language ) )
      {
        return $"{Keyword} ({TranslatedKeyword}) = {TextValue}";
      }
      else
      {
        return $"{Keyword} ({TranslatedKeyword}) = {TextValue} ({Language})";
      }
    }
  }
}
