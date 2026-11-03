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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Layout
    {
      public static CUIComponent ScrollWrapper()
      {
        CUIFrame frame = new CUIDefault.Frame("ScrollWrapper", 400, 600);

        frame["layout"]["scroll wrapper"] = new CUIDefault.ListWithScrollbar() { Flex = 1 };

        List<string> lines = Enumerable.Range(1, 1000).Select(i => $"text line {i}").ToList();

        frame["layout"]["scroll wrapper"]["list"]["dump"] = new CUIFastTextDump()
        {
          Lines = lines.Select(s => new FastTextDump.Line(s, Color.White)).ToList(),
          Solid = true,
        };

        return frame;
      }
    }
  }
}