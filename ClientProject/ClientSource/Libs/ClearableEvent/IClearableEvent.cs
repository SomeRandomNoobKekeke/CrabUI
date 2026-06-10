using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public interface IClearableEvent
  {
    public EventSubscription Add(Delegate callback);

    public EventSubscription Map(IClearableEvent next, Delegate callback);
    public void Unmap(IClearableEvent node);

    public EventSubscription Route(IClearableEvent prev, Delegate callback);
    public void Unroute(IClearableEvent node);

    public EventSubscription Map(IClearableEvent next);
    public EventSubscription Route(IClearableEvent prev);

    public bool IsMapped(IClearableEvent next);
    public bool IsRouted(IClearableEvent prev);

    public void Raise() { }
    public void Raise(object arg1) { }
    public void Raise(object arg1, object arg2) { }
    public void Raise(object arg1, object arg2, object arg3) { }
    public void Raise(object arg1, object arg2, object arg3, object arg4) { }
    public void Raise(object arg1, object arg2, object arg3, object arg4, object arg5) { }
  }
}