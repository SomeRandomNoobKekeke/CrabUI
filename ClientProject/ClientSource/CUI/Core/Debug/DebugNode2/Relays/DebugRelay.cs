using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public abstract class Debug2Relay : IDebug2RelayTarget
  {
    protected List<IDebug2RelayTarget> Children = new();

    public void Route(IDebug2RelayTarget prev) => this.Children.Add(prev);
    public void Unroute(IDebug2RelayTarget prev) => this.Children.Remove(prev);

    public void Route(Debug2NodeDict nodes) => nodes.Map(this);
    public void Route(Debug2RelayDict relays) => relays.Map(this);
    public void Unroute(Debug2NodeDict nodes) => nodes.Unmap(this);
    public void Unroute(Debug2RelayDict relays) => relays.Unmap(this);



    public IEnumerable<Debug2NodeBase> GetNodes(string type)
      => GetNodes().Where(node => node.Type == type);

    public IEnumerable<Debug2NodeBase> GetNodes()
    {
      IEnumerable<Debug2NodeBase> GetNodesRec(Debug2Relay relay)
      {
        foreach (IDebug2RelayTarget target in relay.Children)
        {
          if (target is Debug2NodeBase)
          {
            yield return target as Debug2NodeBase;
          }

          if (target is Debug2Relay)
          {
            foreach (Debug2NodeBase node in GetNodesRec(target as Debug2Relay))
            {
              yield return node;
            }
          }
        }
      }

      foreach (Debug2NodeBase node in GetNodesRec(this))
      {
        yield return node;
      }
    }

    public void Map(Debug2Relay next) => next.Route(this);
    public void Unmap(Debug2Relay next) => next.Unroute(this);

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