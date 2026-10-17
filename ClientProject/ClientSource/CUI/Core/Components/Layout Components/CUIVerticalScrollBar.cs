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
  //LINK:\ClientProject\Tests\CUITest\Snapshots\Tests\Components\CUIScrollBar.cs
  public partial class CUIVerticalScrollBar : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle => new CUIDefaultStyle<CUIVerticalScrollBar>((c) =>
    {
      c.Background.Color = c.Palette["back"].To(c.Palette["main"], 0.5f);
      c.Slider.Background.Color = c.Palette["main"].To(Color.White, 0.5f);
    });

    protected override void InitStyle()
    {
      base.InitStyle();

      ChildrenBounds = CUIBoundaries.Box;
      ConsumeMouseEvents = true;

      Background.Sprite = CUISprite.VerticalTrack;

      Slider = new CUIComponent()
      {
        Background = {
          Sprite = CUISprite.VerticalTrackSlider,
        },
        Relative = new CUINullRect(w: 1),
        Absolute = new CUINullRect(h: 40),
        Draggable = true,
        ConsumeMouseEvents = true,
      };
    }

    private CUIComponent _Slider; public CUIComponent Slider
    {
      get => _Slider;
      set
      {
        if (_Slider is not null) _Slider.Dragged -= HandleDrag;
        this["slider"] = _Slider = value;
        if (_Slider is not null) _Slider.Dragged += HandleDrag;
      }
    }

    private void HandleDrag(Vector2 v)
    {
      prevLambda = Lambda;
      if (_ConnectedList is not null) _ConnectedList.RelativeScroll = prevLambda;
    }

    private float prevLambda;
    public float Lambda
    {
      get
      {
        float spaceToMove = Rect.Height - Slider.Rect.Height;
        float shift = Slider.Rect.Top - Rect.Top;
        return Math.Clamp(shift / spaceToMove, 0, 1);
      }
      set
      {
        prevLambda = value;

        float spaceToMove = Rect.Height - Slider.Rect.Height;
        float shift = value * spaceToMove;
        Slider.Absolute = Slider.Absolute with
        {
          Top = shift,
        };
      }
    }


    public Action<float> OnMoved { set { Moved += value; } }
    public event Action<float> Moved;


    public CUIVerticalList _ConnectedList;
    public CUIComponent ConnectedList
    {
      get => _ConnectedList;
      set
      {
        CUIVerticalList list = value as CUIVerticalList;

        if (_ConnectedList is not null) _ConnectedList.MouseScroll -= HandleConnectedListScroll;
        _ConnectedList = list;
        if (_ConnectedList is not null) _ConnectedList.MouseScroll += HandleConnectedListScroll;
      }
    }

    private void HandleConnectedListScroll(CUIMouseScrollEvent e)
    {
      Lambda = _ConnectedList.RelativeScroll;
    }

    protected override void UpdateRects()
    {
      base.UpdateRects();
      Lambda = prevLambda;
    }

    private float LambdaFromMousePos(Vector2 pos)
    {
      float spaceToMove = Rect.Height - Slider.Rect.Height;
      float shift = pos.Y - Rect.Top;
      return Math.Clamp(shift / spaceToMove, 0, 1);
    }

    public CUIVerticalScrollBar()
    {
      MouseDown += (e) =>
      {
        Lambda = LambdaFromMousePos(e.Pos - new Vector2(0, Slider.Rect.Height / 2.0f));
        if (_ConnectedList is not null) _ConnectedList.RelativeScroll = prevLambda;
      };
    }
  }
}