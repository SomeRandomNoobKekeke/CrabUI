using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public abstract class DebugNodeBase : IDebugRelayTarget
  {
    public string Type { get; }
    public DebugHub Hub { get; }
    public DebugGate GlobalGate { get; }
    public bool IsOpen { get; set; }

    public void Open() => IsOpen = true;
    public void Close() => IsOpen = false;
    public void Toggle() => IsOpen = !IsOpen;

    public void Map(DebugRelayBase next) => next.Route(this);

    public virtual void Send() { }
    public virtual void Send(object arg1) { }
    public virtual void Send(object arg1, object arg2) { }
    public virtual void Send(object arg1, object arg2, object arg3) { }
    public virtual void Send(object arg1, object arg2, object arg3, object arg4) { }
    public virtual void Send(object arg1, object arg2, object arg3, object arg4, object arg5) { }

    public DebugNodeBase(string type, DebugHub hub)
    {
      Type = type;
      Hub = hub;
      GlobalGate = Hub.Gates[Type];
      IsOpen = Hub.IsOpen;
    }
  }
}