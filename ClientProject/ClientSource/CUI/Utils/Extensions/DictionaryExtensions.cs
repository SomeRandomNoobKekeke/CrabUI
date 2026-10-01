using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CursedUI
{
  public static class Dictionary_Extensions
  {
    //omfg
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    {
      if (dict.TryGetValue(key, out TValue value))
      {
        return value;
      }
      return default;
    }
  }
}