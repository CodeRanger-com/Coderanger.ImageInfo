// -----------------------------------------------------------------------
// <copyright file="ExifByte.cs" company="CodeRanger.com">
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
public class ExifByte : ExifTypeBase, IMetadataTypedValue
{
  internal ExifByte( BinaryReader reader, ExifComponent component )
    : base( MetadataType.Byte, reader, component )
  {
  }

  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // If the size * count is within the 4 byte buffer, can just iterate it and yield the byte
    if( Component.ComponentCount * Component.ComponentSize <= BufferByteSize )
    {
      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: Component.DataValueBuffer[ i ] );
      }
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        var dataValue = Reader.ReadByte();
        yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: dataValue );
      }
    }
  }
}
