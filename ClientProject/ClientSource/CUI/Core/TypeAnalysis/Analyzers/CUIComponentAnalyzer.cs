using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  /// <summary>
  /// This thing creating CUIComponentInfo from types
  /// </summary>
  public class CUIComponentAnalyzer
  {
    public static string DefaultStylePropName { get; } = "DefaultStyle";

    public bool IsComponentType(Type T) => T.IsAssignableTo(typeof(CUIComponent));

    //TODO add a way to use pregenerated infos
    public CUIComponentInfo Analyze(Type T)
    {
      CUIComponentInfo info = new CUIComponentInfo()
      {
        ComponentType = T,
      };

      PropertyInfo? defaultStyleProp = T.GetProperty(DefaultStylePropName, BindingFlags.Static | BindingFlags.Public);

      if (defaultStyleProp != null)
      {
        info.DefaultStyle = (ICUIStyle)defaultStyleProp.GetValue(null);
      }

      // CRINGE i have to create defaults from withing CUICore
      // info.DefaultValue = CreateDefault(T); 

      return info;
    }

    public CUIComponent CreateDefault(Type T)
    {
      if (T.IsAbstract) return null;

      if (T.GetConstructor([]) is null)
      {
        // CUI.Logger.Warning($"Failed to create default for [{info.ComponentType.Name}]: {info.ComponentType} doesn't have default constructor");
        return null;
      }

      if (T.GetCustomAttribute<NoDefaultAttribute>() != null) return null;

      try
      {
        return (CUIComponent)Activator.CreateInstance(T);
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Failed to create default for [{T.Name}]: {e.InnerException?.Message}");
      }

      return null;
    }
  }
}