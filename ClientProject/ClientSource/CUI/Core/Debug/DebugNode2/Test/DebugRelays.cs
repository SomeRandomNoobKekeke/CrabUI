using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace CUILibs
{
  public class Debug2RelayTest : Debug2NodeTest
  {

    public override void CreateTests()
    {
      string result = "???";

      Debug2Hub hub = new();

      hub.Gates["cringe"].Open();
      hub.Output.Add((e) => result = e.ToString());
      hub.IsOpen = false;

      Debug2Relay relay1 = new();
      Debug2Relay relay2 = new();

      relay1.Route(relay2);
      hub.Route(relay1);

      Debug2Node<string, int> node1 = new("cringe", hub, (s, i) => $"bruh {s} {i}");
      Debug2Node<string, int> node2 = new("cringe", hub, (s, i) => $"bruh {s} {i}");
      Debug2Node<string, int> node3 = new("bruh", hub, (s, i) => $"bruh {s} {i}");

      node1.Map(relay2);
      relay2.Route(node2);
      node3.Map(relay2);

      Tests.Add(new UListTest(
        relay1.GetNodes("cringe"),
        new List<Debug2NodeBase>() { node1, node2 }
      ));

      Tests.Add(new UListTest(
        hub.GetNodes("bruh"),
        new List<Debug2NodeBase>() { node3 }
      ));


      node1.Send("kek", 123);
      Tests.Add(new UTest(result, $"???"));

      foreach (var node in relay1.GetNodes("cringe"))
      {
        node.Open();
      }
      node1.Send("kek", 123);
      Tests.Add(new UTest(result, $"cringe| bruh kek 123"));


      relay2.Unroute(node3);
      Tests.Add(new UListTest(
        hub.GetNodes("bruh"),
        new List<Debug2NodeBase>() { }
      ));
    }
  }
}