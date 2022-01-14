// -----------------------------------------------------------------------
// <copyright file="ExifDateTime.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;

using System.Text;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class ExifDateTime : ExifTypeBase, IMetadataTypedValue
{
  internal ExifDateTime( BinaryReader reader, ExifComponent component )
    : base( MetadataType.DateTime, reader, component )
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

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // Date data is always 19 characters: yyyy:MM:dd HH:mm:ss

    // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
    // that position and read enough bytes for conversion x number of components saved
    var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
    Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

    var buffer = Reader.ReadBytes( Component.ComponentCount );
    var byteCount = Component.ComponentCount * Component.ComponentSize;

    var stringValue = DataConversion.ConvertBuffer( buffer.AsSpan( 0, byteCount ), StringEncoding.Ascii );
    if( DateTime.TryParseExact( stringValue, DateTimeFormatString, null, System.Globalization.DateTimeStyles.None, out var dt ) )
    {
      // Dates are stored as ASCII strings, but we can do better and make it a proper type
      yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: dt );
    }
  }

  private const string DateTimeFormatString = "yyyy:MM:dd HH:mm:ss";
}
