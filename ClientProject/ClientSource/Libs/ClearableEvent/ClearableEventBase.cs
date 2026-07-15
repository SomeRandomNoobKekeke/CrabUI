using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public abstract class ClearableEventBase : IClearableEvent
  {
    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public ClearableEventBase()
    {
      ID = MaxID++;
    }

    public abstract EventSubscription Add(Delegate callback);
    protected Dictionary<IClearableEvent, EventSubscription> Mapping = new();
    public EventSubscription Map(IClearableEvent next, Delegate callback)
    {
      EventSubscription subscription = Add(callback);
      Mapping[next] = subscription;
      return subscription;
    }
    public void Unmap(IClearableEvent node)
    {
      if (!Mapping.ContainsKey(node)) return;
      Mapping[node].Cancel();
      Mapping.Remove(node);
    }

    public EventSubscription Route(IClearableEvent prev, Delegate callback) => prev.Map(this, callback);
    public void Unroute(IClearableEvent node) => node.Unmap(this);

    protected abstract Delegate DefaultMapping(IClearableEvent next);
    public EventSubscription Map(IClearableEvent next) => Map(next, DefaultMapping(next));
    public EventSubscription Route(IClearableEvent prev) => prev.Map(this);


    public bool IsMapped(IClearableEvent next) => Mapping.ContainsKey(next);
    public bool IsRouted(IClearableEvent prev) => prev.IsMapped(this);

    public virtual void Raise() { }
    public virtual void Raise(object arg1) { }
    public virtual void Raise(object arg1, object arg2) { }
    public virtual void Raise(object arg1, object arg2, object arg3) { }
    public virtual void Raise(object arg1, object arg2, object arg3, object arg4) { }
    public virtual void Raise(object arg1, object arg2, object arg3, object arg4, object arg5) { }

    public override int GetHashCode() => ID;

    //TODO do i even need this?
    // public override bool Equals(object obj)
    // {
    //   //TODO test, i'm sure it's not supposed to work like this
    //   if (obj is not ClearableEventBase other) return false;
    //   return other.ID == ID;
    // }
  }
}