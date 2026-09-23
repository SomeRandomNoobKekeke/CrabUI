using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class Debug2Node : Debug2NodeBase
  {
    public string DefaultMsgFactory() => $"{Type}";
    public Func<string> MsgFactory { get; set; }


    private Debug2Event EventFactory()
    {
      return new Debug2Event()
      {
        Type = Type,
        Args = new object[] { },
        Msg = MsgFactory.Invoke(),
      };
    }

    public override void Send()
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory());
      }
    }

    public Debug2Node(string type, Debug2Hub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
    public Debug2Node(string type, Debug2Hub hub, Func<string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}