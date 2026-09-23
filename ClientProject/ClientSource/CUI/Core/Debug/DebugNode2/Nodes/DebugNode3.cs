using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Node<T1, T2, T3> : Debug2NodeBase
  {
    public string DefaultMsgFactory(T1 arg1, T2 arg2, T3 arg3) => $"{Type}: {arg1}, {arg2}, {arg3}";
    public Func<T1, T2, T3, string> MsgFactory { get; set; }

    private Debug2Event EventFactory(T1 arg1, T2 arg2, T3 arg3)
    {
      return new Debug2Event()
      {
        Type = Type,
        Args = [arg1, arg2, arg3],
        Msg = MsgFactory.Invoke(arg1, arg2, arg3),
      };
    }

    public void Send(T1 arg1, T2 arg2, T3 arg3)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1, arg2, arg3));
      }
    }

    public Debug2Node(string type, Debug2Hub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
    public Debug2Node(string type, Debug2Hub hub, Func<T1, T2, T3, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}