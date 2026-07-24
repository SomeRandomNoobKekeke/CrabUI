using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{
  public static partial class CUIDefault
  {
    public class Frame : CUIFrame
    {
      public CUITextBlock Caption { get; }

      public Frame() : base()
      {
        Anchor = CUIAnchor.Center;
        VisualChildrenOrder = CUIDirection.Reverse;
        // Absolute = new(w: 400, h: 600);

        this["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          VisualChildrenOrder = CUIDirection.Reverse,
        };
        this["layout"]["handle"] = new CUIHorizontalList()
        {
          Absolute = new CUINullRect(h: ResizeHandle.DefaultSize.Y),
          Style = (c) =>
          {
            c.Background.Color = c.Palette["main"];
          },
        };
        this["layout"]["handle"]["caption"] = Caption = new CUITextBlock()
        {
          Flex = 1,
        };
        this["layout"]["handle"]["closebutton"] = new CUICloseButton()
        {
          CrossRelative = new CUINullRect(w: 1),
        };
      }

      public Frame(string caption) : this()
      {
        Caption.Text = caption;
      }
    }
  }
}