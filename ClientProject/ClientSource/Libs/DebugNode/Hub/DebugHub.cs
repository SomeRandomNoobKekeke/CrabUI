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
    public bool IsOpen { get; set; } = true;

    public void Open()
    {
      IsOpen = true;
      foreach (DebugNodeBase node in GetNodes()) { node.Open(); }
    }
    public void Open(string type)
    {
      IsOpen = true;
      foreach (DebugNodeBase node in GetNodes(type)) { node.Open(); }
    }
    public void Close()
    {
      IsOpen = false;
      foreach (DebugNodeBase node in GetNodes()) { node.Close(); }
    }
    public void Close(string type)
    {
      IsOpen = false;
      foreach (DebugNodeBase node in GetNodes(type)) { node.Close(); }
    }
    public void Toggle()
    {
      foreach (DebugNodeBase node in GetNodes()) { node.Toggle(); }
    }
    public void Toggle(string type)
    {
      foreach (DebugNodeBase node in GetNodes(type)) { node.Toggle(); }
    }
  }
}