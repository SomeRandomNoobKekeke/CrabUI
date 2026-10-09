using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;


namespace CUILibs
{
  /// <summary>
  /// It just checks that these methods exist
  /// </summary>
  public class ParserInterfaceTest : ParserTest
  {
    public List<UTest> Usage()
    {
      Parser parser = CreateParser();

      return new List<UTest>()
      {
        new UTest(parser.Serialize(123), "123"),
        new UTest(parser.SerializeEnumerable(new int[] { 123, 321 }) is string),
        new UTest(parser.SerializeDictionary(new Dictionary<string, object>()
        {
          ["a"] = 123,
          ["b"] = 321,
        }) is string),
        new UTest(parser.Parse<int>("123"), 123),
        new UTest(parser.Parse("123", typeof(int)), 123)
      };
    }

    public UTest Settings()
    {
      Parser parser = CreateParser();

      parser.Settings.NullTerm = "123";
      parser.Settings.EnumerableStart = 'q';
      parser.Settings.EnumerableEnd = 'q';
      parser.Settings.Quote = 'q';
      parser.Settings.EnumerableSeparator = 'q';
      parser.Settings.DictSeparator = 'q';
      parser.Settings.DictStart = 'q';
      parser.Settings.DictEnd = 'q';
      parser.Settings.DictKVSeparator = 'q';
      parser.Settings.ExtraParseMethods = new Dictionary<Type, Func<string, object?>>();
      parser.Settings.ExtraSerializeMethods = new Dictionary<Type, Func<object, string>>();

      return new UTest(true);
    }
  }
}