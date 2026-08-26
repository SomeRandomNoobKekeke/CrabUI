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
      public static CUIComponent LayoutProps()
      {

        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Resizable = true,
        };

        frame["a"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 100, null, null),
          Relative = new CUINullRect(null, null, 0.2f, null),
          CrossRelative = new CUINullRect(null, null, null, 0.2f),
          AbsoluteMin = new CUINullRect(null, null, 30, 30),
          Background = { Color = Color.Yellow },
        };

        return frame;
      }
    }
  }
}