using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;

namespace CrabUI
{

  public class WeakCatalog<TKey, TValue> where TValue : class
  {
    public Dictionary<TKey, List<WeakReference<TValue>>> Pages = new();

    public Dictionary<TKey, List<WeakReference<TValue>>>.KeyCollection Keys => Pages.Keys;

    public void Add(TKey key, TValue value)
    {
      if (!Pages.ContainsKey(key)) Pages[key] = new();
      Pages[key].Add(new WeakReference<TValue>(value));
    }

    public void Clear() => Pages.Clear();

    public void RemoveEmptyLinks(TKey key)
    {
      if (!Pages.ContainsKey(key))
      {
        Pages[key] = new();
        return;
      }

      Pages[key] = Pages[key].Where(wr =>
      {
        TValue value = null;
        wr.TryGetTarget(out value);
        return value is not null;
      }).ToList();
    }

    public IEnumerable<TValue> GetPage(TKey key)
    {
      RemoveEmptyLinks(key);
      return Pages[key].Select(wr =>
      {
        TValue value = null;
        wr.TryGetTarget(out value);
        return value;
      });
    }
  }
}