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
    public static partial class Layout
    {
      public static CUIComponent FitContentList()
      {

        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Resizable = true,
        };

        frame["wrapper1"] = new CUIVerticalList()
        {
          Absolute = new CUINullRect(100, 100, 30, 30),
          FitContent = new CUIBool2(true, true),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper1"]["text 1"] = new CUITextBlock()
        {
          Text = "text 1",
          ResizeStrategy = ResizeStrategy.Resist,
          Background = { Color = Color.Green },
          Absolute = new CUINullRect(w: 0),
        };

        frame["wrapper1"]["text 2"] = new CUITextBlock()
        {
          Text = "super long string of text",
          ResizeStrategy = ResizeStrategy.Resist,
          Background = { Color = Color.Orange },
        };

        return frame;
      }
    }
  }
}