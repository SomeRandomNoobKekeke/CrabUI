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
    private static Parser _Default; public static Parser Default
    {
      get
      {
        if (_Default is null)
        {
          _Default = new();
          _Default.Settings.ExtraParseMethods = DefaultExtraParseMethods;
          _Default.Settings.ExtraSerializeMethods = DefaultExtraSerializeMethods;
        }

        return _Default;
      }
    }


    public ILogger Logger { get; set; } = CUILibs.Logger.Default;
    public Settings_Part Settings { get; set; } = new();

    private EnumerableParser enumerableParser;
    private DictionaryParser dictionaryParser;

    public Parser()
    {
      enumerableParser = new(Settings.WhenParsing);
      dictionaryParser = new(Settings.WhenParsing);
    }
  }
}
