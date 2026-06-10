using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class ClearableEventDictConceptTest : ClearableEventTest
  {
    public ClearableEventDict dict1 { get; } = new()
    {
      ["bruh"] = new ClearableEvent<string>(),
    };
    public ClearableEventDict dict2 { get; } = new()
    {
      ["bruh"] = new ClearableEvent<string>(),
    };



    public UTest ConceptTest()
    {
      string value = "???";

      dict1.Map(dict2);

      dict2["bruh"].Add((string s) => { value = s; });

      dict1["bruh"].Raise("bububu");


      return new UTest(value, "bububu");
    }
  }


}