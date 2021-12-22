// -----------------------------------------------------------------------
// <copyright file="DictionaryByType.cs" company="CodeRanger.com">
//     CodeRanger.com. All rights reserved
// </copyright>
// <author>Dan Petitt</author>
// <comment></comment>
// -----------------------------------------------------------------------

namespace Coderanger.ImageInfo.Decoders.DecoderUtils;

using System.Collections.Generic;

#nullable disable

/// <summary>
/// Map from types to instances of those types, e.g. int to 10 and
/// string to "hi" within the same dictionary. This cannot be done
/// without casting (and boxing for value types) as .NET cannot
/// represent this relationship with generics in their current form.
/// This class encapsulates the nastiness in a single place.
/// </summary>
public class DictionaryByType<T>
{
  private readonly IDictionary<T, object> _dictionary = new Dictionary<T, object>();

  /// <summary>
  /// Removes all items from the collection
  /// </summary>
  public void Clear()
  {
    _dictionary.Clear();
  }

  /// <summary>
  /// Maps the specified type argument to the given value. If
  /// the type argument already has a value within the dictionary,
  /// ArgumentException is thrown.
  /// </summary>
  public void Add<V>( T key, V value )
  {
    _dictionary.Add( key, value );
  }

  /// <summary>
  /// Maps the specified type argument to the given value. If
  /// the type argument already has a value within the dictionary,
  /// ArgumentException is thrown.
  /// </summary>
  public bool ContainsKey( T key )
  {
    return _dictionary.ContainsKey( key );
  }

  /// <summary>
  /// Attempts to fetch a value from the dictionary, returning false and
  /// setting the output parameter to the default value for T if it
  /// fails, or returning true and setting the output parameter to the
  /// fetched value if it succeeds.
  /// </summary>
  public bool TryGetValue<V>( T key, out V value )
  {
    if( _dictionary.TryGetValue( key, out object tmp ) )
    {
      value = (V)tmp;
      return true;
    }

    value = default;
    return false;
  }
}
