using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1, T2> : DebugNodeBase
  {
    public string DefaultMsgFactory(T1 arg1, T2 arg2) => $"{Type}: {arg1}, {arg2}";
    public Func<T1, T2, string> MsgFactory { get; set; }


    public void Send(T1 arg1, T2 arg2)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new DebugEvent()
        {
          Type = Type,
          Args = [arg1, arg2],
          Msg = MsgFactory.Invoke(arg1, arg2),
        });
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
  }
}