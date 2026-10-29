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
      parser.Settings.WhenSerializing.ListStart = "[";
      parser.Settings.WhenSerializing.ListEnd = "]";
      parser.Settings.WhenSerializing.ListSeparator = ",";
      parser.Settings.WhenSerializing.DictSeparator = ",";
      parser.Settings.WhenSerializing.DictStart = "{";
      parser.Settings.WhenSerializing.DictEnd = "}";
      parser.Settings.WhenSerializing.DictKVSeparator = ":";

      parser.Settings.WhenParsing.ListStart = '[';
      parser.Settings.WhenParsing.ListEnd = ']';
      parser.Settings.WhenParsing.ListSeparator = ',';
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