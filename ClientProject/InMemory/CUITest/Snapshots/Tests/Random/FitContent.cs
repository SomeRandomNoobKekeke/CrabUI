using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent FitContent()
      {
        CUIFrame frame = new()
        {
          Background = { Color = new Color(32, 32, 32) },
          Absolute = new CUINullRect(0, 0, 400, 400),
          Anchor = CUIAnchor.Center,
          FitContent = new CUIBool2(true, true),
        };

        frame["a"] = new CUIComponent()
        {
          Background = { Color = Color.Yellow },
          Absolute = new CUINullRect(20, 20, 30, 30),
        };

        frame["b"] = new CUIComponent()
        {
          Background = { Color = Color.Red },
          Absolute = new CUINullRect(300, 300, 30, 30),
        };


        return frame;
      }
    }
  }
}