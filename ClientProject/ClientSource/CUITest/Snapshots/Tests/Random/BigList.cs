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
      public static CUIComponent BigList()
      {
        CUIFrame frame = new()
        {
          Background = { Color = Color.Gray },
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 400, 600),
          Resizable = true,
        };

        frame["list"] = new CUIVerticalList()
        {
          Background = { Color = Color.Blue },
          Relative = new CUINullRect(0, 0, 1, 1),
          Scrollable = true,
        };

        for (int i = 1; i < 100; i++)
        {
          frame["list"].Children.Add(new CUITextBlock()
          {
            Text = $"child {i}",
            TextAnchor = CUIAnchor.LeftTop,
            Absolute = new CUINullRect(h: 30),
          });
        }



        return frame;
      }
    }
  }
}