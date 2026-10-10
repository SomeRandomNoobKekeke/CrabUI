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
    public static Parser CreateTestParser()
    {
      Parser parser = new();

      parser.Settings.NullTerm = "_null_";

      parser.Settings.WhenSerializing.OpeningQuote = "`";
      parser.Settings.WhenSerializing.ClosingQuote = "`";
      parser.Settings.WhenSerializing.EnumerableStart = "[";
      parser.Settings.WhenSerializing.EnumerableEnd = "]";
      parser.Settings.WhenSerializing.EnumerableSeparator = ",";
      parser.Settings.WhenSerializing.DictSeparator = ",";
      parser.Settings.WhenSerializing.DictStart = "{";
      parser.Settings.WhenSerializing.DictEnd = "}";
      parser.Settings.WhenSerializing.DictKVSeparator = ":";

      parser.Settings.WhenParsing.EnumerableStart = '[';
      parser.Settings.WhenParsing.EnumerableEnd = ']';
      parser.Settings.WhenParsing.EnumerableSeparator = ',';
      parser.Settings.WhenParsing.DictSeparator = ',';
      parser.Settings.WhenParsing.DictStart = '{';
      parser.Settings.WhenParsing.DictEnd = '}';
      parser.Settings.WhenParsing.DictKVSeparator = ':';

      parser.Settings.ExtraParseMethods = new Dictionary<Type, Func<string, object?>>();
      parser.Settings.ExtraSerializeMethods = new Dictionary<Type, Func<object, string>>();

      return parser;
    }
  }
}