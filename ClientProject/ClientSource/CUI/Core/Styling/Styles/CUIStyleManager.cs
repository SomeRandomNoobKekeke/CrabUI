using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUIStyleManager(CUIComponentTypeManager typeManager)
  {
    public CUIComponentTypeManager TypeManager { get; } = typeManager;


    /// <summary>
    /// Cheap and hacky
    /// New CUIComponents take this value and subscribe to Style.Changed only if it's true
    /// </summary>
    public bool UseReactiveStyles { get; set; } = false;

    public Dictionary<Type, CUIStylePipeline> Styles { get; } = new();

    public CUIStylePipeline Get<T>() => Get(typeof(T));
    public CUIStylePipeline Get(Type T)
    {
      if (!Styles.ContainsKey(T)) Styles[T] = new();
      return Styles[T];
    }

    public void AddStyle(ICUIStyle style)
    {
      foreach (Type T in TypeManager.GetDerivedTypes(style.TargetType))
      {
        Get(T).Add(style);
      }
    }

    public void RemoveStyle(ICUIStyle style)
    {
      foreach (Type T in TypeManager.GetDerivedTypes(style.TargetType))
      {
        Get(T).Remove(style);
      }
    }
  }


}