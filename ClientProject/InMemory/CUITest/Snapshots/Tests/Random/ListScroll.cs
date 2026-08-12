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

        CUIComponent Main = new CUIComponent() { Flex = 1 };
        frame["layout"]["main"] = Main;

        CUIVerticalList list1 = new CUIVerticalList()
        {
          Absolute = new CUINullRect(0, 0, 100, 200),
          Background = { Color = CUICore.Palettes.Secondary["main"] },
          Anchor = CUIAnchor.LeftCenter,
          Direction = CUIDirection.Straight,
          Scrollable = true,
          TopGap = 10,
          BottomGap = 30,
        };
        list1.MouseScroll += (e) => frame.Get<CUITextBlock>("layout.header.scroll1").Text = $"{list1.Scroll}";
        Main["list1"] = list1;

        for (int i = 1; i <= 100; i++)
        {
          Main["list1"].Children.Add(new CUITextBlock($"child {i}"));
        }

        CUIVerticalList list2 = new CUIVerticalList()
        {
          Absolute = new CUINullRect(0, 0, 100, 200),
          Background = { Color = CUICore.Palettes.Secondary["main"] },
          Anchor = CUIAnchor.RightCenter,
          Direction = CUIDirection.Reverse,
          Scrollable = true,
          TopGap = 10,
          BottomGap = 30,
        };
        list2.MouseScroll += (e) => frame.Get<CUITextBlock>("layout.header.scroll2").Text = $"{list2.Scroll}";
        Main["list2"] = list2;

        for (int i = 1; i <= 100; i++)
        {
          Main["list2"].Children.Add(new CUITextBlock($"child {i}"));
        }

        return frame;
      }
    }
  }
}