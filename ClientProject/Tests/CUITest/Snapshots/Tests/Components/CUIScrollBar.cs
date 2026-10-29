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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Components
    {
      //LINK:\ClientProject\ClientSource\CUI\Core\Components\Layout Components\CUIVerticalScrollBar.cs
      public static CUIComponent CUIScrollBar()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIScrollbar", 400, 600);

        frame["layout"]["main"] = new CUIHorizontalList() { Flex = 1 };
        frame["layout"]["main"]["list"] = new CUIVerticalList()
        {
          Flex = 1,
          Scrollable = true,
        };

        for (int i = 0; i < 100; i++)
        {
          frame["layout"]["main"]["list"].Children.Add(new CUITextBlock($"line {i}")
          {
            TextAnchor = CUIAnchor.LeftCenter,
          });
        }

        frame["layout"]["main"]["scrollbar"] = new CUIVerticalScrollBar()
        {
          Absolute = new CUINullRect(w: 24),
          ConnectedList = frame["layout"]["main"]["list"],
        };

        return frame;
      }
    }
  }
}