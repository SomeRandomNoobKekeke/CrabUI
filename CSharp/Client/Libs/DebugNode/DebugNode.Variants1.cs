using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1> : ClearableEvent<T1>, IDebugNode
  {
    public string Name { get; set; } = "";
    public ClearableEvent<DebugEvent> Pin { get; } = new();
    public Func<T1, string> MsgFactory
    {
      set
      {
        PinBridge.Action = (T1 arg1) => Pin.Raise(
          new DebugEvent()
          {
            Name = Name,
            Args = new object[] { arg1 },
            Msg = value.Invoke(arg1),
          }
        );
      }
    }

    void IDebugNode.Send(object arg1)
      => Send((T1)arg1);
    public void Send(T1 arg1)
      => (this as ClearableEvent<T1>).Raise(arg1);

    protected EventBridge<T1> PinBridge { get; }
    public DebugNode()
    {
      PinBridge = this.CreateBridge();
      PinBridge.Action = (T1 arg1) => Pin.Raise(
        new DebugEvent()
        {
          Name = Name,
          Args = new object[] { arg1 },
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