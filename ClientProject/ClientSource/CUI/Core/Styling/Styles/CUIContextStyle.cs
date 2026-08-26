using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CursedUI
{

  public class CUIContextStyle<T> : IDisposable where T : CUIVisualComponent
  {
    public CUIActionStyle Style { get; }
    public Type Type => typeof(T);

    public void Dispose()
    {
      CUICore.Styles.ExitContextStyle(Type, Style);
    }

    public CUIContextStyle(Action<T> action)
    {
      Style = new CUIActionStyle<T>("context", action);
      CUICore.Styles.EnterContextStyle(Type, Style);
    }
  }


}