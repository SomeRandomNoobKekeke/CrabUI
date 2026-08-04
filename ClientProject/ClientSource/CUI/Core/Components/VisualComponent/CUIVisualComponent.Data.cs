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
    private Dictionary<string, Action<object>> DataSetters { get; } = new();

    protected void AddDataImage(string key, Func<object> getter = null, Action<object> setter = null)
    {
      if (getter != null) DataGetters[key] = getter;
      if (setter != null) DataSetters[key] = setter;
    }

    protected void AddDataImage<T>(string key, Func<T> getter = null, Action<T> setter = null)
    {
      if (getter != null) DataGetters[key] = () => getter();
      if (setter != null) DataSetters[key] = (object value) => setter((T)value);
    }

    public Data_Part Data { get; } = new();
    public class Data_Part : Part
    {
      public object this[string key]
      {
        get => Self.DataGetters.GetValueOrDefault(key)?.Invoke();
        set => Self.DataSetters.GetValueOrDefault(key)?.Invoke(value);
      }

      public T GetData<T>(string key) => (T)Self.DataGetters.GetValueOrDefault(key)?.Invoke();
    }

  }
}