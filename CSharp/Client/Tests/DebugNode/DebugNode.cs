using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using BaroJunk;
using CrabUI;

namespace CUITest
{
  public class DebugNodeTest : UTestPack
  {
    public DebugNode Node { get; } = new()
    {
      Factory = (a, b) => new DebugEvent()
      {
        Msg = $"{a} {b}",
      },
    };


    public override void CreateTests()
    {
      EventSubscription sub = Node.Pin.Add((e) => CUI.Logger.Log(e));


      Node.Send(123, "kek");
      sub.Cancel();
      Node.Send(123, "kek");
    }
  }
}