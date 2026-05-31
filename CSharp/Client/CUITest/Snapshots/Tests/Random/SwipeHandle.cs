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
      public static CUIComponent SwipeHandle()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = new Color(32, 32, 32),
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Swipeable = true,
          Draggable = false,
        };

        frame["a"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 100, 50, 50),
          BackgroundColor = Color.Blue,
        };

        frame["b"] = new CUIComponent()
        {
          Absolute = new CUINullRect(200, 100, 50, 50),
          BackgroundColor = Color.Green,
        };

        frame["c"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 200, 50, 50),
          BackgroundColor = Color.Red,
        };

        return frame;
      }
    }
  }
}