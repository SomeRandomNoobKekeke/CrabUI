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
    public class ParsingPrimitivesTest : ParserTest
    {
      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(null), "_null_"),
          new UTest(parser.Serialize(123), "123"),
          new UTest(parser.Serialize(int.MaxValue), $"{int.MaxValue}"),
          new UTest(parser.Serialize(float.PositiveInfinity), $"{float.PositiveInfinity}"),
          new UTest(parser.Serialize(float.NaN), $"{float.NaN}"),
          new UTest(parser.Serialize(123.1), "123.1"),
          new UTest(parser.Serialize(123.1f), "123.1"),
          new UTest(parser.Serialize(true), "True"),
          new UTest(parser.Serialize("true"), "true"),
          new UTest(parser.Serialize('t'), "t"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<object>("_null_"), null!),
          new UTest(parser.Parse<int>("123"), 123),
          new UTest(parser.Parse<int>($"{int.MaxValue}"), int.MaxValue),
          new UTest(parser.Parse<float>($"{float.PositiveInfinity}"), float.PositiveInfinity),
          new UTest(parser.Parse<float>($"{float.NaN}"), float.NaN),
          new UTest(parser.Parse<float>($"123.1"), 123.1f),
          new UTest(parser.Parse<double>($"123.1"), 123.1),
          new UTest(parser.Parse<bool>($"True"), true),
          new UTest(parser.Parse<string>($"True"), "True"),
          new UTest(parser.Parse<char>($"T"), 'T'),
        };
      }

      public List<UTest> ParseInvalid()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Parse<object>("123"), null!),
          new UTest(parser.Parse<int>("sdaf"), 0),
          new UTest(parser.Parse<int>(null!), 0),
          new UTest(parser.Parse<bool>("truth nuke"), false),
          new UTest(parser.Parse<string>(null!), null!),
          new UTest(parser.Parse<char>(""), default(char)),
          new UTest(parser.Parse<char>("qwer"), default(char)),
          new UTest(parser.Parse<char>(null!), default(char)),
        };
      }
    }
  }

}