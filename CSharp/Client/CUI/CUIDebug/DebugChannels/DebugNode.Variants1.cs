using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class DebugNode<T1> : DebugNode
  {
    private Func<T1, DebugEvent> factory;
    public Func<T1, DebugEvent> Factory
    {
      get => factory;
      set
      {
        factory = value;
        Event_Pin.Action = factory is null ? DefaultBringeAction : CustomBringeAction;
      }
    }

    public Dictionary<DebugNode, EventSubscription> Mapping = new();

    private void DefaultBringeAction(T1 arg1)
    {
      Pin.Raise(new DebugEvent()
      {
        Args = new object[] { arg1 },
      });
    }
    private void CustomBringeAction(T1 arg1)
    {
      Pin.Raise(Factory.Invoke(arg1));
    }

    private EventBridge<T1> Event_Pin;
    public ClearableEvent<T1> Event { get; } = new();

    public void Send(T1 arg1) => Event.Raise(arg1);
    public EventSubscription Map(DebugNode next, Action<T1> callback)
    {
      EventSubscription subscription = Event.Add(callback);
      Mapping[next] = subscription; //TODO handle dublicates
      return subscription;
    }

    protected override EventSubscription AddToEvent(Delegate callback)
      => Event.Add((Action<T1>)callback);
    public override EventSubscription Map(DebugNode next, Delegate callback) => Map(next, (Action<T1>)callback);
    public override EventSubscription Map(DebugNode next) => Map((DebugNode<T1>)next);
    public EventSubscription Map(DebugNode<T1> next)
    {
      return Map(next, (T1 arg1) => next.Event.Raise(arg1));
    }
    public EventSubscription Route(DebugNode<T1> prev) => prev.Map(this);


    public override void Unmap(DebugNode node)
    {
      Mapping[node].Cancel();
      Mapping.Remove(node);
    }

    public override EventSubscription Map(DebugNode<object> next)
      => Map(next, (arg1) => next.Event.Raise(arg1));
    public override EventSubscription Map(DebugNode<object, object> next)
      => Map(next, (arg1) => next.Event.Raise(arg1, null));
    public override EventSubscription Map(DebugNode<object, object, object> next)
      => Map(next, (arg1) => next.Event.Raise(arg1, null, null));

    public DebugNode()
    {
      Event_Pin = Event.CreateBridge(DefaultBringeAction);

      Pin.OnSubscribed += (_) =>
      {
        if (!Pin.Empty && !Event_Pin.Opened) Event_Pin.Open();
      };

      Pin.OnUnSubscribed += (_) =>
      {
        if (Pin.Empty && Event_Pin.Opened) Event_Pin.Close();
      };
    }
  }
}