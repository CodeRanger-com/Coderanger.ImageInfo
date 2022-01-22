// -----------------------------------------------------------------------
// <copyright file="ExifDouble.cs" company="CodeRanger.com">
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
public class ExifDouble : ExifTypeBase, IMetadataTypedValue
{
  internal ExifDouble( BinaryReader reader, ExifComponent component )
    : base( MetadataType.Double, reader, component )
  {
  }

  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // Double type is 8 bytes so will always be above the 4 byte buffer so the buffer
    // will contain a reference to the data elsewhere in the IFD, therefore move to
    // that position and read enough bytes for conversion x number of components saved
    var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, Component.ByteOrder );
    Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

    var dataValue = Reader.ReadBytes( Component.ComponentCount * Component.ComponentSize );

    for( var i = 0; i < Component.ComponentCount; i++ )
    {
      var buff = dataValue.AsSpan( i * Component.ComponentSize, Component.ComponentSize );

      var value = DataConversion.DoubleFromBuffer( buff, Component.ByteOrder );
      yield return new MetadataTagValue( Type: TagType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
    }
  }
}
