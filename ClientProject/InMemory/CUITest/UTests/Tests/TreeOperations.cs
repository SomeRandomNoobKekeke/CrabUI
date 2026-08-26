using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;

namespace CursedUI
{
  public class TreeOperationsTest : UTestPack
  {

    public List<UTest> Juggling()
    {
      List<UTest> tests = new();

      CUIComponent a = new CUIComponent() { AKA = "a" };
      CUIComponent b = new CUIComponent() { AKA = "b" };
      CUIComponent c = new CUIComponent() { AKA = "c" };

      b.Children.Add(a);
      tests.Add(new UTest(b.Children.Contains(a)));
      tests.Add(new UTest(a.Parent, b));

      c.Children.Add(a);
      tests.Add(new UTest(!b.Children.Contains(a)));
      tests.Add(new UTest(c.Children.Contains(a)));
      tests.Add(new UTest(a.Parent, c));

      return tests;
    }

    public List<UTest> JugglingWithIndexer()
    {
      List<UTest> tests = new();

      CUIComponent a = new CUIComponent() { AKA = "a" };
      CUIComponent b = new CUIComponent() { AKA = "b" };
      CUIComponent c = new CUIComponent() { AKA = "c" };

      b["a in b"] = a;
      tests.Add(new UTest(b.Children.Contains(a)));
      tests.Add(new UTest(a.Parent, b));
      tests.Add(new UTest(a.AKA, "a in b"));
      tests.Add(new UTest(b["a in b"], a));

      c["a in c"] = a;
      tests.Add(new UTest(!b.Children.Contains(a)));
      tests.Add(new UTest(c.Children.Contains(a)));
      tests.Add(new UTest(a.Parent, c));
      tests.Add(new UTest(a.AKA, "a in c"));
      tests.Add(new UTest(b["a in b"], null));
      tests.Add(new UTest(c["a in c"], a));

      return tests;
    }


  }
}