using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  //Test LINK:\ClientProject\InMemory\CUITest\Snapshots\Tests\Components\CUITextDump.cs
  public class CUITextDump : CUIVerticalList
  {
    private static ConditionalWeakTable<string, CUITextDump> Dumps { get; } = new();

    public static void DumpGlobal(string text, string target)
    {
      if (Dumps.TryGetValue(target, out CUITextDump dump))
      {
        dump.Dump(text);
      }
    }

    public static CUITextDump OpenNew(string name)
    {
      CUIFrame frame = new CUIDefault.Frame(name, 400, 600);

      CUITextDump dump = new CUITextDump(name) { Flex = 1 };
      frame["layout"]["dump"] = dump;
      frame.Open(CUI.TopMain);

      return dump;
    }

    public int MaxLines { get; set; } = 30;

    public void Dump(string text)
    {
      Children.Insert(0, new CUITextBlock(text)
      {
        TextAnchor = CUIAnchor.LeftCenter,
      });

      if (Children.Count > MaxLines) Children.RemoveAt(Children.Count - 1);
    }



    public CUITextDump(string name) : base()
    {
      Scrollable = true;
      Direction = CUIDirection.Reverse;
      Dumps.AddOrUpdate(name, this);
    }
  }
}