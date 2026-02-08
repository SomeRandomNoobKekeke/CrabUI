using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public abstract class TypeInfoCache
  {
    public static Dictionary<Type, TypeInfoCache> AllCaches = new();
    public static void ClearAll()
    {
      foreach (TypeInfoCache cache in AllCaches.Values)
      {
        cache.Clear();
      }
    }
    public abstract void Clear();
  }

  public class TypeInfoCache<T> : TypeInfoCache
  {
    private Dictionary<Type, T> Cache = new();
    private ConstructorInfo Constructor;
    public override void Clear() => Cache.Clear();

    public T Get<Key>() => Get(typeof(Key));
    public T Get(Type key)
    {
      if (!Cache.ContainsKey(key))
      {
        Cache[key] = (T)Constructor.Invoke(new object[] { key });
      }

      return Cache[key];
    }

    public TypeInfoCache() : base()
    {
      AllCaches[typeof(T)] = this;

      Constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, new Type[] { typeof(Type) });
      if (Constructor is null)
      {
        throw new ArgumentException($"Cant create type cache of [{typeof(T)}], if doesn't have Constructor(Type T)");
      }
    }
  }


}
