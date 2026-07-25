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
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUITextInput : CUIComponent, IComponent
  {
    public TextBlock TextBlock { get; } = new();
    public SimpleTexture CaretTexture { get; } = new();
    public SimpleTexture SelectionOverlay { get; } = new();

    public TextMeasurementsStruct TextMeasurements;

    protected double LastSomethingHappenedTime;
    protected bool CaretIsHidden;

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
      _UpdateRects();

      Background.Color = Focused ?
        Valid ?
          FocusedColor : InvalidColor
        : BluredColor;

      SelectionOverlay.Color = Focused ? SelectionColor : SelectionColor * 0.5f;

      CaretTexture.Visible = Focused && SelectionEmpty && !CaretIsHidden;
    }

    private void _UpdateRects()
    {
      TextBlock.Rect = ChildrenRect;
      TextBlock.Text = State.Text;


      CaretTexture.Rect = ChildrenRect with
      {
        Left = ChildrenRect.Left + TextMeasurements.CaretLeft - 1,
        Width = 2,
        Top = ChildrenRect.Top + ChildrenRect.Height * 0.1f,
        Height = ChildrenRect.Height * 0.8f,
      };

      SelectionOverlay.Rect = ChildrenRect with
      {
        Left = ChildrenRect.Left + TextMeasurements.SelectionLeft,
        Width = TextMeasurements.SelectionWidth,
        Top = ChildrenRect.Top + ChildrenRect.Height * 0.1f,
        Height = ChildrenRect.Height * 0.8f,
      };
    }



    protected override void UpdateRects()
    {
      base.UpdateRects();

      UpdateVisualState();
    }

    [CUISerializableProp]
    public virtual bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        SelectionOverlay.Visible = value;
        TextBlock.Visible = value;
        CaretTexture.Visible = value;
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;

      yield return VisualBounds.LeftBound;

      yield return SelectionOverlay.VisualWrapper;
      yield return TextBlock.VisualWrapper;
      yield return CaretTexture.VisualWrapper;

      yield return VisualBounds.RightBound;
    }
  }
}