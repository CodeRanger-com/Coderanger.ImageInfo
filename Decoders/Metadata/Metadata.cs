// -----------------------------------------------------------------------
// <copyright file="Metadata" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.Metadata;

internal class Metadata
{
  internal Metadata()
  {
  }

  internal List<IMetadataTypedValue> GetListForProfile( MetadataProfileType type )
  {
    if( !_tags.TryGetValue( type, out var value ) )
    {
      value = new List<IMetadataTypedValue>();
      _tags.Add( type, value );
    }

    return value;
  }

  internal void AddTag( MetadataProfileType type, IMetadataTypedValue tag )
  {
    var profileTags = GetListForProfile( type );
    if( profileTags != null )
    {
      profileTags.Add( tag );
    }
  }

  internal void AddTags( MetadataProfileType type, List<IMetadataTypedValue> tags )
  {
    var profileTags = GetListForProfile( type );
    if( profileTags != null )
    {
      profileTags.AddRange( tags );
    }
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
