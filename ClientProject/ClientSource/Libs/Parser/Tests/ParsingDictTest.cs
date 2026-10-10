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
    public class ParsingDictTest : ParserTest
    {


      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(new Dictionary<string, int>()
          {
            ["qwe"] = 123,
            ["asd"] = 321,
          }), "{qwe:123,asd:321}"),
          new UTest(parser.Serialize(new Dictionary<string, string>()
          {
            ["qwe"] = "123",
            ["asd"] = "321",
          }), "{qwe:`123`,asd:`321`}"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UDictTest(parser.SplitDict("{qwe:123,asd:321}"), new Dictionary<string, string>
          {
            ["qwe"] = "123",
            ["asd"] = "321",
          }),

          new UDictTest(parser.SplitDict("{qwe:'123, asd','asd, fgh':321}"), new Dictionary<string, string>
          {
            ["qwe"] = "123, asd",
            ["asd, fgh"] = "321",
          }),
        };
      }

      public List<UTest> ParseInvalid()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UDictTest(parser.SplitDict("{'qwe:123':'asd:321'}"), new Dictionary<string, string>
          {
            ["qwe:123"] = "asd:321",
          }),
          new UDictTest(parser.SplitDict("{qwe':'321}"), new Dictionary<string, string>
          {

          }),
          new UDictTest(parser.SplitDict(null), new Dictionary<string, string>{}),
          new UDictTest(parser.SplitDict("{}"), new Dictionary<string, string>{}),
          new UDictTest(parser.SplitDict(""), new Dictionary<string, string>{}),
        };
      }
    }
  }

}