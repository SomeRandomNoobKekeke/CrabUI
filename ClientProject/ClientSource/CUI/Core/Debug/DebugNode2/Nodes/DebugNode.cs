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


    public void Send()
    {
      if (GlobalGate.IsOpen && IsOpen)
      {
        Hub.Send(new Debug2Event()
        {
          Type = Type,
          Args = [],
          Msg = MsgFactory.Invoke(),
        });
      }
    }

    public Debug2Node(string type, Debug2Hub hub) : base(type, hub)
    {
      MsgFactory = DefaultMsgFactory;
    }
  }
}