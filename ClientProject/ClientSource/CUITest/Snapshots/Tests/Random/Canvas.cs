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
      public static CUIComponent Canvas()
      {
        CUIFrame frame = new()
        {
          Background = { Color = new Color(32, 32, 32) },
          Absolute = new CUINullRect(0, 0, 400, 400),
          Anchor = CUIAnchor.Center,
        };

        CUICanvas canvas = new CUICanvas(2, 2)
        {
          Relative = new(0, 0, 1, 1),
          Data = new Color[]{
            Color.Red, Color.Yellow, Color.Green, Color.Blue
          },
        };

        frame["canvas"] = canvas;

        return frame;
      }
    }
  }
}