using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugHub : DebugRelayBase
  {
    public ClearableEvent<DebugEvent> Output { get; } = new();
    public DebugGateDict Gates { get; } = new();
    public bool DefaultIsOpen { get; set; } = true;
  }
}