// -----------------------------------------------------------------------
// <copyright file="ExifDateTime.cs" company="CodeRanger.com">
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
public class ExifDateTime : ExifTypeValue, IExifValue
{
  internal ExifDateTime( BinaryReader reader, ExifComponent component )
    : base( ExifType.DateTime, reader, component )
  {
  }

  /// <summary>
  /// Override to always set as false for Dates as Component.Count refers to character count
  /// in the string which makes up the Date value
  /// </summary>
  public override bool IsArray => false;

  public string StringValue => ToString();

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
    // Date data is always 19 characters: yyyy:MM:dd HH:mm:ss
    // so should always be within the 4 byte buffer
    var value = Encoding.ASCII.GetString( Component.DataValueBuffer );
    if( DateTime.TryParseExact( value, DateTimeFormatString, null, System.Globalization.DateTimeStyles.None, out var dt ) )
    {
      // Dates are stored as ASCII strings, but we can do better
      yield return new ExifTagValue( Type: ExifType, IsArray: IsArray, TagId: Tag, TagName: Name, Value: dt );
    }
  }

  private const string DateTimeFormatString = "yyyy:MM:dd HH:mm:ss";
}
