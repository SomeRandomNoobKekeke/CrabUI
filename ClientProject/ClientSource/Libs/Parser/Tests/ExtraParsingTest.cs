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
    public class ExtraParsingTest : ParserTest
    {
      public class SomeParsableClass(int value) : IParsable
      {
        public int Value { get; set; } = value;

        public static object Parse(string raw)
        {
          return new SomeParsableClass(int.Parse(raw));
        }

        public string ToText() => $"Funny int: {Value}";
      }

      public List<UTest> Serialize()
      {
        List<UTest> tests = [];

        Parser parser = CreateTestParser();

        tests.Add(new UTest(parser.Serialize(123), "123"));
        parser.Settings.ExtraSerializeMethods[typeof(int)] = (i) => $"int: {i}";
        tests.Add(new UTest(parser.Serialize(123), "int: 123"));

        string CustomToText(SomeParsableClass v) => $"normal int: {v.Value}";

        tests.Add(new UTest(parser.Serialize(new SomeParsableClass(123)), "Funny int: 123"));
        parser.Settings.ExtraSerializeMethods[typeof(SomeParsableClass)] = (o) => (CustomToText(o as SomeParsableClass));
        tests.Add(new UTest(parser.Serialize(new SomeParsableClass(123)), "normal int: 123"));

        return tests;
      }

      public List<UTest> Parse()
      {
        List<UTest> tests = [];
        Parser parser = CreateTestParser();

        tests.Add(new UTest(parser.Parse<int>("123"), 123));
        parser.Settings.ExtraParseMethods[typeof(int)] = (raw) => int.Parse(raw) + 1;
        tests.Add(new UTest(parser.Parse<int>("123"), 124));

        tests.Add(new UTest(parser.Parse<SomeParsableClass>("123").Value == 123));
        parser.Settings.ExtraParseMethods[typeof(SomeParsableClass)] = (raw) => new SomeParsableClass(int.Parse(raw) + 1);
        tests.Add(new UTest(parser.Parse<SomeParsableClass>("123").Value == 124));

        return tests;
      }


    }
  }

}