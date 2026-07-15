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
      public static CUIComponent BoxSizing()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "BoxSizing" },
          Absolute = new CUINullRect(w: 400, h: 600),
        };

        frame["background"] = new CUIComponent()
        {
          Background = { Color = new Color(0, 255, 255, 255) },
          Absolute = new CUINullRect(100, 100, 100, 100),
        };

        frame["box1"] = new CUIComponent()
        {
          Background = { Color = new Color(255, 255, 0, 255) },
          Absolute = new CUINullRect(100, 100, 100, 100),
          Margin = new CUISizes(5, 10, 15, 20),
          Padding = new CUISizes(5, 10, 15, 20),
        };

        frame["box1"]["box2"] = new CUIComponent()
        {
          Background = { Color = new Color(0, 255, 0, 255) },
          Relative = new CUINullRect(0, 0, 1, 1),
        };

        return frame;
      }
    }
  }
}