using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;

namespace CrabUI
{
  public class CUIParserTest : UTestPack
  {

    public List<UTest> Random()
    {
      List<UTest> tests = new();

      CUIParser parser = new();

      tests.Add(new UTest(parser.Parse("123", typeof(int)), 123));
      tests.Add(new UTest(parser.Parse("255,0,255", typeof(Color)), Color.Magenta));
      tests.Add(new UTest(parser.Parse("123", typeof(float)), 123.0f));
      tests.Add(new UTest(parser.Parse("123", typeof(float?)), 123.0f));
      tests.Add(new UTest(parser.Parse("{{null}}", typeof(float?)), null));

      return tests;
    }

    public List<UTest> Tuple()
    {
      List<UTest> tests = new();

      CUIParser parser = new();

      Tests.Add(new UTest(parser.Parse("[2,2]", typeof((int, int))), (2, 2)));
      Tests.Add(new UTest(parser.Parse("  [  2  ,   2   ]   ", typeof((int, int))), (2, 2)));
      Tests.Add(new UTest(parser.Parse("[2,]", typeof((int, int))), (2, 0)));
      Tests.Add(new UTest(parser.Parse("[,]", typeof((int, int))), (0, 0)));
      Tests.Add(new UTest(parser.Parse("[,2]", typeof((int, int))), (0, 2)));
      Tests.Add(new UTest(parser.Parse("(2,5]", typeof((int, int))), (0, 5)));
      Tests.Add(new UTest(parser.Parse("qweqwerqwer", typeof((int, int))), (0, 0)));
      Tests.Add(new UTest(parser.Parse("qweqw,3", typeof((int, int))), (0, 3)));

      return tests;
    }

    public List<UTest> Arrays()
    {
      List<UTest> tests = new();

      CUIParser parser = new();

      Tests.Add(new UTest(parser.SerializeArray(2, 2), "[2,2]"));
      Tests.Add(new UTest(parser.SerializeArray(2, (3, 3)), "[2,[3,3]]"));

      Tests.Add(new UListTest(parser.ParseArray<int>("[1,2,3]"), new int[] { 1, 2, 3 }));
      Tests.Add(new UListTest(parser.ParseArray<(int, int)>("[[2,2],[3,3]]"), new (int, int)[] { (2, 2), (3, 3) }));

      return tests;
    }


  }
}