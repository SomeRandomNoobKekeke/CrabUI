using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugRelay : IDebugRelayTarget
  {
    protected List<IDebugRelayTarget> Children = new();

    public void Route(IDebugRelayTarget prev) => this.Children.Add(prev);
    public void Unroute(IDebugRelayTarget prev) => this.Children.Remove(prev);

    public void Route(DebugNodeDict nodes) => nodes.Map(this);
    public void Route(DebugRelayDict relays) => relays.Map(this);
    public void Unroute(DebugNodeDict nodes) => nodes.Unmap(this);
    public void Unroute(DebugRelayDict relays) => relays.Unmap(this);



    public IEnumerable<DebugNodeBase> GetNodes(string type)
      => GetNodes().Where(node => node.Type == type);

    public IEnumerable<DebugNodeBase> GetNodes()
    {
      IEnumerable<DebugNodeBase> GetNodesRec(DebugRelay relay)
      {
        foreach (IDebugRelayTarget target in relay.Children)
        {
          if (target is DebugNodeBase)
          {
            yield return target as DebugNodeBase;
          }

          if (target is DebugRelay)
          {
            foreach (DebugNodeBase node in GetNodesRec(target as DebugRelay))
            {
              yield return node;
            }
          }
        }
      }

      foreach (DebugNodeBase node in GetNodesRec(this))
      {
        yield return node;
      }
    }

    public void Map(DebugRelay next) => next.Route(this);
    public void Unmap(DebugRelay next) => next.Unroute(this);

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