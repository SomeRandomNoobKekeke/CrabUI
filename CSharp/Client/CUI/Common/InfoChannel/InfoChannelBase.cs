using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
namespace CrabUI
{

  public abstract class InfoChannelBase
  {
    public bool Open { get; set; }
    public Func<bool> Condition { get; set; }
    public Action OnSend { set { AddCallback(value); } }
    public void AddCallback(Action callback) => SendEvent += callback;
    public void RemoveCallback(Action callback) => SendEvent -= callback;

    protected event Action SendEvent;
    protected void Send()
    {
      if (Open && (Condition is null || Condition.Invoke())) SendEvent?.Invoke();
    }
  }

  public abstract class InfoChannelBase<T1>
  {
    public bool Open { get; set; }
    public Func<T1, bool> Condition { get; set; }
    public Action<T1> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1> callback) => SendEvent += callback;
    public void RemoveCallback(Action<T1> callback) => SendEvent -= callback;

    protected event Action<T1> SendEvent;
    protected void Send(T1 arg1)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1))) SendEvent?.Invoke(arg1);
    }
  }

  public abstract class InfoChannelBase<T1, T2>
  {
    public bool Open { get; set; }
    public Func<T1, T2, bool> Condition { get; set; }
    public Action<T1, T2> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2> callback) => SendEvent += callback;
    public void RemoveCallback(Action<T1, T2> callback) => SendEvent -= callback;

    protected event Action<T1, T2> SendEvent;
    protected void Send(T1 arg1, T2 arg2)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2))) SendEvent?.Invoke(arg1, arg2);
    }
  }

  public abstract class InfoChannelBase<T1, T2, T3>
  {
    public bool Open { get; set; }
    public Func<T1, T2, T3, bool> Condition { get; set; }
    public Action<T1, T2, T3> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2, T3> callback) => SendEvent += callback;
    public void RemoveCallback(Action<T1, T2, T3> callback) => SendEvent -= callback;

    protected event Action<T1, T2, T3> SendEvent;
    protected void Send(T1 arg1, T2 arg2, T3 arg3)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2, arg3))) SendEvent?.Invoke(arg1, arg2, arg3);
    }
  }


  public abstract class InfoChannelBase<T1, T2, T3, T4>
  {
    public bool Open { get; set; }
    public Func<T1, T2, T3, T4, bool> Condition { get; set; }
    public Action<T1, T2, T3, T4> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2, T3, T4> callback) => SendEvent += callback;
    public void RemoveCallback(Action<T1, T2, T3, T4> callback) => SendEvent -= callback;

    protected event Action<T1, T2, T3, T4> SendEvent;
    protected void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2, arg3, arg4))) SendEvent?.Invoke(arg1, arg2, arg3, arg4);
    }
  }

  public abstract class InfoChannelBase<T1, T2, T3, T4, T5>
  {
    public bool Open { get; set; }
    public Func<T1, T2, T3, T4, T5, bool> Condition { get; set; }
    public Action<T1, T2, T3, T4, T5> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2, T3, T4, T5> callback) => SendEvent += callback;
    public void RemoveCallback(Action<T1, T2, T3, T4, T5> callback) => SendEvent -= callback;

    protected event Action<T1, T2, T3, T4, T5> SendEvent;
    protected void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2, arg3, arg4, arg5))) SendEvent?.Invoke(arg1, arg2, arg3, arg4, arg5);
    }
  }
}