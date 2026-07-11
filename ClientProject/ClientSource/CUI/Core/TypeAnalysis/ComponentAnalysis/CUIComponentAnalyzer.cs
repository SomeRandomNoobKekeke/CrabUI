using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;

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
    public CUIComponentInfo Analyze(Type componentType)
    {
      CUIComponentInfo info = new()
      {
        ComponentType = componentType,
      };

      PropertyInfo defaultStyleProp = componentType.GetProperty(
        DefaultStylePropName,
        BindingFlags.Static | BindingFlags.Public
      );

      if (defaultStyleProp != null)
      {
        info.DefaultStyle = (ICUIStyle)defaultStyleProp.GetValue(null);
      }

      return info;
    }
  }
}