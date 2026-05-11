using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class DebugDictConceptTest : DebugNodeTest
  {
    public DebugNodeDict Nodes1 { get; } = new()
    {
      ["bruh"] = new DebugNode<string>(),
    };

    public DebugNodeDict Nodes2 { get; } = new()
    {
      ["bruh"] = new DebugNode<string>()
      {
        MsgFactory = (s) => s,
      },
    };

    public UTest ConceptTest()
    {
      string value = "???";

      Nodes1.Map(Nodes2);

      // DebugNode<string> node = Nodes1.Get<DebugNode<string>>("bruh");
      // node.Map(Nodes2["bruh"]);

      Nodes2["bruh"].Pin.Add((e) => { value = e.ToString(); });
      Nodes1["bruh"].Send("lel");



      return new UTest(value, "| lel");
    }
  }
}