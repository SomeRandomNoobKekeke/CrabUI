using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode : ClearableEvent, IDebugNode
  {
    public string Name { get; set; } = "";
    public ClearableEvent<DebugEvent> Pin { get; } = new();
    public Func<string> MsgFactory
    {
      set
      {
        PinBridge.Action = () => Pin.Raise(
          new DebugEvent()
          {
            Name = Name,
            Args = new object[] { },
            Msg = value.Invoke(),
          }
        );
      }
    }

    protected EventBridge PinBridge { get; }
    public DebugNode()
    {
      PinBridge = this.CreateBridge();
      PinBridge.Action = () => Pin.Raise(
        new DebugEvent()
        {
          Name = Name,
          Args = new object[] { }
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