using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Relay : Debug2RelayBase, IDebug2RelayTarget
  {
    public void Map(Debug2RelayBase next) => next.Route(this);
    public void Unmap(Debug2RelayBase next) => next.Unroute(this);

    public void Open()
    {
      foreach (Debug2NodeBase node in GetNodes()) { node.Open(); }
    }
    public void Open(string type)
    {
      foreach (Debug2NodeBase node in GetNodes(type)) { node.Open(); }
    }
    public void Close()
    {
      foreach (Debug2NodeBase node in GetNodes()) { node.Close(); }
    }
    public void Close(string type)
    {
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