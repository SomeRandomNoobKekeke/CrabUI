using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;


namespace CrabUI
{
  public class DebugProbe
  {
    public Dictionary<DebugNodeBase, EventSubscription> Subscriptions = new();

    public ClearableEvent<DebugEvent> Read { get; } = new();

    public void Connect(DebugNodeBase node)
    {
      Subscriptions[node] = node.Pin.Add(e => Read.Raise(e));
    }

    public void Disconnect(DebugNodeBase node)
    {
      Subscriptions[node].Cancel();
      Subscriptions.Remove(node);
    }
  }
}