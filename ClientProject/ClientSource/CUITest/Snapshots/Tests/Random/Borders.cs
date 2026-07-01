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
      public static CUIComponent Borders()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "Borders" },
          Borders = {
            Visible = true,
            Sizes = new CUISizes(1,1,1,1),
          },
        };

        frame.Borders.MouseDown.Add((e) => CUI.Logger.Log("bruh"));

        frame["box1"] = new CUIComponent()
        {
          Background = { Color = new Color(0, 0, 255, 255) },
          Absolute = new CUINullRect(100, 100, 100, 100),
        };

        frame["box2"] = new CUIComponent()
        {
          Background = { Color = new Color(255, 255, 0, 255) },
          Absolute = new CUINullRect(100, 100, 100, 100),
          Borders = {
            Visible = true,
            Color = new Color(0,255,255,128),
            Sizes = new CUISizes(0,4,8,12),
          },
          Margin = new CUISizes(5, 5, 5, 5),
        };

        frame["box2"]["inside"] = new CUIComponent()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Background = { Color = Color.Magenta },
        };

        return frame;
      }
    }
  }
}