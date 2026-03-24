using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class EventBridge<T1, T2, T3>
  {
    private EventSubscription BridgeSubscription;
    public bool Opened => BridgeSubscription != null;

    public ClearableEvent<T1, T2, T3> SourceEvent { get; set; }
    public Action<T1, T2, T3> Action { get; set; }

    public void Open()
    {
      BridgeSubscription = SourceEvent.Add(Action);
    }

    public void Close()
    {
      BridgeSubscription.Cancel();
      BridgeSubscription = null;
    }

    public EventBridge(ClearableEvent<T1, T2, T3> source = null)
    {
      SourceEvent = source;
    }
  }
}