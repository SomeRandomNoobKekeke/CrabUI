using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  public class DebugNode<T1, T2, T3, T4> : DebugNodeBase
  {
    public string DefaultMsgFactory(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => $"{Type}: {arg1}, {arg2}, {arg3}, {arg4}";
    public Func<T1, T2, T3, T4, string> MsgFactory { get; set; }

    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Output.Raise(new DebugEvent()
        {
          Type = Type,
          Args = [arg1, arg2, arg3, arg4],
          Msg = MsgFactory.Invoke(arg1, arg2, arg3, arg4),
        });
      }
    }

    public DebugNode(string type, DebugHub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
  }
}