using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public Dictionary_Part As_Dictionary { get; } = new();
    public class Dictionary_Part : Part, IDictionary<string, object>
    {
      public CUIComponentInfo Info => Self.Info;

      public object this[string key]
      {
        get
        {
          PropertyInfo pi = Info.SerializableProps[key];
          return pi.GetValue(Self);
        }
        set => Info.SerializableProps[key].SetValue(Self, value);
      }

      #region IDictionary<string, object>
      public ICollection<string> Keys => Info.SerializableProps.Keys;
      public ICollection<object> Values => Info.SerializableProps.Values.Select(
        p => p.GetValue(Self)
      ).ToArray();
      public bool ContainsKey(string key) => Info.SerializableProps.ContainsKey(key);
      public void Add(string key, object value) => throw new NotImplementedException();
      public bool Remove(string key) => throw new NotImplementedException();
      public bool TryGetValue(string key, out object value)
      {
        if (ContainsKey(key))
        {
          value = this[key]; return true;
        }
        value = null; return false;
      }
      #endregion


      #region ICollection<KeyValuePair<string, object>>
      void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> kvp)
        => Add(kvp.Key, kvp.Value);
      bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> kvp)
        => ContainsKey(kvp.Key);
      bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> kvp)
        => Remove(kvp.Key);
      void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int index)
        => throw new NotImplementedException("too lazy");

      public void Clear() => throw new NotImplementedException();
      public int Count => Info.SerializableProps.Count;

      bool ICollection<KeyValuePair<string, object>>.IsReadOnly => false;
      #endregion

      #region IEnumerable
      public IEnumerable<KeyValuePair<string, object>> Enumerate()
      {
        foreach (string key in Keys)
        {
          yield return new KeyValuePair<string, object>(key, this[key]);
        }
      }

      IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        => Enumerate().GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator()
        => Enumerate().GetEnumerator();
      #endregion
    }

  }
}