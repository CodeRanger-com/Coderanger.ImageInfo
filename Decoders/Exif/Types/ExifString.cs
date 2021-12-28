// -----------------------------------------------------------------------
// <copyright file="ExifString.cs" company="CodeRanger.com">
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
public class ExifString : ExifTypeValue, IExifValue
{
  internal ExifString( ExifStringEncoding encoding, BinaryReader reader, ExifComponent component )
    : base( ExifType.String, reader, component )
  {
    _encoding = encoding;
  }

  public string StringValue => ToString();

  /// <summary>
  /// Override to always set as false for strings as Component.Count refers to character count
  /// in the string
  /// </summary>
  public override bool IsArray => false;

  void IExifValue.SetValue()
  {
    ProcessData();
  }

  /// <summary>
  /// Processes the data buffer for each type value
  /// </summary>
  internal override IEnumerable<ExifTagValue> ExtractValues()
  {
    var byteCount = Component.ComponentCount * Component.ComponentSize;

    // If the size * count is within the 4 byte buffer, can just iterate it and yield the string
    if( Component.ComponentCount * Component.ComponentSize <= BufferByteSize )
    {
      yield return new ExifTagValue( Type: ExifType, IsArray: false, TagId: Tag, TagName: Name, Value: DataConversion.ConvertBuffer( Component.DataValueBuffer, byteCount, _encoding ) );
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var buffer = Reader.ReadBytes( Component.ComponentCount );

      yield return new ExifTagValue( Type: ExifType, IsArray: IsArray, TagId: Tag, TagName: Name, Value: DataConversion.ConvertBuffer( buffer, byteCount, _encoding ) );
    }
  }

  private readonly ExifStringEncoding _encoding;
}
