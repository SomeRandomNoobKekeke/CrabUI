using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public class DebugRelayDictTest : DebugNodeTest
  {

    public override void CreateTests()
    {
      DebugHub hub = new();

      DebugRelayDict dict1 = new DebugRelayDict()
      {
        ["cringe"] = new DebugRelay(),
        ["bruh"] = new DebugRelay(),
      };

      DebugRelayDict dict2 = new DebugRelayDict()
      {
        ["cringe"] = new DebugRelay(),
        ["bruh"] = new DebugRelay(),
        ["kek"] = new DebugRelay(),
      };

      DebugNodeDict nodes = new DebugNodeDict()
      {
        ["bruh"] = new DebugNode<string>("bruh", hub, (s) => $"123 {s}"),
      };

      nodes.Map(dict2);
      dict1.Route(dict2);
      hub.Route(dict1);

      Tests.Add(new UListTest(
        hub.GetNodes("bruh"),
        new List<DebugNodeBase>() { nodes["bruh"] }
      ));
    }
  }
}