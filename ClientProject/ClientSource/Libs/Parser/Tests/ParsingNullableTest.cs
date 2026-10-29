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
    public class ParsingNullableTest : ParserTest
    {
      public enum Direction { Up, Right, Down, Left }

      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        int? qwe = null;
        int? asd = 123;
        Direction? direction1 = null;
        Direction? direction2 = Direction.Right;

        char? char1 = null;
        char? char2 = 'q';

        string? str1 = null;
        string? str2 = "123 asda";

        return new List<UTest>()
        {
          new UTest(parser.Serialize(qwe), "_null_"),
          new UTest(parser.Serialize(asd), "123"),
          new UTest(parser.Serialize(direction1), "_null_"),
          new UTest(parser.Serialize(direction2), "Right"),
          new UTest(parser.Serialize(char1), "_null_"),
          new UTest(parser.Serialize(char2), "q"),
          new UTest(parser.Serialize(str1), "_null_"),
          new UTest(parser.Serialize(str2), "123 asda"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<int?>("_null_")!, null!),
          new UTest(parser.Parse<int?>("123")!, 123),
          new UTest(parser.Parse<Direction?>("_null_")!, null!),
          new UTest(parser.Parse<Direction?>("Right")!, Direction.Right),
          new UTest(parser.Parse<char?>("_null_")!, null!),
          new UTest(parser.Parse<char?>("q")!, 'q'),
          new UTest(parser.Parse<string?>("_null_")!, null!),
          new UTest(parser.Parse<string?>("123 asda")!, "123 asda"),
        };
      }

      public List<UTest> ParseInvalid()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<int?>("qwe")!, null!),
        };
      }
    }
  }

}