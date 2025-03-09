using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  //xd
  public class Indexer<TKey, TValue>
  {
    public Func<TKey, TValue> Get;
    public Action<TKey, TValue> Set;

    public TValue this[TKey key]
    {
      get => Get(key);
      set => Set(key, value);
    }

    public Indexer(Func<TKey, TValue> get, Action<TKey, TValue> set) => (Get, Set) = (get, set);
  }
}