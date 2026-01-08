using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIEvent
  {
    private event CUIEventHandler TheEvent;

    public void Add(CUIEventHandler callback) => TheEvent += callback;
    public void Remove(CUIEventHandler callback) => TheEvent -= callback;
    public void Raise()
    {
      TheEvent?.Invoke();
    }

    public static CUIEvent operator +(CUIEvent e, CUIEventHandler callback)
    {
      e.Add(callback);
      return e;
    }

    public static CUIEvent operator -(CUIEvent e, CUIEventHandler callback)
    {
      e.Remove(callback);
      return e;
    }
    public CUIEvent() { }
  }

  public class CUIEvent<T1>
  {
    private event CUIEventHandler<T1> TheEvent;

    public void Add(CUIEventHandler<T1> callback) => TheEvent += callback;
    public void Remove(CUIEventHandler<T1> callback) => TheEvent -= callback;
    public void Raise(T1 arg1)
    {
      TheEvent?.Invoke(arg1);
    }

    public static CUIEvent<T1> operator +(CUIEvent<T1> e, CUIEventHandler<T1> callback)
    {
      e.Add(callback);
      return e;
    }

    public static CUIEvent<T1> operator -(CUIEvent<T1> e, CUIEventHandler<T1> callback)
    {
      e.Remove(callback);
      return e;
    }
    public CUIEvent() { }
  }


}