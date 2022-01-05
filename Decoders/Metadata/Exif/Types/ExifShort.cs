// -----------------------------------------------------------------------
// <copyright file="ExifShort.cs" company="CodeRanger.com">
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
public class ExifShort : ExifTypeBase, IMetadataTypedValue
{
  internal ExifShort( BinaryReader reader, ExifComponent component )
    : base( MetadataType.Short, reader, component )
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
        var buff = Component.DataValueBuffer.AsSpan( i * Component.ComponentSize, Component.ComponentSize );

        var value = DataConversion.Int16FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );

      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        var buff = dataValue.AsSpan( i * Component.ComponentSize, Component.ComponentSize );
        var value = DataConversion.Int16FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
  }
}

/// <summary>
/// 
/// </summary>
public class ExifUShort : ExifTypeBase, IMetadataTypedValue
{
  internal ExifUShort( BinaryReader reader, ExifComponent component )
    : base( MetadataType.UShort, reader, component )
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
        var buff = Component.DataValueBuffer.AsSpan( i * Component.ComponentSize, Component.ComponentSize );
        var value = DataConversion.UInt16FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );

      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        var buff = dataValue.AsSpan( i * Component.ComponentSize, Component.ComponentSize );
        var value = DataConversion.UInt16FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
  }
}
