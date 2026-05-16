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
      public static CUIComponent SimpleForm()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = new Color(32, 32, 32),
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
        };

        frame["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1)
        };

        frame["layout"]["header"] = new CUITextBlock()
        {
          Text = "Header",
          BackgroundColor = Color.Brown,
          Absolute = new CUINullRect(h: 100),
        };

        frame["layout"]["main"] = new CUIComponent()
        {
          BackgroundColor = new Color(0, 0, 32),
          Flex = 1,
        };

        frame["layout"]["main"]["box"] = new CUIButton()
        {
          Absolute = new CUINullRect(w: 100, h: 100),
          Anchor = CUIAnchor.Center,
          Text = "Don't",
        };

        return frame;
      }
    }
  }
}