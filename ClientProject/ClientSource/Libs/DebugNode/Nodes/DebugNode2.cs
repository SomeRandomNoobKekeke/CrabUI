using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1, T2> : DebugNodeBase
  {
    public string DefaultMsgFactory(T1 arg1, T2 arg2) => $"{Type}: {arg1}, {arg2}";
    public Func<T1, T2, string> MsgFactory { get; set; }

    private DebugEvent EventFactory(T1 arg1, T2 arg2)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1, arg2 },
        Msg = MsgFactory.Invoke(arg1, arg2),
      };
    }

    public override void Send(object arg1, object arg2) => Send((T1)arg1, (T2)arg2);
    public void Send(T1 arg1, T2 arg2)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1, arg2));
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
    public DebugNode(string type, DebugHub hub, Func<T1, T2, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}