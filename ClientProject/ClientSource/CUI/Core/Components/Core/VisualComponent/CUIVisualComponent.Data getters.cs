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
  // TODO interesting but i think it encourages bad design with upward data flow
  // public partial class CUIVisualComponent
  // {
  //   private Dictionary<string, Func<object>> DataGetters { get; } = new();
  //   private Dictionary<string, Action<object>> DataSetters { get; } = new();

  //   protected void AddDataImage(string key, Func<object> getter = null, Action<object> setter = null)
  //   {
  //     if (getter != null) DataGetters[key] = getter;
  //     if (setter != null) DataSetters[key] = setter;
  //   }

  //   protected void AddDataImage<T>(string key, Func<T> getter = null, Action<T> setter = null)
  //   {
  //     if (getter != null) DataGetters[key] = () => getter();
  //     if (setter != null) DataSetters[key] = (object value) => setter((T)value);
  //   }

  //   public Data_Part Data { get; } = new();
  //   public class Data_Part : Part
  //   {
  //     private Func<object> SafeGetGetter(string key)
  //     {
  //       if (key is null || !Self.DataGetters.ContainsKey(key))
  //       {
  //         CUI.Logger.Warning($"No data getter with key [{key}] on [{Self}]"); return null;
  //       }

  //       return Self.DataGetters[key];
  //     }

  //     private Action<object> SafeGetSetter(string key)
  //     {
  //       if (key is null || !Self.DataSetters.ContainsKey(key))
  //       {
  //         CUI.Logger.Warning($"No data setter with key [{key}] on [{Self}]"); return null;
  //       }

  //       return Self.DataSetters[key];
  //     }


  //     public object this[string key]
  //     {
  //       get => SafeGetGetter(key)?.Invoke();
  //       set => SafeGetSetter(key)?.Invoke(value);
  //     }

  //     public T GetData<T>(string key) => (T)SafeGetGetter(key)?.Invoke();
  //   }

  // }
}