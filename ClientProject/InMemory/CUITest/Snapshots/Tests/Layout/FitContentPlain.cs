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
    public static partial class Layout
    {
      public static CUIComponent FitContentPlain()
      {

        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Resizable = true,
        };

        frame["wrapper1"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 100, 30, 30),
          FitContent = new CUIBool2(true, true),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper1"]["text"] = new CUITextBlock()
        {
          Text = "Fit x,y",
          Absolute = new CUINullRect(50, 50),
          Background = { Color = Color.Orange },
        };


        frame["wrapper2"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 200, 200, 30),
          FitContent = new CUIBool2(false, true),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper2"]["text"] = new CUITextBlock()
        {
          Text = "Fit y",
          ResizeStrategy = ResizeStrategy.Resist,
          Absolute = new CUINullRect(50, 50),
          Background = { Color = Color.Orange },
        };


        frame["wrapper3"] = new CUIComponent()
        {
          Absolute = new CUINullRect(100, 300, 200, 30),
          FitContent = new CUIBool2(true, false),
          Background = { Color = Color.Yellow },
        };

        frame["wrapper3"]["text"] = new CUITextBlock()
        {
          Text = "Fit x",
          ResizeStrategy = ResizeStrategy.Resist,
          Absolute = new CUINullRect(50, 50),
          Background = { Color = Color.Orange },
        };

        return frame;
      }
    }
  }
}