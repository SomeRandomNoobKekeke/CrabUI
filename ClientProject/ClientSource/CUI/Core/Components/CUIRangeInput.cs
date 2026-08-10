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
using CUILibs;

namespace CrabUI
{
  public partial class CUIRangeInput : CUIComponent, IComponent
  {
    public record Interval(float Start, float End, float Pin)
    {
      public bool IsInside(float lambda) => Start <= lambda && lambda <= End;
    }

    protected override void InitStyle()
    {
      base.InitStyle();
      // Background.Color = Color.Brown;

      // LeftLineEnd.Color = Color.Red;
      // RightLineEnd.Color = Color.Red;
    }


    private float _Lambda; public float Lambda
    {
      get => _Lambda;
      set
      {
        _Lambda = value;
        PinLambda();
        SyncHandleWithLambda();
      }
    }


    private void SetLambdaFromPoint(Vector2 v)
    {
      Lambda = (v.X - ChildrenRect.Left) / (ChildrenRect.Width - Handle.Rect.Width);
    }
    private void SyncLambdaWithHandle()
    {
      _Lambda = (Handle.Rect.Left - ChildrenRect.Left) / (ChildrenRect.Width - Handle.Rect.Width);
    }

    private void SyncHandleWithLambda()
    {
      Handle.Absolute = Handle.Absolute with
      {
        Left = _Lambda * (ChildrenRect.Width - Handle.Rect.Width)
      };
    }

    private void PinLambda()
    {
      for (int i = 0; i < Intervals.Count; i++)
      {
        if (Intervals[i].IsInside(_Lambda))
        {
          _Lambda = Intervals[i].Pin;
          _Pin = i;
          break;
        }
      }
    }

    private int _Pin; public int Pin
    {
      get => _Pin;
      set
      {
        if (Intervals.Count == 0) return;
        _Pin = Math.Clamp(value, 0, Intervals.Count - 1);
        _Lambda = Intervals[_Pin].Pin;
        SyncHandleWithLambda();
      }
    }

    public int PinCount
    {
      get => Intervals.Count;
      set => SetPins(value);
    }

    /// <summary>
    /// |-------  -------|-------    -------|-------    -------|-------  -------|
    /// </summary>
    public void SetPins(int count)
    {
      Intervals = new();

      if (count < 1) return;

      if (count == 1)
      {
        Intervals.Add(new Interval(0.0f, 1.0f, 0.5f));
        return;
      }

      // count >= 2
      float dl = 1.0f / (count - 1) / 2.0f;

      Intervals.Add(new Interval(0.0f, dl, 0.0f)); // First pin

      for (int i = 1; i < count - 1; i++)
      {
        float pin = dl * 2 * i;

        Intervals.Add(
          new Interval(pin - dl, pin + dl, pin)
        );
      }

      Intervals.Add(new Interval(1.0f - dl, 1.0f, 1.0f)); // Last pin
    }
    public List<Interval> Intervals { get; set; } = new();

    public SimpleTexture LeftLineEnd { get; } = new() { Sprite = CUISprite.LeftLineEnd };
    public SimpleTexture RightLineEnd { get; } = new() { Sprite = CUISprite.RightLineEnd };
    public SimpleTexture LineCenter { get; } = new() { Sprite = CUISprite.LineCenter };
    public CUIComponent Handle { get; }

    protected override void UpdateRects()
    {
      base.UpdateRects();

      float squareSide = ChildrenRect.Height;

      LeftLineEnd.Rect = new CUIRect(
        ChildrenRect.Left,
        ChildrenRect.Top,
        squareSide,
        ChildrenRect.Height
      );

      RightLineEnd.Rect = new CUIRect(
        ChildrenRect.Left + ChildrenRect.Width - squareSide,
        ChildrenRect.Top,
        squareSide,
        ChildrenRect.Height
      );

      LineCenter.Rect = new CUIRect(
        ChildrenRect.Left + squareSide,
        ChildrenRect.Top,
        ChildrenRect.Width - 2 * squareSide,
        ChildrenRect.Height
      );

      Handle.Rect = new CUIRect(
        ChildrenRect.Left + (ChildrenRect.Width - squareSide) * Lambda,
        ChildrenRect.Top,
        squareSide,
        ChildrenRect.Height
      );
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;

      yield return LineCenter.VisualWrapper;
      yield return LeftLineEnd.VisualWrapper;
      yield return RightLineEnd.VisualWrapper;
      yield return Handle.VisualWrapper;

      yield return VisualBounds.RightBound;

      yield return Borders.VisualWrapper;
    }

    public CUIRangeInput()
    {
      ChildrenBounds = CUIBoundaries.Box;
      ConsumeMouseEvents = true;

      this["handle"] = Handle = new CUIComponent()
      {
        Background = { Sprite = CUISprite.Handle },
        Relative = new CUINullRect(h: 1),
        CrossRelative = new CUINullRect(w: 1),
        Draggable = true,
      };

      RectSet += (c, rect) => SyncHandleWithLambda();
      Handle.Dragged += (c, v) => SyncLambdaWithHandle();
      Handle.DragEnded += (c, v) =>
      {
        SyncLambdaWithHandle();
        PinLambda();
        SyncHandleWithLambda();
      };
      MouseDown += (c, e) => SetLambdaFromPoint(e.Pos);
    }
  }
}