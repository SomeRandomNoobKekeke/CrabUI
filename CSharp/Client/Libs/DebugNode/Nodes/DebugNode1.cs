using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1> : DebugNodeBase
  {
    public Func<T1, string> MsgFactory { get; }

    private DebugEvent EventFactory(T1 arg1)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1 },
        Msg = MsgFactory.Invoke(arg1),
      };
    }

    public void Send(T1 arg1)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1));
      }
    }

    public DebugNode(string type, DebugHub hub, Func<T1, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}