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
    public static partial class Layout
    {
      public static CUIComponent TextWrap()
      {
        CUIFrame frame = new CUIDefault.Frame("TextWrap", 400, 600);

        frame["layout"]["header"] = new CUIDefault.HorizontalPanel();

        for (int i = 100; i <= 400; i += 50)
        {
          int width = i;
          frame["layout"]["header"][$"{i}px"] = new CUIButton($"{i}px")
          {
            OnMouseDown = (e) => frame["layout"]["main"]["container"].Absolute = frame["layout"]["main"]["container"].Absolute with
            {
              Width = width,
            },
            Flex = 1,
          };
        }

        frame["layout"]["main"] = new CUIComponent() { Flex = 1 };
        frame["layout"]["main"]["container"] = new CUIVerticalList()
        {
          Anchor = CUIAnchor.Center,
          Relative = new CUINullRect(0, 0, 1, 1),
          Background = { Color = new Color(32, 0, 32) },
        };

        for (int i = 0; i < 10; i++)
        {
          frame["layout"]["main"]["container"].Children.Add(new CUITextBlock(Utils.Lorem)
          {
            ResizeStrategy = ResizeStrategy.Wrap,
            Margin = new CUISizes(10, 10, 10, 10),
            Background = { Color = new Color(0, 64, 64) },
          });
        }

        return frame;
      }
    }
  }
}