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
  public class CUIEventCancelationTest : ConceptTest
  {

    public override void CreateTests()
    {
      CUIEvent bebebe = new();

      string value = "ok";

      var subscription = bebebe.Add(() => value = "not ok");
      subscription.Cancel(); // Event subscription is canceled, even though i used lambda
      bebebe.Raise();

      Tests.Add(new UTest(value, "ok"));
    }
  }
}