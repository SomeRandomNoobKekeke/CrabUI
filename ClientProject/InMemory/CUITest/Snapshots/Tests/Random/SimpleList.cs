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
    public static partial class Random
    {
      public static CUIComponent SimpleList()
      {
        CUIFrame frame = new()
        {
          Background = { Color = Color.Gray },
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 400, 600),
        };

        CUIVerticalList list = new CUIVerticalList()
        {
          Background = { Color = Color.Blue },
          Relative = new CUINullRect(0, 0, 1, 1),
        };

        frame.Children.Add(list);

        list.Add(new CUIComponent()
        {
          Background = { Color = Color.Red },
          Absolute = new CUINullRect(0, 0, 300, 100),
        });

        list.Add(new CUIComponent()
        {
          Background = { Color = Color.Yellow },
          Absolute = new CUINullRect(30, 0, 350, 100),
        });

        list.Add(new CUITextLine()
        {
          Text = "123",
          Background = { Color = Color.Green },
          Flex = 1,
        });

        list.Add(new CUITextLine()
        {
          Text = "321",
          Background = { Color = Color.Pink },
          Flex = 3,
        });

        return frame;
      }
    }
  }
}