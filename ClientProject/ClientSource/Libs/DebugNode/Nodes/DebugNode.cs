using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode : DebugNodeBase
  {
    public string DefaultMsgFactory() => $"{Type}";
    public Func<string> MsgFactory { get; set; }


    public void Send()
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new DebugEvent()
        {
          Type = Type,
          Args = [],
          Msg = MsgFactory.Invoke(),
        });
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
  }
}