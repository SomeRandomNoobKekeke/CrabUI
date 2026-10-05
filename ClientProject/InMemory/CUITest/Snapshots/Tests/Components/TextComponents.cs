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
      public static CUIComponent TextComponents()
      {
        CUIFrame frame = new CUIDefault.Frame("TextComponents", 400, 600);

        frame["textblock1"] = new CUITextBlock()
        {
          Text = "Resize Strategy Passive",
          Absolute = new CUINullRect(null, 50, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Passive,
        };

        frame["textblock2"] = new CUITextBlock()
        {
          Text = "Resize Strategy Rescale",
          Absolute = new CUINullRect(null, 80, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Rescale,
        };

        frame["textblock3"] = new CUITextBlock()
        {
          Text = "Resize Strategy Resist",
          Absolute = new CUINullRect(null, 110, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Resist,
        };

        frame["textblock4"] = new CUITextBlock()
        {
          Text = "Resize Strategy Wrap",
          Absolute = new CUINullRect(null, 140, null, 20),
          Relative = new CUINullRect(0.1f, null, 0.8f, null),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
          ResizeStrategy = ResizeStrategy.Wrap,
        };

        frame["textline 1"] = new CUITextLine()
        {
          Text = "text line",
          Absolute = new CUINullRect(y: 170),
          Relative = new CUINullRect(x: 0.1f),
          Background = { Color = new Color(64, 0, 64) },
          TextColor = Color.White,
        };

        return frame;
      }
    }
  }
}