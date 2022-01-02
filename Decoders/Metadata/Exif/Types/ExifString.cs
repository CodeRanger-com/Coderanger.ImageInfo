// -----------------------------------------------------------------------
// <copyright file="ExifString.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Exif.Types;

using Coderanger.ImageInfo.Decoders.DecoderUtils;
using static Coderanger.ImageInfo.Decoders.Metadata.Exif.ExifConstants;

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

  public string StringValue => ToString();

  /// <summary>
  /// Override to always set as false for strings as Component.Count refers to character count
  /// in the string
  /// </summary>
  public override bool IsArray => false;

  void IMetadataTypedValue.SetValue()
  {
    ProcessData();
  }

  long IMetadataTypedValue.ValueOffsetReferenceStart
  {
    get
    {
      return base.ValueOffsetReferenceStart;
    }
    set
    {
      base.ValueOffsetReferenceStart = value;
    }
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
      yield return new MetadataTagValue( Type: ExifType, IsArray: false, TagId: TagId, TagName: Name, Value: DataConversion.ConvertBuffer( Component.DataValueBuffer, byteCount, _encoding ) );
    }
    else
    {
      // Buffer will contain a reference to the data elsewhere in the IFD, therefore move to
      // that position and read enough bytes for conversion x number of components saved
      var exifValue = DataConversion.Int32FromBuffer( Component.DataValueBuffer, 0, Component.ByteOrder );
      Reader.BaseStream.Seek( Component.DataStart + exifValue, SeekOrigin.Begin );

      ValueOffsetReferenceStart = Component.DataStart + exifValue;

      var buffer = Reader.ReadBytes( Component.ComponentCount );
      var encoding = _encoding;
      if( encoding == StringEncoding.Undefined )
      {
        // First 8 bytes should be encoding type
        var signatureSpan = buffer.AsSpan( 0, 8 );
        if( signatureSpan.SequenceEqual( EncodingSignature.Ascii ) )
        {
          encoding = StringEncoding.Ascii;

          // Reset conversion buffer to be past the signature to the end
          buffer = buffer.AsSpan( 8 ).ToArray();
          byteCount -= 8;
        }
        else if( signatureSpan.SequenceEqual( EncodingSignature.Unicode ) )
        {
          encoding = StringEncoding.Unicode;

          // Reset conversion buffer to be past the signature to the end
          buffer = buffer.AsSpan( 8 ).ToArray();
          byteCount -= 8;
        }
        else if( signatureSpan.SequenceEqual( EncodingSignature.Jis ) )
        {
          encoding = StringEncoding.Jis;

          // Reset conversion buffer to be past the signature to the end
          buffer = buffer.AsSpan( 8 ).ToArray();
          byteCount -= 8;
        }
        else
        {
          // Assume no signature and ascii
          encoding = StringEncoding.Ascii;
        }
      }

      yield return new MetadataTagValue( Type: ExifType, IsArray: IsArray, TagId: TagId, TagName: Name, Value: DataConversion.ConvertBuffer( buffer, byteCount, encoding ) );
    }
  }

  private readonly StringEncoding _encoding;
}
