using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1, T2, T3, T4> : DebugNodeBase
  {
    public Func<T1, T2, T3, T4, string> MsgFactory { get; }

    private DebugEvent EventFactory(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1, arg2, arg3, arg4 },
        Msg = MsgFactory.Invoke(arg1, arg2, arg3, arg4),
      };
    }

    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1, arg2, arg3, arg4));
      }
    }

    public DebugNode(string type, DebugHub hub, Func<T1, T2, T3, T4, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}