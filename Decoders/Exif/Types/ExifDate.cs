// -----------------------------------------------------------------------
// <copyright file="ExifDate.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

using System.Text;

/// <summary>
/// 
/// </summary>
public class ExifDate : ExifTypeValue, IExifValue
{
  internal ExifDate( BinaryReader reader, ExifComponent component )
    : base( ExifType.Date, reader, component )
  {
  }

  public string StringValue => ToString();

  /// <summary>
  /// Override to always set as false for Dates as Component.Count refers to character count
  /// in the string which makes up the Date value
  /// </summary>
  public override bool IsArray => false;

  public override string ToString()
  {
    return $"{Name} = {_convertedValue}";
  }

  void IExifValue.SetValue()
  {
    ProcessData();
  }

  internal override IEnumerable<ExifTagValue> ExtractValues()
  {
    // Date data is always 10 characters: yyyy:MM:dd
    // so should always be within the 4 byte buffer
    var value = Encoding.ASCII.GetString( Component.DataValueBuffer );
    if( DateOnly.TryParseExact( value, DateFormatString, null, System.Globalization.DateTimeStyles.None, out var dt ) )
    {
      // Dates are stored as ASCII strings, but we can do better
      yield return new ExifTagValue( Type: ExifType, IsArray: IsArray, TagId: Tag, TagName: Name, Value: dt );
    }
  }

  private const string DateFormatString = "yyyy:MM:dd";
}
