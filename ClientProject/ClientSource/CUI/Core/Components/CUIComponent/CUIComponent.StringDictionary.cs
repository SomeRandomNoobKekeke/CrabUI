using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    public StringDictionary_Part As_StringDictionary { get; } = new();
    public class StringDictionary_Part : Part, IDictionary<string, string>
    {
      public CUIComponentInfo Info => Self.Info;

      public string this[string key]
      {
        get
        {
          PropertyPath pp = Info.SerializableProps[key];
          return CUICore.Parser.Serialize(pp.GetValue(Self));
        }
        set => Info.SerializableProps[key].SetValue(
          Self,
          CUICore.Parser.Parse(value, Info.SerializableProps[key].Type)
        );
      }

      #region IDictionary<string, string>
      public ICollection<string> Keys => Info.SerializableProps.Keys;
      public ICollection<string> Values => Info.SerializableProps.Values.Select(
        p => CUICore.Parser.Serialize(p.GetValue(Self))
      ).ToArray();
      public bool ContainsKey(string key) => Info.SerializableProps.ContainsKey(key);
      public void Add(string key, string value) => throw new NotImplementedException();
      public bool Remove(string key) => throw new NotImplementedException();
      public bool TryGetValue(string key, out string value)
      {
        if (ContainsKey(key))
        {
          value = this[key]; return true;
        }
        value = null; return false;
      }
      #endregion


      #region ICollection<KeyValuePair<string, string>>
      void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> kvp)
        => Add(kvp.Key, kvp.Value);
      bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> kvp)
        => ContainsKey(kvp.Key);
      bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> kvp)
        => Remove(kvp.Key);
      void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int index)
        => throw new NotImplementedException("too lazy");

      public void Clear() => throw new NotImplementedException();
      public int Count => Info.SerializableProps.Count;

      bool ICollection<KeyValuePair<string, string>>.IsReadOnly => false;
      #endregion

      #region IEnumerable
      public IEnumerable<KeyValuePair<string, string>> Enumerate()
      {
        foreach (string key in Keys)
        {
          yield return new KeyValuePair<string, string>(key, this[key]);
        }
      }

      IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        => Enumerate().GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator()
        => Enumerate().GetEnumerator();
      #endregion
    }

  }
}