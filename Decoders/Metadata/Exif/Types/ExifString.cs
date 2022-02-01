// -----------------------------------------------------------------------
// <copyright file="ExifString.cs" company="CodeRanger.com">
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
public class ExifString : ExifTypeBase, IMetadataTypedValue
{
  internal ExifString( StringEncoding encoding, BinaryReader reader, ExifComponent component )
    : base( MetadataType.String, reader, component )
  {
    _encoding = encoding;
  }

  /// <summary>
  /// Override to always set as false for strings as Component.Count refers to character count
  /// in the string
  /// </summary>
  public override bool IsArray => false;

  /// <summary>
  /// Sets the value of the object
  /// </summary>
  /// <param name="buffer">Buffer which contains the appropriate data value</param>
  public void SetValue( ReadOnlySpan<byte> buffer )
  {
    ProcessData();
  }

  /// <summary>
  /// Processes the data buffer for each type value
  /// </summary>
  internal override IEnumerable<MetadataTagValue> ExtractValues()
  {
    var byteCount = Component.ComponentCount * Component.ComponentSize;

    // If the size * count is within the 4 byte buffer, can just iterate it and yield the string
    // Unless its an undefined type which means that the first 8 bytes would be the encoding
    if( Component.ComponentCount * Component.ComponentSize <= BufferByteSize && _encoding != StringEncoding.Undefined )
    {
      var value = DataConversion.ConvertBuffer( Component.DataValueBuffer.AsSpan( 0, byteCount ), _encoding );
      if( value.Length > 0 )
      {
        yield return new MetadataTagValue( Type: TagType, IsArray: false, TagId: TagId, TagName: Name, Value: value );
      }
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer.AsSpan(), Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      var buffer = Reader.ReadBytes( Component.ComponentCount );
      var encoding = _encoding;
      if( encoding == StringEncoding.Undefined )
      {
        // First 8 bytes should be encoding type
        var signature = DataConversion.UInt64FromBigEndianBuffer( buffer.AsSpan( 0, 8 ) );
        switch( signature )
        {
          case ExifConstants.EncodingSignature.AsciiValue:
            encoding = StringEncoding.Ascii;

            // Reset conversion buffer to be past the signature to the end
            buffer = buffer.AsSpan( 8 ).ToArray();
            byteCount -= 8;
            break;

          case ExifConstants.EncodingSignature.UnicodeValue:
            encoding = StringEncoding.Unicode;

            // Reset conversion buffer to be past the signature to the end
            buffer = buffer.AsSpan( 8 ).ToArray();
            byteCount -= 8;
            break;

          case ExifConstants.EncodingSignature.JisValue:
            encoding = StringEncoding.Jis;

            // Reset conversion buffer to be past the signature to the end
            buffer = buffer.AsSpan( 8 ).ToArray();
            byteCount -= 8;
            break;

          default:
            // Assume no signature and ascii
            encoding = StringEncoding.Ascii;
            break;
        }
      }

      var value = DataConversion.ConvertBuffer( buffer.AsSpan( 0, byteCount ), encoding );
      if( value.Length > 0 )
      {
        yield return new MetadataTagValue( Type: TagType,
                                           IsArray: IsArray,
                                           TagId: TagId,
                                           TagName: Name,
                                           Value: value );
      }
    }
  }

  private readonly StringEncoding _encoding;
}
