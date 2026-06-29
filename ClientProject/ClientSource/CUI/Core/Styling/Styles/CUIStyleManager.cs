using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  public class CUIStyleManager(CUIComponentTypeManager typeManager)
  {
    public CUIComponentTypeManager TypeManager { get; } = typeManager;
    private CUIContextStyleTracker CUIContextStyleTracker { get; } = new();


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

    public void EnterContext<T>(Action<T> action) where T : CUIComponent
    {
      ICUIStyle style = CUIContextStyleTracker.EnterContext<T>(action);
      Styles[typeof(T)].AddSilent(style);
    }

    public void ExitContext()
    {
      (Type, ICUIStyle) result = CUIContextStyleTracker.ExitContext();
      if (result.Item2 is null) return;
      Styles[result.Item1].RemoveSilent(result.Item2);
    }



  }


}