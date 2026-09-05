using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  /// <summary>
  /// This thing creating CUIComponentInfo from types
  /// </summary>
  public class CUIVisualComponentAnalyzer
  {
    public static string DefaultStylePropName { get; } = "DefaultStyle";

    public bool IsCUIVisualComponentType(Type T) => T.IsAssignableTo(typeof(CUIVisualComponent));

    //TODO add a way to use pregenerated infos
    public CUIVisualComponentInfo Analyze(Type T)
    {
      CUIVisualComponentInfo info = new CUIVisualComponentInfo()
      {
        ComponentType = T,
        NoDefault = T.GetCustomAttribute<NoDefaultAttribute>() != null,
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


  }
}