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
  public class DebugChannelTest : UTestPack
  {
    public DebugGate<string> Gate1 = new();
    public DebugRouter<string> Router1 = new();
    public DebugRouter<string> Router2 = new();

    public override void CreateTests()
    {
      CUI.Logger.Log(123);


      Gate1.Map(s => CUI.Logger.Log("Gate1"));
      Router1.Map(s => CUI.Logger.Log("Router1"));
      Router2.Map(s => CUI.Logger.Log("Router2"));


      Router1.Route(Gate1);
      Router2.Route(Router1);

      Gate1.Send("hi");
    }
  }
}