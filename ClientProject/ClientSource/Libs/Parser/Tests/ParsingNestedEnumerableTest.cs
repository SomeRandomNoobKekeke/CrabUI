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
    public class ParsingNestedEnumerableTest : ParserTest
    {


      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(new int[][]{
            new int[]{1,2,3},
            new int[]{4,5,6 }
          }), "[`[1,2,3]`,`[4,5,6]`]"),

          new UTest(parser.Serialize(new List<Dictionary<string,int>>{
            new Dictionary<string, int>()
            {
              ["q"] = 1,
              ["w"] = 2,
            },
            new Dictionary<string, int>()
            {
              ["a"] = 3,
              ["s"] = 4,
            },
          }), "[`{q:1,w:2}`,`{a:3,s:4}`]"),

          new UTest(parser.Serialize(new Dictionary<string,int[]>{
            ["qwe"] = new int[]{1,2,3},
            ["asd"] = new int[]{3,2,1},
          }), "{qwe:`[1,2,3]`,asd:`[3,2,1]`}"),

          new UTest(parser.Serialize(new Dictionary<string,Dictionary<string,int>>{
            ["qwe"] = new Dictionary<string, int>()
            {
              ["q"] = 1,
              ["w"] = 2,
              ["e"] = 3,
            },
            ["asd"] = new Dictionary<string, int>()
            {
              ["a"] = 4,
              ["s"] = 5,
              ["d"] = 6,
            },
          }), "{qwe:`{q:1,w:2,e:3}`,asd:`{a:4,s:5,d:6}`}"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UListTest(parser.SplitEnumerable("[`[1,2,3]`,`[4,5,6]`]"), new string[]{"[1,2,3]","[4,5,6]"}),
          new UListTest(parser.SplitEnumerable("[`{q:1,w:2}`,`{a:3,s:4}`]"), new string[]{ "{q:1,w:2}", "{a:3,s:4}"}),
          new UDictTest(parser.SplitDict("{qwe:`[1,2,3]`,asd:`[4,5,6]`}"), new Dictionary<string, string>
          {
            ["qwe"] = "[1,2,3]",
            ["asd"] = "[4,5,6]",
          }),
          new UDictTest(parser.SplitDict("{qwe:`{q:1,w:2,e:3}`,asd:`{a:4,s:5,d:6}`}"), new Dictionary<string, string>
          {
            ["qwe"] = "{q:1,w:2,e:3}",
            ["asd"] = "{a:4,s:5,d:6}",
          }),
        };
      }
    }
  }

}