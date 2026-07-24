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
  public class ReactiveDict<TKey, TValue> : IDictionary<TKey, TValue>
  {
    private Dictionary<TKey, TValue> _Dict = new();

    public event Action Changed;

    public TValue this[TKey key]
    {
      get => _Dict[key];
      set
      {
        _Dict[key] = value;
        Changed?.Invoke();
      }
    }

    public void Swap(ReactiveDict<TKey, TValue> other)
    {
      _Dict = other._Dict.CreateCopy();
      Changed?.Invoke();
    }

    public ICollection<TKey> Keys => _Dict.Keys;
    public ICollection<TValue> Values => _Dict.Values;
    public int Count => _Dict.Count;


    public void Add(TKey key, TValue value) => this[key] = value;
    public void Clear() => _Dict.Clear();
    public bool ContainsKey(TKey key) => _Dict.ContainsKey(key);
    public bool Remove(TKey key)
    {
      if (_Dict.Remove(key))
      {
        Changed?.Invoke();
        return true;
      }

      return false;
    }

    public bool TryGetValue(TKey key, out TValue value) => _Dict.TryGetValue(key, out value!);

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get; }
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => this[item.Key] = item.Value;
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
      => ((ICollection<KeyValuePair<TKey, TValue>>)_Dict).Contains(item);
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
      => ((ICollection<KeyValuePair<TKey, TValue>>)_Dict).CopyTo(array, arrayIndex);
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
      => ((ICollection<KeyValuePair<TKey, TValue>>)_Dict).Remove(item);

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
      => _Dict.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
      => _Dict.GetEnumerator();
  }
}