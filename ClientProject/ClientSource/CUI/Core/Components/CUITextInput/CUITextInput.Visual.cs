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

      float caretLeft = MeasureX(State.Text.Substring(0, State.CaretPos));
      float caretRight = MeasureX(State.Text.Substring(0, State.CaretPos + 1));

      TextMeasurements = TextMeasurements with
      {
        CaretOffsetX = caretLeft,
        CaretWidth = Math.Max(5, caretRight - caretLeft),
      };
    }

    private void UpdateVisualState()
    {
      TextBlock.Text = State.Text;

      Background.Color = Focused ? new Color(0, 255, 0, 64) : new Color(255, 0, 0, 64);

      CaretTexture.Color = Focused ? new Color(0, 255, 255, 64) : Color.Transparent;


      CaretTexture.Rect = Rect with
      {
        Left = Rect.Left + TextMeasurements.CaretOffsetX,
        Width = TextMeasurements.CaretWidth,
      };

      // VisualState = VisualState with
      // {
      //   CaretVisible = Focused,
      //   SelectionVisible = Focused && !State.SelectionEmpty
      // };
    }



    protected override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);
      TextBlock.Rect = rect;

      UpdateVisualState();
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Visible || CulledOut) yield break;

      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.PrimitiveVisualElement(TextBlock);
      yield return new VisualUnit.PrimitiveVisualElement(SelectionOverlay);
      yield return new VisualUnit.PrimitiveVisualElement(CaretTexture);
    }
  }
}