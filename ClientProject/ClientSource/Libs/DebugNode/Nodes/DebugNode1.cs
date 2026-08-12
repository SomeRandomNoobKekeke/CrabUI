using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1> : DebugNodeBase
  {
    public static string DefaultMsgFactory(T1 arg1) => $"{arg1}";
    public Func<T1, string> MsgFactory { get; set; } = DefaultMsgFactory;

    private DebugEvent EventFactory(T1 arg1)
    {
      return new DebugEvent()
      {
        Type = Type,
        Args = new object[] { arg1 },
        Msg = MsgFactory.Invoke(arg1),
      };
    }

    public override void Send(object arg1) => Send((T1)arg1);
    public void Send(T1 arg1)
    {
      // Logger.Default.LogVars(Type, GlobalGate.IsOpen, IsOpen);
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(EventFactory(arg1));
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub) { }
    public DebugNode(string type, DebugHub hub, Func<T1, string> msgFactory) : base(type, hub)
    {
      MsgFactory = msgFactory;
    }
  }
}