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
    public class FrameHandle : CUIHorizontalList
    {
      public static ICUIStyle DefaultStyle => new CUIDefaultStyle<FrameHandle>((c) =>
      {
        c.Background.Color = Color.Lerp(c.Palette["back"], c.Palette["main"], 1.0f);
        c.FitContent = new CUIBool2(false, true);
      });

      protected override void InitStyle()
      {
        base.InitStyle();
        Background.Sprite = CUISprite.DimmedVertical;
      }
    }


    public class Frame : CUIFrame
    {
      public string Caption
      {
        get => Get<CUITextBlock>("layout.handle.caption").Text;
        set => Get<CUITextBlock>("layout.handle.caption").Text = value;
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
        this["layout"]["handle"] = new FrameHandle();
        this["layout"]["handle"]["caption"] = new CUITextBlock()
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
        Caption = caption;
        AKA = caption;
      }

      public Frame(string caption, float width, float height) : this()
      {
        Caption = caption;
        Absolute = new CUINullRect(w: width, h: height);
        AKA = caption;
      }
    }
  }
}