using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1, T2, T3> : ClearableEvent<T1, T2, T3>, IDebugNode
  {
    public string Name { get; set; } = "";
    public ClearableEvent<DebugEvent> Pin { get; } = new();
    public Func<T1, T2, T3, string> MsgFactory
    {
      set
      {
        PinBridge.Action = (T1 arg1, T2 arg2, T3 arg3) => Pin.Raise(
          new DebugEvent()
          {
            Name = Name,
            Args = new object[] { arg1, arg2, arg3 },
            Msg = value.Invoke(arg1, arg2, arg3),
          }
        );
      }
    }

    void IDebugNode.Send(object arg1, object arg2, object arg3)
      => Send((T1)arg1, (T2)arg2, (T3)arg3);
    public void Send(T1 arg1, T2 arg2, T3 arg3)
      => (this as ClearableEvent<T1, T2, T3>).Raise(arg1, arg2, arg3);

    protected EventBridge<T1, T2, T3> PinBridge { get; }
    public DebugNode()
    {
      PinBridge = this.CreateBridge();
      PinBridge.Action = (T1 arg1, T2 arg2, T3 arg3) => Pin.Raise(
        new DebugEvent()
        {
          Name = Name,
          Args = new object[] { arg1, arg2, arg3 },
        }
      );

      Pin.OnSubscribed += (_) =>
      {
        if (!Pin.Empty && !PinBridge.Opened) PinBridge.Open();
      };

      Pin.OnUnSubscribed += (_) =>
      {
        if (Pin.Empty && PinBridge.Opened) PinBridge.Close();
      };
    }
  }
}