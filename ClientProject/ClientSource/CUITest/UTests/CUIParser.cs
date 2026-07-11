using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class CUIParserTest : UTestPack
  {

    public override void CreateTests()
    {
      CUIParser parser = new();

      Tests.Add(new UTest(parser.Parse("123", typeof(int)), 123));
      Tests.Add(new UTest(parser.Parse("255,0,255", typeof(Color)), Color.Magenta));
      Tests.Add(new UTest(parser.Parse("123", typeof(float)), 123.0f));
      Tests.Add(new UTest(parser.Parse("123", typeof(float?)), 123.0f));
      Tests.Add(new UTest(parser.Parse("{{null}}", typeof(float?)), null));
    }

  }
}