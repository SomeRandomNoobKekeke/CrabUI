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
    /// <summary>
    /// Don't do this, ever
    /// You'll get:
    /// The call is ambiguous between the following methods or properties: 
    /// 'System.Collections.Generic.CollectionExtensions.GetValueOrDefault<TKey, TValue>(System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>, TKey)' and 
    /// 'CursedUI.Dictionary_Extensions.GetValueOrDefault<TKey, TValue>(System.Collections.Generic.IDictionary<TKey, TValue>, TKey)'CS0121
    /// </summary>
    // public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    // {
    //   if (dict.TryGetValue(key, out TValue value))
    //   {
    //     return value;
    //   }
    //   return default;
    // }
  }
}