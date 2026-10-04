using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CursedUI
{

  public partial class CUICore : IComponent
  {
    /// <summary>
    /// Trash can for various singletons  
    /// They are stored in CUICore so they will be thrown away on CUI.Stop
    /// </summary>
    public class Singletons_Module
    {
      public Dictionary<Type, object> Values { get; } = [];

      public void AddOrUpdate(object singleton)
      {
        ArgumentNullException.ThrowIfNull(singleton);
        Values[singleton.GetType()] = singleton;
      }

      public bool Has(Type T) => Values.ContainsKey(T);
      public bool Has<T>() => Values.ContainsKey(typeof(T));

      public void Get(Type T) => Values.GetValueOrDefault(T);
      public void Get<T>() => Values.GetValueOrDefault(typeof(T));

      public T GetOrUpdate<T>(Func<T> factory) => (T)GetOrUpdate(typeof(T), () => factory());
      public object GetOrUpdate(Type T, Func<object> factory)
      {
        if (Values.TryGetValue(T, out object value)) return value;

        Values[T] = factory();
        return Values[T];
      }
    }

    private Singletons_Module _Singletons = new();
    public static Singletons_Module Singletons => Instance?._Singletons;
  }
}