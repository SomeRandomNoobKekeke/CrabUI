using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace CUILibs
{
  public class Debug2NodeConceptTest : Debug2NodeTest
  {

    public override void CreateTests()
    {
      string result = "???";

      Debug2Hub hub = new();

      hub.Gates["cringe"].Open();
      hub.Output += (e) => result = e.ToString();

      Debug2Node<string, int> node1 = new("cringe", hub)
      {
        MsgFactory = (s, i) => $"bruh {s} {i}"
      };
      node1.Send("kek", 123);

      Tests.Add(new UTest(result, $"cringe| bruh kek 123"));
    }
  }
}