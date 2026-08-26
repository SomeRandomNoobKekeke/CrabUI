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

namespace CursedUI
{
  public static partial class CUIDefault
  {
    public class Frame : CUIFrame
    {
      public CUITextBlock CaptionBlock { get; }
      public string Caption
      {
        get => CaptionBlock.Text;
        set => CaptionBlock.Text = value;
      }

      public Frame() : base()
      {
        Anchor = CUIAnchor.Center;
        // VisualChildrenOrder = CUIDirection.Reverse;
        // Absolute = new(w: 400, h: 600);

        this["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          VisualChildrenOrder = CUIDirection.Reverse,
        };
        this["layout"]["handle"] = new CUIHorizontalList()
        {
          Absolute = new CUINullRect(h: ResizeHandle.DefaultSize.Y),
          Background = { Sprite = CUISprite.DimmedVertical },
          Style = (c) =>
          {
            c.Background.Color = c.Palette["main"];
          },
        };
        this["layout"]["handle"]["caption"] = CaptionBlock = new CUITextBlock()
        {
          Flex = 1,
          Padding = new CUISizes(0, 0, 0, 0),
        };
        this["layout"]["handle"]["closebutton"] = new CUICloseButton()
        {
          Background = {
            Sprite = CUISprite.DimmedVertical,
            Color = Palette["main"], //HACK
          },
          CrossRelative = new CUINullRect(w: 1),
        };
      }

      public Frame(string caption) : this()
      {
        CaptionBlock.Text = caption;
      }

      public Frame(string caption, float width, float height) : this()
      {
        CaptionBlock.Text = caption;
        Absolute = new CUINullRect(w: width, h: height);
      }
    }
  }
}