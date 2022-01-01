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
/// <param name="Keyword"></param>
/// <param name="TextValue"></param>
/// <param name="Language"></param>
public record PngText( string Keyword, string TextValue, string Language = "", string TranslatedKeyword = "" )
{
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
