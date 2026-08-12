using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1, T2, T3> : DebugNodeBase
  {
    public static string DefaultMsgFactory(T1 arg1, T2 arg2, T3 arg3) => $"{arg1}, {arg2}, {arg3}";
    public Func<T1, T2, T3, string> MsgFactory { get; set; } = DefaultMsgFactory;

    private DebugEvent EventFactory(T1 arg1, T2 arg2, T3 arg3)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1, arg2, arg3 },
        Msg = MsgFactory.Invoke(arg1, arg2, arg3),
      };
    }

    public override void Send(object arg1, object arg2, object arg3) => Send((T1)arg1, (T2)arg2, (T3)arg3);
    public void Send(T1 arg1, T2 arg2, T3 arg3)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1, arg2, arg3));
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub) { }
    public DebugNode(string type, DebugHub hub, Func<T1, T2, T3, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}