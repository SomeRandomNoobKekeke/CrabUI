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

    public void Analyze(CUIComponentInfo info)
    {
      PropertyInfo defaultStyleProp = info.ComponentType.GetProperty(
        DefaultStylePropName,
        BindingFlags.Static | BindingFlags.Public
      );

      if (defaultStyleProp != null)
      {
        info.DefaultStyle = (ICUIStyle)defaultStyleProp.GetValue(null);
      }
    }

    public void CreateDefault(CUIComponentInfo info)
    {
      if (info.ComponentType.IsAbstract) return;

      if (info.ComponentType.GetConstructor([]) is null)
      {
        // CUI.Logger.Warning($"Failed to create default for [{info.ComponentType.Name}]: {info.ComponentType} doesn't have default constructor");
        return;
      }

      if (info.ComponentType.GetCustomAttribute<NoDefaultAttribute>() != null) return;

      try
      {
        info.DefaultValue = (CUIComponent)Activator.CreateInstance(info.ComponentType);
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Failed to create default for [{info.ComponentType.Name}]: {e.InnerException?.Message}");
      }
    }
  }
}