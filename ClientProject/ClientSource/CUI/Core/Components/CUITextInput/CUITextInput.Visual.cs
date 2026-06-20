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
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUITextInput : CUIComponent, IComponent
  {
    public TextBlock TextBlock { get; } = new();
    public SimpleTexture CaretTexture { get; } = new();
    public SimpleTexture SelectionOverlay { get; } = new();

    public TextMeasurementsStruct TextMeasurements;

    public void UpdaTextMeasurements()
    {
      float MeasureX(string s) => TextBlock.Font.MeasureString(s).X;

      TextMeasurements = TextMeasurements with
      {
        CaretLeft = MeasureX(State.Text.Substring(0, State.CaretPos)),
        SelectionLeft = MeasureX(State.Text.Substring(0, State.SelectionStart)),
        SelectionRight = MeasureX(State.Text.Substring(0, State.SelectionEnd)),
      };
    }

    private void UpdateVisualState()
    {
      UpdateRects();

      Background.Color = Focused ? new Color(0, 255, 0, 64) : new Color(255, 0, 0, 64);
      CaretTexture.Color = Focused ? new Color(0, 255, 255, 127) : Color.Transparent;
    }

    private void UpdateRects()
    {
      TextBlock.Rect = Rect;
      TextBlock.Text = State.Text;


      CaretTexture.Rect = Rect with
      {
        Left = Rect.Left + TextMeasurements.CaretLeft,
        Width = 2,
        Top = Rect.Top + Rect.Height * 0.1f,
        Height = Rect.Height * 0.8f,
      };

      SelectionOverlay.Rect = Rect with
      {
        Left = Rect.Left + TextMeasurements.SelectionLeft,
        Width = TextMeasurements.SelectionWidth,
      };
    }



    protected override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);

      UpdateVisualState();
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Visible || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return SelectionOverlay.VisualWrapper;
      yield return TextBlock.VisualWrapper;
      yield return CaretTexture.VisualWrapper;
    }
  }
}