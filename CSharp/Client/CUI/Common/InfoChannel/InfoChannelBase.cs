using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
namespace CrabUI
{
  //TODO add routelist, route with arg mapping
  public abstract class InfoChannelBase : InfoChannelCore
  {
    public Action<InfoChannelBase> OnInstall { set { value?.Invoke(this); } }
    public Func<bool> Condition { get; set; }
    public Action OnSend { set { AddCallback(value); } }
    public void AddCallback(Action callback) => SendEvent += callback;
    public void Map(Action callback) => SendEvent += callback;
    public IEnumerable<Action> MapList { set { foreach (var map in value) Map(map); } }
    public void Map(InfoChannelBase channel) => AddCallback(() => channel.Send());
    public void Route(InfoChannelBase channel) => channel.AddCallback(Send);
    public void RemoveCallback(Action callback) => SendEvent -= callback;

    protected event Action SendEvent;
    protected void Send()
    {
      if (Open && (Condition is null || Condition.Invoke())) SendEvent?.Invoke();
    }
  }

  public abstract class InfoChannelBase<T1> : InfoChannelCore
  {
    public Action<InfoChannelBase<T1>> OnInstall { set { value?.Invoke(this); } }
    public Func<T1, bool> Condition { get; set; }
    public Action<T1> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1> callback) => SendEvent += callback;
    public void Map(Action<T1> callback) => SendEvent += callback;
    public IEnumerable<Action<T1>> MapList { set { foreach (var map in value) Map(map); } }
    public void Map(InfoChannelBase<T1> channel) => AddCallback((arg1) => channel.Send(arg1));
    public void Route(InfoChannelBase<T1> channel) => channel.AddCallback(Send);
    public void RemoveCallback(Action<T1> callback) => SendEvent -= callback;

    protected event Action<T1> SendEvent;
    protected void Send(T1 arg1)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1))) SendEvent?.Invoke(arg1);
    }
  }

  public abstract class InfoChannelBase<T1, T2> : InfoChannelCore
  {
    public Action<InfoChannelBase<T1, T2>> OnInstall { set { value?.Invoke(this); } }
    public Func<T1, T2, bool> Condition { get; set; }
    public Action<T1, T2> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2> callback) => SendEvent += callback;
    public void Map(Action<T1, T2> callback) => SendEvent += callback;
    public IEnumerable<Action<T1, T2>> MapList { set { foreach (var map in value) Map(map); } }
    public void Map(InfoChannelBase<T1, T2> channel) => AddCallback((arg1, arg2) => channel.Send(arg1, arg2));
    public void Route(InfoChannelBase<T1, T2> channel) => channel.AddCallback(Send);
    public void RemoveCallback(Action<T1, T2> callback) => SendEvent -= callback;

    protected event Action<T1, T2> SendEvent;
    protected void Send(T1 arg1, T2 arg2)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2))) SendEvent?.Invoke(arg1, arg2);
    }
  }

  public abstract class InfoChannelBase<T1, T2, T3> : InfoChannelCore
  {
    public Action<InfoChannelBase<T1, T2, T3>> OnInstall { set { value?.Invoke(this); } }
    public Func<T1, T2, T3, bool> Condition { get; set; }
    public Action<T1, T2, T3> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2, T3> callback) => SendEvent += callback;
    public void Map(Action<T1, T2, T3> callback) => SendEvent += callback;
    public IEnumerable<Action<T1, T2, T3>> MapList { set { foreach (var map in value) Map(map); } }
    public void Map(InfoChannelBase<T1, T2, T3> channel) => AddCallback((arg1, arg2, arg3) => channel.Send(arg1, arg2, arg3));
    public void Route(InfoChannelBase<T1, T2, T3> channel) => channel.AddCallback(Send);
    public void RemoveCallback(Action<T1, T2, T3> callback) => SendEvent -= callback;

    protected event Action<T1, T2, T3> SendEvent;
    protected void Send(T1 arg1, T2 arg2, T3 arg3)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2, arg3))) SendEvent?.Invoke(arg1, arg2, arg3);
    }
  }


  public abstract class InfoChannelBase<T1, T2, T3, T4> : InfoChannelCore
  {
    public Action<InfoChannelBase<T1, T2, T3, T4>> OnInstall { set { value?.Invoke(this); } }
    public Func<T1, T2, T3, T4, bool> Condition { get; set; }
    public Action<T1, T2, T3, T4> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2, T3, T4> callback) => SendEvent += callback;
    public void Map(Action<T1, T2, T3, T4> callback) => SendEvent += callback;
    public IEnumerable<Action<T1, T2, T3, T4>> MapList { set { foreach (var map in value) Map(map); } }
    public void Map(InfoChannelBase<T1, T2, T3, T4> channel) => AddCallback((arg1, arg2, arg3, arg4) => channel.Send(arg1, arg2, arg3, arg4));
    public void Route(InfoChannelBase<T1, T2, T3, T4> channel) => channel.AddCallback(Send);
    public void RemoveCallback(Action<T1, T2, T3, T4> callback) => SendEvent -= callback;

    protected event Action<T1, T2, T3, T4> SendEvent;
    protected void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2, arg3, arg4))) SendEvent?.Invoke(arg1, arg2, arg3, arg4);
    }
  }

  public abstract class InfoChannelBase<T1, T2, T3, T4, T5> : InfoChannelCore
  {
    public Action<InfoChannelBase<T1, T2, T3, T4, T5>> OnInstall { set { value?.Invoke(this); } }
    public Func<T1, T2, T3, T4, T5, bool> Condition { get; set; }
    public Action<T1, T2, T3, T4, T5> OnSend { set { AddCallback(value); } }
    public void AddCallback(Action<T1, T2, T3, T4, T5> callback) => SendEvent += callback;
    public void Map(Action<T1, T2, T3, T4, T5> callback) => SendEvent += callback;
    public IEnumerable<Action<T1, T2, T3, T4, T5>> MapList { set { foreach (var map in value) Map(map); } }
    public void Map(InfoChannelBase<T1, T2, T3, T4, T5> channel) => AddCallback((arg1, arg2, arg3, arg4, arg5) => channel.Send(arg1, arg2, arg3, arg4, arg5));
    public void Route(InfoChannelBase<T1, T2, T3, T4, T5> channel) => channel.AddCallback(Send);
    public void RemoveCallback(Action<T1, T2, T3, T4, T5> callback) => SendEvent -= callback;

    protected event Action<T1, T2, T3, T4, T5> SendEvent;
    protected void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
      if (Open && (Condition is null || Condition.Invoke(arg1, arg2, arg3, arg4, arg5))) SendEvent?.Invoke(arg1, arg2, arg3, arg4, arg5);
    }
  }
}