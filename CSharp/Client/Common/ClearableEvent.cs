using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class ClearableEvent
  {
    private event Action Event;
    public void Add(Action callback) => Event -= callback;
    public void Remove(Action callback) => Event -= callback;
    public void Raise() => Event?.Invoke();
    public void Clear()
    {
      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= callback;
      }
    }
  }

  public class ClearableEvent<T1>
  {
    private event Action<T1> Event;
    public void Add(Action<T1> callback) => Event -= callback;
    public void Remove(Action<T1> callback) => Event -= callback;
    public void Raise(T1 arg1) => Event?.Invoke(arg1);
    public void Clear()
    {
      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= callback;
      }
    }
  }

  public class ClearableEvent<T1, T2>
  {
    private event Action<T1, T2> Event;
    public void Add(Action<T1, T2> callback) => Event -= callback;
    public void Remove(Action<T1, T2> callback) => Event -= callback;
    public void Raise(T1 arg1, T2 arg2) => Event?.Invoke(arg1, arg2);
    public void Clear()
    {
      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= callback;
      }
    }
  }
}