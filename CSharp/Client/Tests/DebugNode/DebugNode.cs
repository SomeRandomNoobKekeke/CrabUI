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
    public UTest PinTest()
    {
      DebugNode<int, string> Node = new()
      {
        Name = "Node",
        Factory = (a, b) => new DebugEvent() { Msg = $"{a} {b}" }
      };

      string msg = null;

      EventSubscription sub = Node.Pin.Add((e) => msg = e.ToString());

      Node.Send(123, "kek");
      return new UTest(msg, "123 kek");
    }

    public UTest RouteTest()
    {
      DebugNode<int, string> Node1 = new()
      {
        Name = "Node1",
      };
      DebugNode<int, string> Node2 = new()
      {
        Name = "Node2",
        Factory = (a, b) => new DebugEvent() { Msg = $"{a} {b}" }
      };

      string msg = null;

      Node2.Pin.Add((e) => msg = e.ToString());

      Node2.Route(Node1);
      Node1.Send(123, "kek");

      return new UTest(msg, "123 kek");
    }

    public UTest UnrouteTest()
    {
      DebugNode<int, string> Node1 = new()
      {
        Name = "Node1",
      };
      DebugNode<int, string> Node2 = new()
      {
        Name = "Node2",
        Factory = (a, b) => new DebugEvent() { Msg = $"{a} {b}" }
      };

      string msg = null;

      Node2.Pin.Add((e) => msg = e.ToString());

      Node2.Route(Node1);
      Node1.Send(123, "kek");
      Node2.Unroute(Node1);
      Node1.Send(321, "lol");

      return new UTest(msg, "123 kek");
    }
  }
}