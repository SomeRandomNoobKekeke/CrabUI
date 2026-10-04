using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1> : DebugNodeBase
  {
    public string DefaultMsgFactory(T1 arg1) => $"{Type}: {arg1}";
    public Func<T1, string> MsgFactory { get; set; }

    public void Send(T1 arg1)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new DebugEvent()
        {
          Type = Type,
          Args = [arg1],
          Msg = MsgFactory.Invoke(arg1),
        });
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
  }
}