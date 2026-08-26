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
    public static partial class Random
    {
      public static CUIComponent NestedScissorRects()
      {
        CUIFrame frame = new()
        {
          Background = { Color = Color.Gray },
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 400, 600),
          Resizable = true,
        };

        frame["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
        };

        frame["layout"]["a"] = new CUIVerticalList()
        {
          Background = { Color = Color.Blue },
          Flex = 1,
        };

        frame["layout"]["b"] = new CUIVerticalList()
        {
          Background = { Color = Color.Red },
          Flex = 1,
        };

        frame.PrintVisualSplit();

        return frame;
      }
    }
  }
}