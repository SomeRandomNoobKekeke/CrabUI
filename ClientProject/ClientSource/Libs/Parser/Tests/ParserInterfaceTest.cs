using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;


namespace CUILibs
{
  public class ParserInterfaceTest : ParserTest
  {
    /// <summary>
    /// It just checks that these methods exist
    /// </summary>
    public override void CreateTests()
    {
      Parser parser = DefaultParser;

      Tests.Add(new UTest(parser.Serialize(123), "123"));
      Tests.Add(new UTest(parser.SerializeEnumerable(new int[] { 123, 321 }), "[123,321]"));
      Tests.Add(new UTest(parser.SerializeDictionary(new Dictionary<string, object>()
      {
        ["a"] = 123,
        ["b"] = 321,
      }), "{a:123, b:321}"));
      Tests.Add(new UTest(parser.Parse<int>("123"), 123));
      Tests.Add(new UTest(parser.Parse("123", typeof(int)), 123));
    }
  }
}