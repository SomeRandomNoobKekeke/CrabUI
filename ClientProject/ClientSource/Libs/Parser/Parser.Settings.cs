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
      public string NullTerm { get; set; } = "_null_";
      public char EnumerableStart { get; set; } = '[';
      public char EnumerableEnd { get; set; } = ']';
      public char Quote { get; set; } = '\'';
      public char EnumerableSeparator { get; set; } = ',';
      public char DictSeparator { get; set; } = ',';
      public char DictStart { get; set; } = '{';
      public char DictEnd { get; set; } = '{';
      public char DictKVSeparator { get; set; } = ':';

      public Dictionary<Type, Func<string, object?>> ExtraParseMethods { get; set; } = [];
      public Dictionary<Type, Func<object, string>> ExtraSerializeMethods { get; set; } = [];
    }

    public Settings_Part Settings { get; set; } = new();
  }
}
