using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Components
    {
      //Component LINK:\ClientProject\ClientSource\CUI\Core\Components\Tools\CUITextDump.cs
      public static CUIComponent CUITextDumpTest()
      {
        CUITextDump dump = CUITextDump.OpenNew("pomoyka");

        int a = 1;
        int b = 1;
        int c = 0;

        for (int i = 0; i < 20; i++)
        {
          c = a + b;
          (a, b) = (b, c);
          CUITextDump.DumpGlobal($"fib {i} = {c}", "pomoyka");
        }

        return new CUIComponent();
      }
    }
  }
}