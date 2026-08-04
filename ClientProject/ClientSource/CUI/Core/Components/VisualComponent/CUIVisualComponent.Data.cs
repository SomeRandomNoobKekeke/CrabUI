using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    private Dictionary<string, Func<object>> DataGetters { get; } = new();

    protected void AddDataGetter(string key, Func<object> getter)
      => DataGetters[key] = getter;

    public object GetData(string key)
    {
      if (!DataGetters.ContainsKey(key)) return null;
      return DataGetters[key]();
    }

    public T GetData<T>(string key)
    {
      if (!DataGetters.ContainsKey(key)) return default;
      return (T)DataGetters[key]();
    }
  }
}