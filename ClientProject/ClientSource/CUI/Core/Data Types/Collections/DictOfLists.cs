using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;
using System.Collections;

namespace CrabUI
{
  /// <summary>
  /// It's a dict of lists but you can add and remove elements one by one  
  /// Also it never returns null  
  /// Also list ref is persistent
  /// </summary>
  public class DictOfLists<TKey, TValue> : IDictionary<TKey, List<TValue>>
  {
    private Dictionary<TKey, List<TValue>> _Dict = new();

    public void Add(TKey key, TValue value)
    {
      if (!_Dict.ContainsKey(key)) _Dict[key] = [];
      _Dict[key].Add(value);
    }

    public void Remove(TKey key, TValue value)
    {
      if (!_Dict.ContainsKey(key)) return;
      _Dict[key].Remove(value);
    }

    public bool Contains(TKey key, TValue value)
    {
      if (!_Dict.ContainsKey(key)) return false;
      return _Dict[key].Contains(value);
    }

    public void Clear(TKey key)
    {
      if (!_Dict.ContainsKey(key)) _Dict[key] = [];
      _Dict[key].Clear();
    }

    #region  IDictionary<TKey, List<TValue>>

    public List<TValue> this[TKey key]
    {
      get
      {
        if (!_Dict.ContainsKey(key)) _Dict[key] = [];
        return _Dict[key];
      }
      set => _Dict[key] = value ?? [];
    }

    public ICollection<TKey> Keys => _Dict.Keys;
    public ICollection<List<TValue>> Values => _Dict.Values;
    public int Count => _Dict.Count;
    public bool IsReadOnly => false;

    public void Add(TKey key, List<TValue> value) => _Dict.Add(key, value);

    public void Add(KeyValuePair<TKey, List<TValue>> item)
      => ((ICollection<KeyValuePair<TKey, List<TValue>>>)_Dict).Add(item);

    public void Clear() => _Dict.Clear();

    public bool Contains(KeyValuePair<TKey, List<TValue>> item)
      => ((ICollection<KeyValuePair<TKey, List<TValue>>>)_Dict).Contains(item);

    public bool ContainsKey(TKey key) => _Dict.ContainsKey(key);

    public void CopyTo(KeyValuePair<TKey, List<TValue>>[] array, int arrayIndex)
      => ((ICollection<KeyValuePair<TKey, List<TValue>>>)_Dict).CopyTo(array, arrayIndex);

    public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator()
      => ((IEnumerable<KeyValuePair<TKey, List<TValue>>>)_Dict).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_Dict).GetEnumerator();

    public bool Remove(TKey key)
    {
      Clear(key);
      return true;
    }
    public bool Remove(KeyValuePair<TKey, List<TValue>> item)
    {
      Clear(item.Key);
      return true;
    }

    public bool TryGetValue(TKey key, out List<TValue> value)
    {
      value = _Dict[key];
      return true;
    }
    #endregion
  }
}