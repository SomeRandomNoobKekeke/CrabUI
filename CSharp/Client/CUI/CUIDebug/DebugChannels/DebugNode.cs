using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class DebugNode
  {
    private Func<object, object, DebugEvent> factory;
    public Func<object, object, DebugEvent> Factory
    {
      get => factory;
      set
      {
        factory = value;
        Event_Pin.Action = factory is null ? DefaultBringeAction : CustomBringeAction;
      }
    }

    private void DefaultBringeAction(object arg1, object arg2)
    {
      Pin.Raise(new DebugEvent()
      {
        Args = new object[] { arg1, arg2 },
      });
    }
    private void CustomBringeAction(object arg1, object arg2)
    {
      Pin.Raise(Factory.Invoke(arg1, arg2));
    }

    private EventBridge<object, object> Event_Pin;
    public ClearableEvent<DebugEvent> Pin { get; } = new();
    public ClearableEvent<object, object> Event { get; } = new();

    public void Send(object arg1, object arg2) => Event.Raise(arg1, arg2);


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