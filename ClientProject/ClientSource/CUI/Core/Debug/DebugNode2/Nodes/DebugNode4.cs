using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Node<T1, T2, T3, T4> : Debug2NodeBase
  {
    public string DefaultMsgFactory(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => $"{Type}: {arg1}, {arg2}, {arg3}, {arg4}";
    public Func<T1, T2, T3, T4, string> MsgFactory { get; set; }

    private Debug2Event EventFactory(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      return new Debug2Event()
      {
        Type = Type,
        Args = new object[] { arg1, arg2, arg3, arg4 },
        Msg = MsgFactory.Invoke(arg1, arg2, arg3, arg4),
      };
    }

    public override void Send(object arg1, object arg2, object arg3, object arg4) => Send((T1)arg1, (T2)arg2, (T3)arg3, (T4)arg4);
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1, arg2, arg3, arg4));
      }
    }

    public Debug2Node(string type, Debug2Hub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
    public Debug2Node(string type, Debug2Hub hub, Func<T1, T2, T3, T4, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}