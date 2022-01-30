// -----------------------------------------------------------------------
// <copyright file="Metadata" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

using Coderanger.ImageInfo.Decoders.Metadata.Exif;

internal class Metadata
{
  internal Metadata()
  {
  }

  internal List<IMetadataTypedValue> GetListForProfile( MetadataProfileType profile )
  {
    if( !_tags.TryGetValue( profile, out var value ) )
    {
      value = new List<IMetadataTypedValue>();
      _tags.Add( profile, value );
    }

    return value;
  }

  internal void AddTag( MetadataProfileType profile, IMetadataTypedValue tag )
  {
    var profileTags = GetListForProfile( profile );
    if( profileTags != null )
    {
      profileTags.Add( tag );
    }
  }

  internal void AddTags( MetadataProfileType profile, List<IMetadataTypedValue> tags )
  {
    var profileTags = GetListForProfile( profile );
    if( profileTags != null )
    {
      profileTags.AddRange( tags );
    }
  }

  internal IMetadataTypedValue? FindTag( MetadataProfileType profile, ushort tagId )
  {
    if( _tags.TryGetValue( profile, out var value ) )
    {
      return value.Find( t => t.TagId == tagId );
    }
    return null;
  }

  internal Dictionary<MetadataProfileType, List<IMetadataTypedValue>>? GetTags()
  {
    if( _tags.Count == 0 )
    {
      return null;
    }

    return _tags;
  }

  private readonly Dictionary<MetadataProfileType, List<IMetadataTypedValue>> _tags = new();
}
