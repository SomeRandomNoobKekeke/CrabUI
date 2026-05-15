using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugRelay : DebugRelayBase, IDebugRelayTarget
  {
    public void Map(DebugRelayBase next) => next.Route(this);

    public void Open()
    {
      foreach (DebugNodeBase node in GetNodes()) { node.Open(); }
    }
    public void Open(string type)
    {
      foreach (DebugNodeBase node in GetNodes(type)) { node.Open(); }
    }
    public void Close()
    {
      foreach (DebugNodeBase node in GetNodes()) { node.Close(); }
    }
    public void Close(string type)
    {
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