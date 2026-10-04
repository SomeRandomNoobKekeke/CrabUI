using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    private Dictionary<string, object> _Data; public Dictionary<string, object> Data
    {
      get => _Data ??= [];
      set => _Data = value;
    }

    public T GetData<T>(string key)
    {
      if (Data.TryGetValue(key, out object data))
      {
        return (T)data;
      }
      else
      {
        return default;
      }
    }

  }
}