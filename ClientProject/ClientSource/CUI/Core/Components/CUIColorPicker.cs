using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CursedUI
{

  public class CUIColorPicker : CUIComponent
  {
    private CUIPosSelect HueSelect;
    private CUIPosSelect ColorSelect;

    private CUIComponent ColorSelectMark;
    private CUIComponent HueSelectMark;

    public float? HueSelectWidth
    {
      get => this["layout"]["HueSelect"].Absolute.Width;
      set => this["layout"]["HueSelect"].Absolute = this["layout"]["HueSelect"].Absolute with
      {
        Width = value
      };
    }

    private float _Hue; public float Hue
    {
      get => _Hue;
      private set
      {
        _Hue = value;
        HueColor = CUIColor.FromHSV(_Hue, 1, 1);
      }
    }
    public Color HueColor
    {
      get => ColorSelect.Background.Sprite.ColorTR;
      private set => ColorSelect.Background.Sprite.ColorTR = value;
    }

    public Vector2 ColorPos { get; private set; }

    public Action<Color> OnSelected { set { Selected += value; } }
    public event Action<Color> Selected;


    private void SelectColor()
    {
      Color cl = CUIColor.FromHSV(Hue, ColorPos.X, 1.0f - ColorPos.Y);

      Selected?.Invoke(cl);
    }

    public CUIColorPicker()
    {
      ConsumeMouseEvents = true;

      if (!CUICore.TextureManager.Has("HueSelect"))
      {
        CUICore.TextureManager.Add("HueSelect",
          new TextureBuilder(1, 360)
            .Fill((x, y) => CUIColor.FromHSV(y, 1, 1))
            .Build(tracked: true)
        );
      }

      if (!CUICore.TextureManager.Has("bw target 6x6"))
      {
        CUICore.TextureManager.Add("bw target 6x6",
          new TextureBuilder(6, 6)
            .DrawTarget(new Rectangle(0, 0, 6, 6), Color.White * 0.75f, Color.Black * 0.75f)
            .Build(tracked: true)
        );
      }


      this["layout"] = new CUIHorizontalList() { Relative = new CUINullRect(0, 0, 1, 1) };

      this["layout"]["ColorSelect"] = ColorSelect = new CUIPosSelect()
      {
        Flex = 1,
        Background = {
          Sprite = new CUISprite()
          {
            ColorTL = Color.White,
            ColorTR = Color.Red,
            ColorBR = Color.Black,
            ColorBL = Color.Black,
          },
        },
        OnSelected = (v) =>
        {
          ColorPos = v;
          ColorSelectMark.Relative = ColorSelectMark.Relative with { Position = v };
          SelectColor();
        },
      };

      this["layout"]["ColorSelect"]["mark"] = ColorSelectMark = new CUIComponent()
      {
        Absolute = new CUINullRect(w: 6, h: 6),
        Background = {
          Sprite = CUISprite.Get("bw target 6x6")
        },
        Anchor = CUIAnchor.Center,
        ParentAnchor = CUIAnchor.LeftTop,
      };

      this["layout"]["HueSelect"] = HueSelect = new CUIPosSelect()
      {
        Absolute = new CUINullRect(w: 30),
        Borders = { Sizes = new CUISizes(left: 2) },
        Style = (c) => c.Borders.Color = Color.White,
        Background = { Sprite = CUISprite.Get("HueSelect") },
        OnSelected = (v) =>
        {
          Hue = (float)Math.Floor(v.Y * 360);
          HueSelectMark.Relative = HueSelectMark.Relative with { Top = v.Y };
          SelectColor();
        },
      };

      this["layout"]["HueSelect"]["mark"] = HueSelectMark = new CUIComponent()
      {
        Absolute = new CUINullRect(h: 1),
        Relative = new CUINullRect(w: 1),
        Background = { Color = Color.White },
      };


    }
  }
}