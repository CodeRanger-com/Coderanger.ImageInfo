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
  internal enum EnumType
  {
    Numeric,
    String,
  }

  internal ExifEnum( BinaryReader reader, ExifComponent component, EnumType enumType )
    : base( MetadataType.Enum, reader, component )
  {
    _enumType = enumType;
  }

  public void SetValue( ReadOnlySpan<byte> buffer )
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
        var buff = Component.DataValueBuffer.AsSpan( 0 + ( i * Component.ComponentSize ) );
        var value = GetEnumFromValue( buff );
        if( value != null )
        {
          yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
        }
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
        var value = GetEnumFromValue( buff );
        if( value != null )
        {
          yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
        }
      }
    }
  }

  private MetadataEnumValue? GetEnumFromValue( ReadOnlySpan<byte> value )
  {
    if( _enumType == EnumType.String )
    {
      var bufferValue = DataConversion.ConvertBuffer( value, StringEncoding.Utf8 );
      if( bufferValue?.Length > 0 )
      {
        var attributeInfo = MetadataTagEnumAttribute.GetTagEnumValue( ReflectionExifTag, TagId, bufferValue );
        return new MetadataEnumValue( bufferValue, attributeInfo ?? string.Empty );
      }
    }
    else
    {
      var numberValue = DataConversion.UInt16FromBuffer( value, Component.ByteOrder );
      var attributeInfo = MetadataTagEnumAttribute.GetTagEnumValue( ReflectionExifTag, TagId, numberValue );
      return new MetadataEnumValue( numberValue.ToString(), attributeInfo ?? string.Empty );
    }

    return null;
  }

  private readonly EnumType _enumType;
}
