using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;

namespace CursedUI
{
  public class CUIStylePipeline
  {
    public ReactiveList<ICUIStyle> DefaultStyles = new();
    public ReactiveList<ICUIStyle> UserStyles = new();

    public SimpleWeakEvent Changed { get; } = new();

    public void Apply(CUIVisualComponent component)
    {
      foreach (ICUIStyle style in DefaultStyles)
      {
        style.Apply(component);
      }

      foreach (ICUIStyle style in UserStyles)
      {
        style.Apply(component);
      }
    }

    public CUIStylePipeline()
    {
      UserStyles.Changed += Changed.Raise;
      DefaultStyles.Changed += Changed.Raise;
    }

    public override string ToString()
    {
      return $"{Logger.Wrap.IEnumerable(DefaultStyles, true)}\n{Logger.Wrap.IEnumerable(UserStyles, true)}";
    }
  }
}