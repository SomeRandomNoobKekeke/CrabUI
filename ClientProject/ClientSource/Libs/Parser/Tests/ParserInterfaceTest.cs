using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;


namespace CUILibs
{
  public partial class ParserTest : UTestPack
  {
    /// <summary>
    /// It just checks that these methods exist
    /// </summary>
    public class ParserInterfaceTest : ParserTest
    {
      public List<UTest> Usage()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(123), "123"),
          new UTest(parser.SerializeEnumerable(new int[] { 123, 321 }) is string),
          new UTest(parser.SerializeDictionary(new Dictionary<string, object>()
          {
            ["a"] = 123,
            ["b"] = 321,
          }) is string),
          new UTest(parser.Parse<int>("123"), 123),
          new UTest(parser.Parse("123", typeof(int)), 123),
          new UTest(parser.ParseEnumerable<int>("[]") is IEnumerable<int>),
          new UTest(parser.SplitList("[]") is List<string>),
          new UTest(parser.SplitDict("[]") is Dictionary<string,string>),
        };
      }
    }
  }
}