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

    public void Send(T1 arg1)
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Send(new Debug2Event()
        {
          Type = Type,
          Args = [arg1],
          Msg = MsgFactory.Invoke(arg1),
        });
      }
    }

    public Debug2Node(string type, Debug2Hub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
  }
}