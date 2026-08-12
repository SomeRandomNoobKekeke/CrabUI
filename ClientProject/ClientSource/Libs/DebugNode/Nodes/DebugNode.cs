using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode : DebugNodeBase
  {
    public static string DefaultMsgFactory() => "bruh";
    public Func<string> MsgFactory { get; set; } = DefaultMsgFactory;


    private DebugEvent EventFactory()
    {
      return new DebugEvent()
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

    public DebugNode(string type, DebugHub hub) : base(type, hub) { }
    public DebugNode(string type, DebugHub hub, Func<string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}