using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  /// <summary>
  /// Debug2Relay or Debug2Hub
  /// </summary>
  public abstract class Debug2RelayBase
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
      IEnumerable<Debug2NodeBase> GetNodesRec(Debug2RelayBase relay)
      {
        foreach (IDebug2RelayTarget target in relay.Children)
        {
          if (target is Debug2NodeBase)
          {
            yield return target as Debug2NodeBase;
          }

          if (target is Debug2RelayBase)
          {
            foreach (Debug2NodeBase node in GetNodesRec(target as Debug2RelayBase))
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


  }
}