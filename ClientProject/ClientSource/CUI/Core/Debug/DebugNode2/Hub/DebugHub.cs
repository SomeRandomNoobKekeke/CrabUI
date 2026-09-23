using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Hub : Debug2Relay
  {
    public ClearableEvent<Debug2Event> Output { get; } = new();
    public Debug2GateDict Gates { get; } = new();
  }
}