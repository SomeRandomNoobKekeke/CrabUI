using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Hub : Debug2Relay
  {
    public event Action<Debug2Event> Output;
    public Debug2GateDict Gates { get; } = new();

    public void Send(Debug2Event e) => Output?.Invoke(e);
  }
}