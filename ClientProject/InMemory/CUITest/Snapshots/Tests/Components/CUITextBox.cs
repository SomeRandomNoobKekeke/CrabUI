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
    public static partial class Components
    {
      public static CUIComponent CUITextBox()
      {

        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 60, 200),
          Anchor = CUIAnchor.Center,
          Resizable = true,
        };

        frame["textbox1"] = new CUITextBlock()
        {
          Text = "Resize Strategy Passive",
          Absolute = new CUINullRect(null, 10, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Passive,
        };

        frame["textbox2"] = new CUITextBlock()
        {
          Text = "Resize Strategy Rescale",
          Absolute = new CUINullRect(null, 40, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Rescale,
        };

        frame["textbox3"] = new CUITextBlock()
        {
          Text = "Resize Strategy Resist",
          Absolute = new CUINullRect(null, 70, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Resist,
        };

        frame["textbox4"] = new CUITextBlock()
        {
          Text = "Resize Strategy Wrap",
          Absolute = new CUINullRect(null, 100, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Wrap,
        };

        return frame;
      }
    }
  }
}