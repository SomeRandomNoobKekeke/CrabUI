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
      public static CUIComponent BigList()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = Color.Gray,
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 400, 600),
        };

        frame["list"] = new CUIVerticalList()
        {
          BackgroundColor = Color.Blue,
          Relative = new CUINullRect(0, 0, 1, 1),
          AddMouseScroll = (e) =>
          {
            frame["list"].ChildrenOffset += new Vector2(0, e.Scroll);
            CUI.Logger.Log(frame["list"].ChildrenOffset);
          },
          ChildrenOffsetBounds = new CUIBoundaries(minY: 0),
        };

        for (int i = 1; i < 100; i++)
        {
          frame["list"].Append(new CUITextBlock()
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