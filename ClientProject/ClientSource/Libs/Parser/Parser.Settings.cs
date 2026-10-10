using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using System.IO;
using System.Text;


namespace CUILibs
{
  public partial class Parser
  {
    public class Settings_Part
    {
      public class WhenSerializing_Part
      {
        public string OpeningQuote { get; set; } = "`";
        public string ClosingQuote { get; set; } = "`";
        public string EnumerableStart { get; set; } = "[";
        public string EnumerableEnd { get; set; } = "]";
        public string EnumerableSeparator { get; set; } = ",";
        public string DictSeparator { get; set; } = ",";
        public string DictStart { get; set; } = "{";
        public string DictEnd { get; set; } = "}";
        public string DictKVSeparator { get; set; } = ":";
      }

      public class WhenParsing_Part
      {

        // public char[] Quotes { get; set; } = ['\'', '\"', '`']; // Hardcoded
        public char EnumerableStart { get; set; } = '[';
        public char EnumerableEnd { get; set; } = ']';
        public char EnumerableSeparator { get; set; } = ',';
        public char DictSeparator { get; set; } = ',';
        public char DictStart { get; set; } = '{';
        public char DictEnd { get; set; } = '}';
        public char DictKVSeparator { get; set; } = ':';
      }

      public string NullTerm { get; set; } = "_null_";

      public WhenSerializing_Part WhenSerializing { get; } = new();
      public WhenParsing_Part WhenParsing { get; } = new();

      public Dictionary<Type, Func<string, object?>> ExtraParseMethods { get; set; } = [];
      public Dictionary<Type, Func<object, string>> ExtraSerializeMethods { get; set; } = [];
    }
  }
}
