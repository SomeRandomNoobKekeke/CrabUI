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
    public class ParsingEnumsTest : ParserTest
    {
      public enum Direction { Up, Right, Down, Left }

      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(Direction.Right), "Right"),
          new UTest(parser.Serialize(((Direction)2)), "Down"),
          new UTest(parser.Serialize(BindingFlags.Public | BindingFlags.Instance), "Instance, Public"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<Direction>("Right"), Direction.Right),
          new UTest(parser.Parse<BindingFlags>("Instance, Public"), BindingFlags.Public | BindingFlags.Instance),
        };
      }

      public List<UTest> ParseInvalid()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<Direction>("Diagonal"), Direction.Up),
          new UTest(parser.Parse<Direction>("123123"), (Direction)123123),
          new UTest(parser.Parse<Direction>("2"), Direction.Down),
          new UTest(parser.Parse<Direction>(""), Direction.Up),
          new UTest(parser.Parse<Direction>(null!), Direction.Up),
        };
      }
    }
  }

}