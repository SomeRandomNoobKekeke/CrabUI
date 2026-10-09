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
      public string EnumerableStart { get; set; } = "[";
      public string EnumerableEnd { get; set; } = "]";
      public string Quote { get; set; } = "\'";
      public string EnumerableSeparator { get; set; } = ",";
      public string DictSeparator { get; set; } = ",";
      public string DictStart { get; set; } = "{";
      public string DictEnd { get; set; } = "}";
      public string DictKVSeparator { get; set; } = ":";

      public Dictionary<Type, Func<string, object?>> ExtraParseMethods { get; set; } = [];
      public Dictionary<Type, Func<object, string>> ExtraSerializeMethods { get; set; } = [];
    }

    public Settings_Part Settings { get; set; } = new();
  }
}
