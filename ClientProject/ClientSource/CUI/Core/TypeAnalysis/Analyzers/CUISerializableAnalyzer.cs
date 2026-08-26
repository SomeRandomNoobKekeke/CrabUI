using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public class CUISerializableAnalyzer
  {
    public bool IsCUISerializable(Type T) => T.IsAssignableTo(typeof(CUISerializable));
    public bool IsCUISerializableProp(PropertyInfo pi)
      => pi.GetCustomAttribute<CUISerializableProp>() != null;

    public CUISerializableInfo Analyze(Type T)
    {
      CUISerializableInfo info = new()
      {
        SerializableProps = new(),
      };

      AnalyzeContainer(info, T, []);

      return info;
    }

    private void AnalyzeContainer(CUISerializableInfo info, Type T, List<PropertyInfo> path)
    {
      foreach (PropertyInfo pi in T.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        if (IsCUISerializableProp(pi))
        {
          // CheckCringe(pi.PropertyType);

          PropertyPath pp = new PropertyPath(path.Append(pi));

          if (IsCUISerializable(pi.PropertyType))
          {
            AnalyzeContainer(info, pi.PropertyType, pp.Path);
          }
          else
          {
            info.SerializableProps[pp.ToString()] = pp;
          }
        }
      }
    }

    private bool CheckCringe(Type T)
    {
      if (T.IsAssignableTo(typeof(IParsable))) return true;
      if (T.IsPrimitive) return true;
      if (T == typeof(string)) return true;
      if (T.IsEnum) return true;
      if (
        CUIParser.ExtraParseMethods.ContainsKey(T) &&
        CUIParser.ExtraSerializeMethods.ContainsKey(T)
      ) return true;

      if (Nullable.GetUnderlyingType(T) != null && CheckCringe(Nullable.GetUnderlyingType(T))) return true;

      CUI.Logger.Warning($"Khe Khem, {T} can't be serialized properly");

      return false;
    }
  }
}