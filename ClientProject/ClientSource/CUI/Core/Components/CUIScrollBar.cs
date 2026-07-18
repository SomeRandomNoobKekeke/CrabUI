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
  public partial class CUIScrollBar : CUIComponent, IComponent
  {
    private CUIComponent _Slider; public CUIComponent Slider
    {
      get => _Slider;
      set
      {
        _Slider = value;
        this["slider"] = value;
      }
    }

    public bool Vertical => ChildrenRect.Height > ChildrenRect.Width;
    public bool Horizontal => ChildrenRect.Width >= ChildrenRect.Height;
    public float MinDimension => Vertical ? ChildrenRect.Width : ChildrenRect.Height;
    private float _Lambda; public float Lambda
    {
      get => _Lambda;
      set
      {
        _Lambda = Math.Clamp(value, 0, 1);
        SyncLambda();
      }
    }

    private void UpdateLambda()
    {
      if (ChildrenRect.Width == ChildrenRect.Height)
      {
        _Lambda = 0;
        return;
      }

      _Lambda = Vertical ?
               (Slider.Rect.Top - ChildrenRect.Top) / (ChildrenRect.Height - Slider.Rect.Height) :
               (Slider.Rect.Left - ChildrenRect.Left) / (ChildrenRect.Width - Slider.Rect.Width);

      _Lambda = Math.Clamp(_Lambda, 0, 1);
    }

    private void SyncLambda()
    {
      Slider.Absolute = Slider.Absolute with
      {
        Position = Vertical ?
          new Vector2(0, Lambda * (ChildrenRect.Height - Slider.Rect.Height)) :
          new Vector2(Lambda * (ChildrenRect.Width - Slider.Rect.Width), 0)
      };
    }

    public Action<float> OnMoved { set { Moved += value; } }
    public event Action<float> Moved;

    protected override void UpdateRects()
    {
      base.UpdateRects();
      Slider.Absolute = Slider.Absolute with { Size = new Vector2(MinDimension, MinDimension) };
      SyncLambda();
    }

    public CUIScrollBar()
    {
      ChildrenBounds = CUIBoundaries.Box;
      ConsumeMouseEvents = true;

      Slider = new CUIComponent()
      {
        Background = { Color = Color.Blue },
        Draggable = true,
        OnDragged = (c, pos) =>
        {
          UpdateLambda();
          Moved?.Invoke(Lambda);
        },
      };
    }
  }
}