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
    /// <summary>
    /// You can attach some simple metadata to components
    /// </summary>
    public Data_Part Data { get; } = new();
    public class Data_Part : Part
    {
      private Dictionary<string, object> _Values; private Dictionary<string, object> Values
      {
        get => _Values ??= new();
      }

      public object this[string key]
      {
        get => Values[key];
        set => Values[key] = value;
      }

      public T Get<T>(string key) => (T)this[key];
    }

  }
}