using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;


namespace CrabUI
{
  public class DebugRouter<T1> : DebugNode<T1>
  {
    public Dictionary<DebugNodeBase, EventSubscription> Subscriptions = new();

    public void Route(DebugNode<T1> node)
    {
      Subscriptions[node] = node.Map(this);
    }

    public void Route(DebugNodeBase node, Delegate callback)
    {
      Subscriptions[node] = node.Map(callback);
    }

    public void Forget(DebugNodeBase node)
    {
      Subscriptions[node].Cancel();
    }
  }

  public class DebugRouter<T1, T2> : DebugNode<T1, T2>
  {
    public Dictionary<DebugNodeBase, EventSubscription> Subscriptions = new();

    public void Route(DebugNode<T1, T2> node)
    {
      Subscriptions[node] = node.Map(this);
    }

    public void Route(DebugNodeBase node, Delegate callback)
    {
      Subscriptions[node] = node.Map(callback);
    }

    public void Forget(DebugNodeBase node)
    {
      Subscriptions[node].Cancel();
      Subscriptions.Remove(node);
    }
  }
}