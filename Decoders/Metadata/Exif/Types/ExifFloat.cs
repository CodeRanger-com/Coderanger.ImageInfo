// -----------------------------------------------------------------------
// <copyright file="ExifFloat.cs" company="CodeRanger.com">
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
public class ExifFloat : ExifTypeBase, IMetadataTypedValue
{
  internal ExifFloat( BinaryReader reader, ExifComponent component )
    : base( MetadataType.Float, reader, component )
  {
  }

  public string StringValue => ToString();

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    // Float type is 4 bytes so may be within our existing 4 byte buffer if there is only one
    if( Component.ComponentCount == 1 )
    {
      var value = DataConversion.FloatFromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      for( var i = 0; i < Component.ComponentCount; i++ )
      {
        var dataValue = Reader.ReadBytes( Component.ComponentSize );

        var value = DataConversion.FloatFromBuffer( dataValue, 0, Component.ByteOrder );
        yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: value );
      }
    }
  }
}
