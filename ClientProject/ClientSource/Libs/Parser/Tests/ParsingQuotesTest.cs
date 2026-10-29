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
    public class ParsingQuotesTest : ParserTest
    {


      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(new int[]{123, 321}), "[123,321]"),
          new UTest(parser.Serialize(new string[]{"123", "321"}), "[`123`,`321`]"),
          new UTest(parser.Serialize(new Dictionary<string, int>
          {
            ["qwe"] = 123
          }), "{qwe:123}"),
          new UTest(parser.Serialize(new Dictionary<string, string>
          {
            ["qwe"] = "123"
          }), "{qwe:`123`}"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UDictTest(parser.SplitDict("{qwe:123}"), new Dictionary<string, string>
          {
            ["qwe"] = "123"
          }),
          new UDictTest(parser.SplitDict("{`qwe`:`123`}"), new Dictionary<string, string>
          {
            ["qwe"] = "123"
          }),
          new UDictTest(parser.SplitDict("{'qwe':'123'}"), new Dictionary<string, string>
          {
            ["qwe"] = "123"
          }),
          new UDictTest(parser.SplitDict("{`qwe,:[]{}''asd`:`123,:[]{}''456`}"), new Dictionary<string, string>
          {
            ["qwe,:[]{}''asd"] = "123,:[]{}''456"
          }),
          new UListTest(parser.SplitList("[`qwe,:[]{}''asd`,`123,:[]{}''456`]"), new string[]
          {
            "qwe,:[]{}''asd", "123,:[]{}''456"
          }),
        };
      }
    }
  }

}