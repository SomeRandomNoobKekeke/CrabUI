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
          Background = { Color = new Color(32, 32, 32) },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Resizable = true,
        };

        frame["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Background = { Color = Color.Red },
        };

        frame["layout"]["header"] = new CUIHorizontalList()
        {
          Background = { Color = Color.Blue },
          Absolute = new CUINullRect(h: 100),
        };

        frame["layout"]["header"]["caption"] = new CUITextBlock()
        {
          Text = "header",
          Flex = 1
        };

        frame["layout"]["header"]["close"] = new CUICloseButton()
        {
          Absolute = new CUINullRect(w: 100, h: 100),
        };

        frame["layout"]["main"] = new CUIComponent()
        {
          Background = { Color = new Color(0, 0, 32) },
          Flex = 1,
        };

        frame["layout"]["main"]["box"] = new CUIButton()
        {
          Absolute = new CUINullRect(w: 100, h: 100),
          Anchor = CUIAnchor.Center,
          Text = "Don't",
        };

        CUIComponent bruh = CUIComponent.CreateFromXML(frame.ToXML());

        return bruh;
      }
    }
  }
}