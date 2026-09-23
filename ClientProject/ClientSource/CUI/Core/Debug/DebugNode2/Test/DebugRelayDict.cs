using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace CUILibs
{
  public class Debug2RelayDictTest : Debug2NodeTest
  {

    public override void CreateTests()
    {
      Debug2Hub hub = new();

      Debug2RelayDict dict1 = new Debug2RelayDict()
      {
        ["cringe"] = new Debug2Relay(),
        ["bruh"] = new Debug2Relay(),
      };

      Debug2RelayDict dict2 = new Debug2RelayDict()
      {
        ["cringe"] = new Debug2Relay(),
        ["bruh"] = new Debug2Relay(),
        ["kek"] = new Debug2Relay(),
      };

      Debug2NodeDict nodes = new Debug2NodeDict()
      {
        ["bruh"] = new Debug2Node<string>("bruh", hub, (s) => $"123 {s}"),
      };

      nodes.Map(dict2);
      dict1.Route(dict2);
      hub.Route(dict1);

      Tests.Add(new UListTest(
        hub.GetNodes("bruh"),
        new List<Debug2NodeBase>() { nodes["bruh"] }
      ));


      dict1.Unroute(dict2);
      Tests.Add(new UListTest(
        hub.GetNodes("bruh"),
        new List<Debug2NodeBase>() { }
      ));
    }
  }
}