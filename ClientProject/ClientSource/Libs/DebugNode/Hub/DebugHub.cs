using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugHub : DebugRelay
  {
    public ClearableEvent<DebugEvent> Output { get; } = new();
    public DebugGateDict Gates { get; } = new();
  }
}