using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1, T2, T3, T4, T5> : DebugNodeBase
  {
    public Func<T1, T2, T3, T4, T5, string> MsgFactory { get; }

    private DebugEvent EventFactory(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1, arg2, arg3, arg4, arg5 },
        Msg = MsgFactory.Invoke(arg1, arg2, arg3, arg4, arg5),
      };
    }

    public override void Send(object arg1, object arg2, object arg3, object arg4, object arg5) => Send((T1)arg1, (T2)arg2, (T3)arg3, (T4)arg4, (T5)arg5);
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1, arg2, arg3, arg4, arg5));
      }
    }

    public DebugNode(string type, DebugHub hub, Func<T1, T2, T3, T4, T5, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}