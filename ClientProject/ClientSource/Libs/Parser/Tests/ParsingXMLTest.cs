using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;
using System.Xml.Linq;

namespace CUILibs
{
  public partial class ParserTest : UTestPack
  {
    public class ParsingXMLTest : ParserTest
    {


      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        XDocument doc = new XDocument();
        doc.Add(new XElement("root"));
        doc.Root.Add(new XAttribute("value", parser.Serialize(new Dictionary<string, Dictionary<string, int>>()
        {
          ["qwe"] = new Dictionary<string, int>()
          {
            ["q"] = 1,
            ["w"] = 2,
            ["e"] = 3,
          },
          ["asd"] = new Dictionary<string, int>()
          {
            ["a"] = 1,
            ["s"] = 2,
            ["d"] = 3,
          },
        })));

        return new List<UTest>()
        {
          new UTest(doc.ToString(), "<root value=\"{qwe:{q:1,w:2,e:3},asd:{a:1,s:2,d:3}}\" />"),
        };
      }
    }
  }

}