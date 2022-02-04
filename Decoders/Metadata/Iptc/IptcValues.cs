// -----------------------------------------------------------------------
// <copyright file="IptcValues.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc;

using System.Buffers.Binary;
using System.Collections.Generic;
using Coderanger.ImageInfo.Decoders.DecoderUtils;

internal record IptcValue( uint Tag, byte[] Data )
{
  public override string ToString()
  {
    return DataConversion.ConvertBuffer( Data, StringEncoding.Ascii );
  }

  internal static List<IMetadataTypedValue>? Decode( ReadOnlySpan<byte> buffer )
  {
    if( buffer == null || buffer[ 0 ] != 0x1c )
    {
      return null;
    }

    var values = new List<IMetadataTypedValue>();

    int i = 0;
    while( i + 4 < buffer.Length )
    {
      if( buffer[ i++ ] != 0x1c )
      {
        continue;
      }

      var record = buffer[ i++ ];
      var tag = buffer[ i++ ];

      int count = BinaryPrimitives.ReadInt16BigEndian( buffer.Slice( i, 2 ) );
      i += 2;

      if( ( count > 0 ) && ( i + count <= buffer.Length ) )
      {
        var iptcData = buffer.Slice( i, count ).ToArray();

        // If its repeatable, add to existing tag or create it
        var added = false;

        var repeatable = IptcTagValueFactory.IsRepeatable( tag );
        if( repeatable )
        {
          var existingTag = values.Find( t => t.TagId == tag );
          if( existingTag is not null )
          {
            existingTag.AddToExistingValue( iptcData );
            added = true;
          }
        }

        if( !added )
        {
          var value = IptcTagValueFactory.Create( record, tag, iptcData );
          if( value is not null )
          {
            values.Add( value );
          }
        }
      }

      i += count;
    }

    return values;
  }
}
