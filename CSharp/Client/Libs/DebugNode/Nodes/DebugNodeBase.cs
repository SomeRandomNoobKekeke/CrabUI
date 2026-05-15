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

    public DebugNodeBase(string type, DebugHub hub)
    {
      Type = type;
      Hub = hub;
      GlobalGate = Hub.Gates[Type];
      IsOpen = Hub.DefaultIsOpen;
    }
  }
}