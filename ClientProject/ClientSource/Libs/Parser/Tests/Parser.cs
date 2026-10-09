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
      parser.Settings.EnumerableStart = "[";
      parser.Settings.EnumerableEnd = "]";
      parser.Settings.Quote = "\'";
      parser.Settings.EnumerableSeparator = ",";
      parser.Settings.DictSeparator = ",";
      parser.Settings.DictStart = "{";
      parser.Settings.DictEnd = "}";
      parser.Settings.DictKVSeparator = ":";
      parser.Settings.ExtraParseMethods = new Dictionary<Type, Func<string, object?>>();
      parser.Settings.ExtraSerializeMethods = new Dictionary<Type, Func<object, string>>();

      return parser;
    }
  }
}