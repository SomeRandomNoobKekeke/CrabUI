using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public abstract class Debug2NodeBase : IDebug2RelayTarget
  {
    public string Type { get; }
    public Debug2Hub Hub { get; }
    public Debug2Gate GlobalGate { get; }
    public bool IsOpen { get; set; }

    public void Open() => IsOpen = true;
    public void Close() => IsOpen = false;
    public void Toggle() => IsOpen = !IsOpen;

    public void Map(Debug2RelayBase next) => next.Route(this);

    public Debug2NodeBase(string type, Debug2Hub hub)
    {
      ArgumentNullException.ThrowIfNull(type);
      ArgumentNullException.ThrowIfNull(hub);

      Type = type;
      Hub = hub;
      GlobalGate = Hub.Gates[Type];
      IsOpen = Hub.IsOpen;
    }
  }
}