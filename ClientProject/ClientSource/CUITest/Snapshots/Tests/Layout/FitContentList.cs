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
        };

        frame["list"] = new CUIVerticalList()
        {
          FitContent = new CUIBool2(true, true),
          Background = { Color = Color.Yellow },
        };

        frame["list"]["text 1"] = new CUITextBlock("text 1")
        {
          Absolute = new CUINullRect(w: 0),
          Background = { Color = Color.Green },
        };

        frame["list"]["text 2"] = new CUITextBlock("super long string of text")
        {
          Background = { Color = Color.Orange },
        };

        return frame;
      }
    }
  }
}