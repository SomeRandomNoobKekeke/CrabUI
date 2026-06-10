using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public class DebugNodeConceptTest : DebugNodeTest
  {

    public override void CreateTests()
    {
      string result = "???";

      DebugHub hub = new();

      hub.Gates["cringe"].Open();
      hub.Output.Add((e) => result = e.ToString());

      DebugNode<string, int> node1 = new("cringe", hub, (s, i) => $"bruh {s} {i}");
      node1.Send("kek", 123);

      Tests.Add(new UTest(result, $"cringe| bruh kek 123"));
    }
  }
}