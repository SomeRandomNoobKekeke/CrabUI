using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class EventBridge : IEventBridge
  {
    private EventSubscription BridgeSubscription;
    public bool Opened => BridgeSubscription != null;

    public ClearableEvent SourceEvent { get; set; }
    public Action Action { get; set; }

    public void Open()
    {
      BridgeSubscription = SourceEvent.Add(Action);
    }

    public void Close()
    {
      BridgeSubscription.Cancel();
      BridgeSubscription = null;
    }

    public EventBridge(ClearableEvent source = null)
    {
      SourceEvent = source;
    }
  }

  public static partial class ClearableEvent_Extensions
  {
    public static EventBridge CreateBridge(this ClearableEvent self)
      => new EventBridge(self);
  }
}