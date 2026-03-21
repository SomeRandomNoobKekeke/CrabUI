using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class ClearableEvent
  {
    private event Action Event;
    public ClearableEvent Add(Action callback)
    {
      Event += callback;
      return this;
    }
    public ClearableEvent Remove(Action callback)
    {
      Event -= callback;
      return this;
    }
    public void Raise() => Event?.Invoke();
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action)callback;
      }
    }

    public static ClearableEvent operator +(ClearableEvent self, Action action)
    {
      return self.Add(action);
    }

    public static ClearableEvent operator -(ClearableEvent self, Action action)
    {
      return self.Remove(action);
    }
  }

  public class ClearableEvent<T1>
  {
    private event Action<T1> Event;
    public ClearableEvent<T1> Add(Action<T1> callback)
    {
      Event += callback;
      return this;
    }
    public ClearableEvent<T1> Remove(Action<T1> callback)
    {
      Event -= callback;
      return this;
    }
    public void Raise(T1 arg1) => Event?.Invoke(arg1);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1>)callback;
      }
    }

    public static ClearableEvent<T1> operator +(ClearableEvent<T1> self, Action<T1> action)
    {
      return self.Add(action);
    }

    public static ClearableEvent<T1> operator -(ClearableEvent<T1> self, Action<T1> action)
    {
      return self.Remove(action);
    }
  }

  public class ClearableEvent<T1, T2>
  {
    private event Action<T1, T2> Event;
    public ClearableEvent<T1, T2> Add(Action<T1, T2> callback)
    {
      Event += callback;
      return this;
    }
    public ClearableEvent<T1, T2> Remove(Action<T1, T2> callback)
    {
      Event -= callback;
      return this;
    }
    public void Raise(T1 arg1, T2 arg2) => Event?.Invoke(arg1, arg2);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1, T2>)callback;
      }
    }

    public static ClearableEvent<T1, T2> operator +(ClearableEvent<T1, T2> self, Action<T1, T2> action)
    {
      return self.Add(action);
    }

    public static ClearableEvent<T1, T2> operator -(ClearableEvent<T1, T2> self, Action<T1, T2> action)
    {
      return self.Remove(action);
    }
  }

  public class ClearableEvent<T1, T2, T3>
  {
    private event Action<T1, T2, T3> Event;
    public ClearableEvent<T1, T2, T3> Add(Action<T1, T2, T3> callback)
    {
      Event += callback;
      return this;
    }
    public ClearableEvent<T1, T2, T3> Remove(Action<T1, T2, T3> callback)
    {
      Event -= callback;
      return this;
    }
    public void Raise(T1 arg1, T2 arg2, T3 arg3) => Event?.Invoke(arg1, arg2, arg3);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1, T2, T3>)callback;
      }
    }

    public static ClearableEvent<T1, T2, T3> operator +(ClearableEvent<T1, T2, T3> self, Action<T1, T2, T3> action)
    {
      return self.Add(action);
    }

    public static ClearableEvent<T1, T2, T3> operator -(ClearableEvent<T1, T2, T3> self, Action<T1, T2, T3> action)
    {
      return self.Remove(action);
    }
  }


  public class ClearableEvent<T1, T2, T3, T4>
  {
    private event Action<T1, T2, T3, T4> Event;
    public ClearableEvent<T1, T2, T3, T4> Add(Action<T1, T2, T3, T4> callback)
    {
      Event += callback;
      return this;
    }
    public ClearableEvent<T1, T2, T3, T4> Remove(Action<T1, T2, T3, T4> callback)
    {
      Event -= callback;
      return this;
    }
    public void Raise(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Event?.Invoke(arg1, arg2, arg3, arg4);
    public void Clear()
    {
      if (Event is null) return;

      foreach (Delegate callback in Event.GetInvocationList())
      {
        Event -= (Action<T1, T2, T3, T4>)callback;
      }
    }

    public static ClearableEvent<T1, T2, T3, T4> operator +(ClearableEvent<T1, T2, T3, T4> self, Action<T1, T2, T3, T4> action)
    {
      return self.Add(action);
    }

    public static ClearableEvent<T1, T2, T3, T4> operator -(ClearableEvent<T1, T2, T3, T4> self, Action<T1, T2, T3, T4> action)
    {
      return self.Remove(action);
    }
  }
}