using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugRelay : DebugRelayBase, IDebugRelayTarget
  {
    private bool isOpen; public bool IsOpen
    {
      get => isOpen;
      set
      {
        isOpen = value;
        foreach (IDebugRelayTarget child in Children)
        {
          child.IsOpen = value;
        }
      }
    }

    public void Open() => IsOpen = true;
    public void Close() => IsOpen = false;
    public void Toggle() => IsOpen = !IsOpen;

    public void Map(DebugRelayBase next) => next.Route(this);

  }
}