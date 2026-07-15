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
      public static CUIComponent ListScroll()
      {
        CUIFrame frame = new CUIDefault.Frame("ListScroll")
        {
          Absolute = new CUINullRect(w: 400, h: 600),
        };

        frame["layout"]["header"] = new CUIHorizontalList()
        {
          FitContent = new CUIBool2(false, true),
        };

        frame["layout"]["header"]["scroll1"] = new CUITextBlock("0")
        {
          Flex = 1,
        };

        frame["layout"]["header"]["scroll2"] = new CUITextBlock("0")
        {
          Flex = 1,
        };

        CUIComponent Main = frame["layout"]["main"] = new CUIComponent() { Flex = 1 };

        Main["list1"] = new CUIVerticalList()
        {
          Absolute = new CUINullRect(0, 0, 100, 200),
          Background = { Color = Color.Blue },
          Anchor = CUIAnchor.LeftCenter,
          Direction = CUIDirection.Straight,
          Scrollable = true,
          TopGap = 10,
          BottomGap = 30,
          OnMouseScroll = (c, e) => frame.Get<CUITextBlock>("layout.header.scroll1").Text = $"{(c as CUIVerticalList).Scroll}",
        };

        for (int i = 1; i <= 100; i++)
        {
          Main["list1"].Children.Add(new CUITextBlock($"child {i}"));
        }


        Main["list2"] = new CUIVerticalList()
        {
          Absolute = new CUINullRect(0, 0, 100, 200),
          Background = { Color = Color.Blue },
          Anchor = CUIAnchor.RightCenter,
          Direction = CUIDirection.Reverse,
          Scrollable = true,
          TopGap = 10,
          BottomGap = 30,
          OnMouseScroll = (c, e) => frame.Get<CUITextBlock>("layout.header.scroll2").Text = $"{(c as CUIVerticalList).Scroll}",
        };

        for (int i = 1; i <= 100; i++)
        {
          Main["list2"].Children.Add(new CUITextBlock($"child {i}"));
        }

        return frame;
      }
    }
  }
}