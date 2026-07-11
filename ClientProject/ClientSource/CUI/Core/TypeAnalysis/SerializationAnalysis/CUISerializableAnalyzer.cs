using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
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
  }
}