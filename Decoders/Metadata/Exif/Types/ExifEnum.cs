// -----------------------------------------------------------------------
// <copyright file="ExifShortEnum.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
public class ExifEnum : ExifTypeBase, IMetadataTypedValue
{
  internal ExifEnum( BinaryReader reader, ExifComponent component )
    : base( MetadataType.Enum, reader, component )
  {
  }

  public string StringValue => ToString();

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // If the size * count is within the 4 byte buffer, can just iterate it and yield the short
    if( Component.ComponentCount * Component.ComponentSize <= BufferByteSize )
    {
      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        var value = DataConversion.UInt16FromBuffer( Component.DataValueBuffer.AsSpan( 0 + ( i * Component.ComponentSize ) ), Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: new MetadataEnumValue( value, GetEnumValue( value ) ) );
      }
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer.AsSpan(), Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );

      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        var buff = dataValue.AsSpan( i * Component.ComponentSize, 2 );
        var value = DataConversion.UInt16FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: new MetadataEnumValue( value, GetEnumValue( value ) ) );
      }
    }
  }

  private string GetEnumValue( ushort enumValue )
  {
    var enumVal = MetadataTagEnumAttribute.GetTagEnumValue( ReflectionExifTag, Component.Tag, enumValue );
    if( enumVal != null )
    {
      return enumVal;
    }

    return string.Empty;
  }
}
