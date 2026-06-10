using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class EventBridge<T1, T2, T3, T4, T5> : IEventBridge
  {
    private EventSubscription BridgeSubscription;
    public bool Opened => BridgeSubscription != null;

    public ClearableEvent<T1, T2, T3, T4, T5> SourceEvent { get; set; }
    public Action<T1, T2, T3, T4, T5> Action { get; set; }

    public void Open()
    {
      BridgeSubscription = SourceEvent.Add(Action);
    }

    public void Close()
    {
      BridgeSubscription.Cancel();
      BridgeSubscription = null;
    }

    public EventBridge(ClearableEvent<T1, T2, T3, T4, T5> source = null)
    {
      SourceEvent = source;
    }
  }

  public static partial class ClearableEvent_Extensions
  {
    public static EventBridge<T1, T2, T3, T4, T5> CreateBridge<T1, T2, T3, T4, T5>(this ClearableEvent<T1, T2, T3, T4, T5> self)
      => new EventBridge<T1, T2, T3, T4, T5>(self);
  }
}