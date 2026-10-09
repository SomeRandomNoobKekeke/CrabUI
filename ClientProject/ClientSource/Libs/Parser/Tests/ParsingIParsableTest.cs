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
    public class ParsingIParsableTest : ParserTest
    {
      public record FunnyVector2(float X = 0, float Y = 0) : IParsable
      {
        public static object Parse(string raw)
        {
          string[] parts = raw.Split(',');

          if (raw == "6, 7") throw new ExecutionEngineException("System32 removed");
          try
          {
            return new FunnyVector2(int.Parse(parts[0]), int.Parse(parts[1]));
          }
          catch (Exception)
          {
            return new FunnyVector2();
          }
        }

        public override string ToString() => $"FunnyVector2 [{X}, {Y}]";

        public string ToText()
        {
          if (X == 6 && Y == 7) throw new ExecutionEngineException("System32 removed");
          return $"{X}, {Y}";
        }
      }

      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(new FunnyVector2(2,3)), "2, 3"),
          new UTest(parser.Serialize(new FunnyVector2(6,7)), "FunnyVector2 [6, 7]"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<FunnyVector2>("2, 3"), new FunnyVector2(2,3)),
        };
      }

      public List<UTest> ParseInvalid()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<FunnyVector2>("123"), new FunnyVector2()),
          new UTest(parser.Parse<FunnyVector2>("6, 7"), null),
        };
      }
    }
  }

}