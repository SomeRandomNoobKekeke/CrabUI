using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class ClearableEventConceptTest : ClearableEventTest
  {
    public ClearableEvent<string> bruh = new();
    public ClearableEvent<string> kek = new();

    public UTest ConceptTest()
    {
      string value = "???";

      bruh.Map(kek);

      kek.Add((s) => value = s);
      bruh.Raise("lol");

      return new UTest(value, "lol");
    }
  }


}