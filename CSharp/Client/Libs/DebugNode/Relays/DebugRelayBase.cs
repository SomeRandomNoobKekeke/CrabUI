using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// DebugRelay or DebugHub
  /// </summary>
  public abstract class DebugRelayBase
  {
    protected List<IDebugRelayTarget> Children = new();

    public void Route(IDebugRelayTarget prev) => this.Children.Add(prev);

    public void Route(DebugNodeDict nodes) => nodes.Map(this);
    public void Route(DebugRelayDict relays) => relays.Map(this);


    public IEnumerable<DebugNodeBase> GetNodes(string type)
      => GetNodes().Where(node => node.Type == type);

    public IEnumerable<DebugNodeBase> GetNodes()
    {
      IEnumerable<DebugNodeBase> GetNodesRec(DebugRelayBase relay)
      {
        foreach (IDebugRelayTarget target in relay.Children)
        {
          if (target is DebugNodeBase)
          {
            yield return target as DebugNodeBase;
          }

          if (target is DebugRelayBase)
          {
            foreach (DebugNodeBase node in GetNodesRec(target as DebugRelayBase))
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


  }
}