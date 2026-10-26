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
    public static partial class Dynamic
    {
      //LINK:\ClientProject\ClientSource\CUI\Core\Components\CUIFastTextDump.cs
      public static CUIComponent CUIFastTextDump()
      {
        DynamicContainer container = new DynamicContainer() { FPS = 60 };

        CUIComponent frame = container["frame"] = new CUIDefault.Frame("CUIFastTextDump", 400, 600);

        CUIFastTextDump dump = new CUIFastTextDump()
        {
          Background = { Color = Color.Gray },
          Offset = new Vector2(10, 10),
          Shift = new Vector2(0, 20),
          Solid = true,
        };

        frame["layout"]["scroll wrapper"] = new CUIDefault.ListWithScrollbar() { Flex = 1 };
        frame["layout"]["scroll wrapper"]["list"]["dump"] = dump;

        container.Updated += () =>
        {
          dump.Clear();

          dump.Lines = Enumerable.Range(0, 100 + CUI.Random.Next(0, 50))
            .Select(i => new FastTextDump.Line(CUI.Random.RandomString(20), CUI.Random.Color()))
            .ToList();
        };

        return container;
      }
    }
  }
}