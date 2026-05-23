using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public class CUIComponentAnalyzer
  {
    public CUIComponentInfo Analyze(Type componentType)
    {
      CUIComponentInfo info = new()
      {
        ComponentType = componentType,
        SerializableProps = new Dictionary<string, PropertyInfo>(),
      };

      foreach (PropertyInfo pi in componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        if (pi.GetCustomAttribute<CUISerializable>() != null)
        {
          info.SerializableProps[pi.Name] = pi;
        }
      }

      return info;
    }
  }
}