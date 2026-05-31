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
      public static CUIComponent SimpleList()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = Color.Gray,
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 400, 600),
        };

        CUIVerticalList list = new CUIVerticalList()
        {
          BackgroundColor = Color.Blue,
          Relative = new CUINullRect(0, 0, 1, 1),
        };

        frame.Append(list);

        list.Append(new CUIComponent()
        {
          BackgroundColor = Color.Red,
          Absolute = new CUINullRect(0, 0, 300, 100),
        });

        list.Append(new CUIComponent()
        {
          BackgroundColor = Color.Yellow,
          Absolute = new CUINullRect(30, 0, 350, 100),
        });

        list.Append(new CUITextLine()
        {
          Text = "123",
          BackgroundColor = Color.Green,
          Flex = 1,
        });

        list.Append(new CUITextLine()
        {
          Text = "321",
          BackgroundColor = Color.Pink,
          Flex = 3,
        });

        return frame;
      }
    }
  }
}