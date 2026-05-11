using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1, T2, T3, T4, T5> : ClearableEvent<T1, T2, T3, T4, T5>, IDebugNode
  {
    public string Name { get; set; } = "";
    public ClearableEvent<DebugEvent> Pin { get; } = new();
    public Func<T1, T2, T3, T4, T5, string> MsgFactory
    {
      set
      {
        PinBridge.Action = (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Pin.Raise(
          new DebugEvent()
          {
            Name = Name,
            Args = new object[] { arg1, arg2, arg3, arg4, arg5 },
            Msg = value.Invoke(arg1, arg2, arg3, arg4, arg5),
          }
        );
      }
    }

    void IDebugNode.Send(object arg1, object arg2, object arg3, object arg4, object arg5)
      => Send((T1)arg1, (T2)arg2, (T3)arg3, (T4)arg4, (T5)arg5);
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
      => (this as ClearableEvent<T1, T2, T3, T4, T5>).Raise(arg1, arg2, arg3, arg4, arg5);

    protected EventBridge<T1, T2, T3, T4, T5> PinBridge { get; }
    public DebugNode()
    {
      PinBridge = this.CreateBridge();
      PinBridge.Action = (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Pin.Raise(
        new DebugEvent()
        {
          Name = Name,
          Args = new object[] { arg1, arg2, arg3, arg4, arg5 },
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