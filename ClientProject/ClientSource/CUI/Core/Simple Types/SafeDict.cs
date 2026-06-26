using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class SafeDict<TKey, TValue> : Dictionary<TKey, TValue>
  {
    public TValue Default { get; set; }

    public new TValue this[TKey key]
    {
      get
      {
        if (TryGetValue(key, out TValue value))
        {
          return value;
        }
        return Default;
      }
      set => Add(key, value);
    }
  }
}