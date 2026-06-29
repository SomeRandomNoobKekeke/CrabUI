using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrabUI
{
  //Experimental trash
  public class CUIContextStyleTracker
  {
    public Type Type { get; private set; }
    public ICUIStyle ContextStyle { get; private set; }

    public ICUIStyle EnterContext<T>(Action<T> action) where T : CUIComponent
    {
      Type = typeof(T);
      ContextStyle = new CUIActionStyle<T>($"context", action);
      return ContextStyle;
    }

    public (Type, ICUIStyle) ExitContext()
    {
      (Type, ICUIStyle) result = (Type, ContextStyle);
      (Type, ContextStyle) = (null, null);
      return result;
    }
  }


}