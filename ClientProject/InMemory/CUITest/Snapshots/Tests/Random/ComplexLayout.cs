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
      public static CUIComponent ComplexLayout()
      {
        CUIFrame frame = new()
        {
          Background = { Color = new Color(32, 32, 32) },
          Absolute = new CUINullRect(0, 0, 400, 400),
          Anchor = CUIAnchor.Center,
        };

        frame["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
        var header = frame["layout"]["header"] = new CUIComponent()
        {
          Background = { Color = Color.Brown },
          FitContent = new CUIBool2(false, true),
        };

        header["wrapper"] = new CUIComponent()
        {
          Absolute = new CUINullRect(h: 100),
          FitContent = new CUIBool2(true, false),
          Background = { Color = Color.Lime },
        };

        header["wrapper"]["text"] = new CUITextBlock("Text")
        {
          Anchor = CUIAnchor.RightCenter,
          TextColor = Color.Black,
          Padding = new CUISizes(0, 20, 0, 20),
        };

        frame["layout"]["main"] = new CUIComponent()
        {
          Flex = 1,
          Background = { Color = Color.Blue },
        };


        frame["layout"]["footer"] = new CUIVerticalList()
        {
          FitContent = new CUIBool2(false, true),
          Background = { Color = Color.Red },
        };

        frame["layout"]["footer"]["text"] = new CUITextBlock("footer")
        {
          Anchor = CUIAnchor.RightCenter,
        };

        return frame;
      }
    }
  }
}