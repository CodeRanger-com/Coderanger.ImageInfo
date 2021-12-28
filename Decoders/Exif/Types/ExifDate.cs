// -----------------------------------------------------------------------
// <copyright file="ExifDate.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Exif.Types;

using System.Text;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

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

    // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
    // that position and read enough bytes for conversion x number of components saved
    var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
    Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

    var buffer = Reader.ReadBytes( Component.ComponentCount );
    var byteCount = Component.ComponentCount * Component.ComponentSize;

    var stringValue = DataConversion.ConvertBuffer( buffer, byteCount, ExifStringEncoding.Ascii );
    if( DateOnly.TryParseExact( stringValue, DateFormatString, null, System.Globalization.DateTimeStyles.None, out var dt ) )
    {
      // Dates are stored as ASCII strings, but we can do better and make it a proper type
      yield return new ExifTagValue( Type: ExifType, IsArray: IsArray, TagId: Tag, TagName: Name, Value: dt );
    }
  }

  private const string DateFormatString = "yyyy:MM:dd";
}
