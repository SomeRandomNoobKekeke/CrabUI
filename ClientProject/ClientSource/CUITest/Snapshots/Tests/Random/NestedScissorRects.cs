using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent NestedScissorRects()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = Color.Gray,
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
          BackgroundColor = Color.Blue,
          Flex = 1,
        };

        frame["layout"]["b"] = new CUIVerticalList()
        {
          BackgroundColor = Color.Red,
          Flex = 1,
        };

        frame.PrintVisualSplit();

        return frame;
      }
    }
  }
}