using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComponentGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{
  public static partial class CUIDefault
  {
    public class Frame : CUIFrame
    {
      public CUITextBlock Caption { get; set; }

      public Frame() : base()
      {
        Anchor = CUIAnchor.Center;
        Absolute = new(w: 400, h: 600);


        this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
        this["layout"]["handle"] = new CUIHorizontalList()
        {
          FitContent = new CUIBool2(false, true),
          Style = (c) =>
          {
            c.Background.Color = c.Palette.Colors["border"];
          },
          // Background = { Color = Palette.Colors["border"] }
        };
        this["layout"]["handle"]["caption"] = Caption = new CUITextBlock()
        {
          Flex = 1,
        };
        this["layout"]["handle"]["closebutton"] = new CUIComponent()
        {
          FitContent = new(true, true),
          Background = { Color = Palette.Colors["border"] },
        };
        this["layout"]["handle"]["closebutton"]["bruh"] = new CUICloseButton();


        this["layout"]["main"] = new CUIComponent();
      }
    }
  }
}