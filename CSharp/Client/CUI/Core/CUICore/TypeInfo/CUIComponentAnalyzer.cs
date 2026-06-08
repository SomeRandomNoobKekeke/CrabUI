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
    public static IEnumerable<Type> GetCUIComponentTypeChain(Type T) // where T : CUIComponent
    {
      yield return T;

      Type baseType = T.BaseType;
      while (baseType != null && baseType.IsAssignableTo(typeof(CUIComponent)))
      {
        yield return baseType;
        baseType = baseType.BaseType;
      }
    }

    public bool IsComponentType(Type T)
      => T.IsAssignableTo(typeof(CUIComponent));

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

    public IEnumerable<Type> FindAllComponentTypesInAssembly(Assembly assembly)
      => assembly.GetTypes().Where(T => IsComponentType(T));

    public IEnumerable<CUIComponentInfo> AnalyzeAssembly(Assembly assembly)
    {
      foreach (Type T in FindAllComponentTypesInAssembly(assembly))
      {
        yield return Analyze(T);
      }
    }


  }
}