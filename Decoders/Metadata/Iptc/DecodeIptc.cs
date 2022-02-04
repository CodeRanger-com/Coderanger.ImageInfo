// -----------------------------------------------------------------------
// <copyright file="DecodeIptc.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment>
// IPTC data is stored in big endian format
// Specifications:
// https://www.iptc.org/std/IIM/4.1/specification/IIMV4.1.pdf
// https://exiftool.org/TagNames/IPTC.html
// https://www.codeproject.com/articles/1208/extracting-iptc-header-information-from-jpeg-image
// </comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata.Iptc;

using Coderanger.ImageInfo.Decoders.DecoderUtils;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// IPTC data is stored in big endian format
/// </remarks>
internal class DecodeIptc
{
  internal static DecodeIptc? DecodeFromReader( BinaryReader reader, int maxAvailableBytes )
  {
    var decoder = new DecodeIptc( reader );
    if( decoder.Decode( maxAvailableBytes ) )
    {
      return decoder;
    }  

    return null;
  }

  internal static DecodeIptc? DecodeFromBuffer( byte[] buffer, int count, int maxAvailableBytes, bool validateSignature )
  {
    using var stream = new MemoryStream( buffer, 0, count );
    using var reader = new BinaryReader( stream );

    var decoder = new DecodeIptc( reader );
    if( decoder.Decode( maxAvailableBytes, validateSignature ) )
    {
      return decoder;
    }

    return null;
  }

  internal bool HasTags()
  {
    return _values?.Count > 0;
  }

  internal bool AddTagsToProfile( ref Metadata metadata )
  {
    if( _values is not null && HasTags() )
    {
      var tags = metadata.GetListForProfile( MetadataProfileType.Iptc );
      foreach( var tag in _values )
      {
        tags.Add( tag );
      }
      return true;
    }

    return false;
  }

  private DecodeIptc( BinaryReader reader )
  {
    _reader = reader;
  }

  private bool Decode( int maxAvailableBytes, bool validateSignature = true )
  {
    if( _processed )
    {
      // Already done
      return false;
    }

    // Buffer only the data we need
    if( validateSignature )
    {
      if( maxAvailableBytes < IptcConstants.AdobePhotoshop.MagicBytes.Length )
      {
        return false;
      }

      var signature = _reader.ReadBytes( IptcConstants.AdobePhotoshop.MagicBytes.Length );
      if( !signature.SequenceEqual( IptcConstants.AdobePhotoshop.MagicBytes ) )
      {
        return false;
      }

      maxAvailableBytes -= IptcConstants.AdobePhotoshop.MagicBytes.Length;
    }

    var resourceBlockData = _reader.ReadBytes( maxAvailableBytes );
    var blockDataSpan = resourceBlockData.AsSpan();

    while( blockDataSpan.Length > 12 )
    {
      if( DataConversion.UInt32FromBigEndianBuffer( blockDataSpan[ ..4 ] ) != IptcConstants.AdobePhotoshop.ImageResourceMarkerValue )
      {
        return false;
      }

      // Reslice past the image resource marker
      blockDataSpan = blockDataSpan[ 4.. ];

      int resourceBlockNameLength = ReadImageResourceNameLength( blockDataSpan );
      int resourceDataSize = ReadResourceDataLength( blockDataSpan, resourceBlockNameLength );

      // 2 for the image resource block id, 4 for the resource data size
      int dataStartIdx = 2 + resourceBlockNameLength + 4;

      var imageResourceBlockId = DataConversion.UInt16FromBigEndianBuffer( blockDataSpan[ ..2 ] );
      if( imageResourceBlockId == IptcConstants.AdobePhotoshop.IptcMarkerValue )
      {
        if( resourceDataSize > 0 && blockDataSpan.Length >= dataStartIdx + resourceDataSize )
        {
          //this.isIptc = true;
          var data = blockDataSpan.Slice( dataStartIdx, resourceDataSize );
          var decoded = IptcValue.Decode( data );
          if( decoded is not null )
          {
            if( _values is null )
            {
              _values = new();
            }
            _values.AddRange( decoded );
          }
          break;
        }
      }
      else
      {
        if( blockDataSpan.Length < dataStartIdx + resourceDataSize )
        {
          // Not enough data or the resource data size is wrong.
          break;
        }

        blockDataSpan = blockDataSpan[ ( dataStartIdx + resourceDataSize ).. ];
      }
    }

    _processed = true;

    return true;
  }

  /// <summary>
  /// Reads the adobe image resource block name: a Pascal string (padded to make size even)
  /// </summary>
  /// <param name="blockDataSpan">The span holding the block resource data.</param>
  /// <returns>The length of the name.</returns>
  private static int ReadImageResourceNameLength( ReadOnlySpan<byte> blockDataSpan )
  {
    var nameLength = blockDataSpan[ 2 ];
    var nameDataSize = nameLength == 0 ? 2 : nameLength;
    if( nameDataSize % 2 != 0 )
    {
      nameDataSize++;
    }

    return nameDataSize;
  }

  /// <summary>
  /// Reads the length of a adobe image resource data block
  /// </summary>
  /// <param name="blockDataSpan">The span holding the block resource data.</param>
  /// <param name="resourceBlockNameLength">The length of the block name.</param>
  /// <returns>The block length.</returns>
  private static int ReadResourceDataLength( Span<byte> blockDataSpan, int resourceBlockNameLength )
      => DataConversion.Int32FromBigEndianBuffer( blockDataSpan.Slice( 2 + resourceBlockNameLength, 4 ) );

  private bool _processed = false;
  private List<IMetadataTypedValue>? _values;

  private readonly BinaryReader _reader;
}
