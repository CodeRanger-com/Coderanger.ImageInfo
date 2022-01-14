// -----------------------------------------------------------------------
// <copyright file="ExifInt.cs" company="CodeRanger.com">
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
internal class ExifInt : ExifTypeBase, IMetadataTypedValue
{
  internal ExifInt( BinaryReader reader, ExifComponent component )
    :base( MetadataType.Int, reader, component )
  {
  }

  public string StringValue => ToString();

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // Exif Long type is a 4 byte int so may be within our existing 4 byte buffer if there is only one
    if( Component.ComponentCount == 1 )
    {
      var value = DataConversion.Int32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
      yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
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

        var value = DataConversion.Int32FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
  }
}

/// <summary>
/// 
/// </summary>
internal class ExifUInt : ExifTypeBase, IMetadataTypedValue
{
  internal ExifUInt( BinaryReader reader, ExifComponent component )
    : base( MetadataType.UInt, reader, component )
  {
  }

  public string StringValue => ToString();

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // Exif ULong type is a 4 byte int so may be within our existing 4 byte buffer if there is only one
    if( Component.ComponentCount == 1 )
    {
      var value = DataConversion.UInt32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
      yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
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

        var value = DataConversion.UInt32FromBuffer( buff, Component.ByteOrder );
        yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
  }
}
