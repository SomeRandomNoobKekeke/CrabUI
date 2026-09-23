using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Node<T1> : Debug2NodeBase
  {
    public string DefaultMsgFactory(T1 arg1) => $"{Type}: {arg1}";
    public Func<T1, string> MsgFactory { get; set; }

    private Debug2Event EventFactory(T1 arg1)
    {
      return new Debug2Event()
      {
        Type = Type,
        Args = [arg1],
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

    public Debug2Node(string type, Debug2Hub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
    public Debug2Node(string type, Debug2Hub hub, Func<T1, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}