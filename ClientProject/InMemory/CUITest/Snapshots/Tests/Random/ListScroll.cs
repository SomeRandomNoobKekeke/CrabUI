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
      public static CUIComponent ListScroll()
      {
        IEnumerable<CUIComponent> LotsOfTextBlocks()
        {
          return Enumerable.Range(0, 30).Select(i => new CUITextBlock($"child {i}"));
        }

        CUIFrame frame = new CUIDefault.Frame("ListScroll", 600, 600);


        CUIComponent Main = new CUIComponent() { Flex = 1 };
        frame["layout"]["main"] = Main;

        Main["list left"] = new CUIDefault.VerticalPanel()
        {
          Absolute = new CUINullRect(0, 0, 100, 200),
          Anchor = CUIAnchor.LeftCenter,
          Direction = CUIDirection.Straight,
          Scrollable = true,
          TopGap = 10,
          BottomGap = 30,
          Children = { AddBulk = LotsOfTextBlocks() },
        };

        Main["list right"] = new CUIDefault.VerticalPanel()
        {
          Absolute = new CUINullRect(0, 0, 100, 200),
          Anchor = CUIAnchor.RightCenter,
          Direction = CUIDirection.Reverse,
          Scrollable = true,
          TopGap = 10,
          BottomGap = 30,
          Children = { AddBulk = LotsOfTextBlocks() },
        };

        Main["list top"] = new CUIDefault.HorizontalPanel()
        {
          Absolute = new CUINullRect(0, 0, 200, 100),
          Anchor = CUIAnchor.CenterTop,
          Direction = CUIDirection.Straight,
          Scrollable = true,
          LeftGap = 10,
          RightGap = 30,
          Children = { AddBulk = LotsOfTextBlocks() },
        };

        Main["list bottom"] = new CUIDefault.HorizontalPanel()
        {
          Absolute = new CUINullRect(0, 0, 200, 100),
          Anchor = CUIAnchor.CenterBottom,
          Direction = CUIDirection.Reverse,
          Scrollable = true,
          LeftGap = 10,
          RightGap = 30,
          Children = { AddBulk = LotsOfTextBlocks() },
        };

        return frame;
      }
    }
  }
}