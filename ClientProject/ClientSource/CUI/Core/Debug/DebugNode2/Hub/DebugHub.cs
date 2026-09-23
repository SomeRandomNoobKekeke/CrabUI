using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Hub : Debug2RelayBase
  {
    public ClearableEvent<Debug2Event> Output { get; } = new();
    public Debug2GateDict Gates { get; } = new();
    public bool IsOpen { get; set; } = true;

    public void Open()
    {
      IsOpen = true;
      foreach (Debug2NodeBase node in GetNodes()) { node.Open(); }
    }
    public void Open(string type)
    {
      IsOpen = true;
      foreach (Debug2NodeBase node in GetNodes(type)) { node.Open(); }
    }
    public void Close()
    {
      IsOpen = false;
      foreach (Debug2NodeBase node in GetNodes()) { node.Close(); }
    }
    public void Close(string type)
    {
      IsOpen = false;
      foreach (Debug2NodeBase node in GetNodes(type)) { node.Close(); }
    }
    public void Toggle()
    {
      foreach (Debug2NodeBase node in GetNodes()) { node.Toggle(); }
    }
    public void Toggle(string type)
    {
      foreach (Debug2NodeBase node in GetNodes(type)) { node.Toggle(); }
    }
  }
}