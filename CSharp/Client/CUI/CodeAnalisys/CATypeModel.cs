using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using System.IO;

namespace CrabUI
{

  public class CATypeModel
  {
    static CATypeModel()
    {
      CodeAnalizer.OnClearCache += ClearCache;
    }

    public static void ClearCache() => Cache.Clear();
    private static Dictionary<Type, CATypeModel> Cache = new();

    public static CATypeModel For<T>() => For(typeof(T));
    public static CATypeModel For(Type T)
    {
      if (!Cache.ContainsKey(T)) Cache[T] = new CATypeModel(T);
      return Cache[T];
    }



    public Type Type { get; }

    public List<PropertyInfo> PublicGet { get; } = new();
    public List<PropertyInfo> ProtectedGet { get; } = new();


    private void Analyze()
    {
      foreach (PropertyInfo pi in Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
      {
        if (pi.GetMethod != null)
        {
          if (pi.GetMethod.IsPublic) PublicGet.Add(pi);
          if (pi.GetMethod.IsFamily) ProtectedGet.Add(pi);
        }
      }
    }

    private CATypeModel(Type type)
    {
      Type = type;
      Analyze();
    }
  }
}