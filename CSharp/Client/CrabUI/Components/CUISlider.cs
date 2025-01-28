using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Aka range input, aka scrollbar
  /// has draggable handle and outputs its relative pos in the slider [0..1]
  /// </summary>
  public class CUISlider : CUIComponent
  {
    /// <summary>
    /// Happens when handle is dragged, value is [0..1]
    /// </summary>
    public event Action<float> OnSlide;

    private float? pendingLambda;


    private float lambda;
    /// <summary>
    /// Relative handle position [0..1]
    /// </summary>
    public float Lambda
    {
      get => lambda;
      set
      {
        lambda = value;
        pendingLambda = value;
      }
    }

    /// <summary>
    /// The handle
    /// </summary>
    public CUIComponent Slider;
    private bool Vertical;

    private void HandleSlide()
    {
      if (Vertical)
      {
        lambda = (Slider.Real.Top - Real.Top) / (Real.Height - Slider.Real.Height);
      }
      else
      {
        lambda = (Slider.Real.Left - Real.Left) / (Real.Width - Slider.Real.Width);
      }
      OnSlide?.Invoke(lambda);
    }

    public CUISlider() : base()
    {
      ChildrenBoundaries = CUIBoundaries.Box;

      this["slider"] = Slider = new CUIComponent()
      {
        BackgroundColor = Color.Blue,
        BorderColor = Color.Transparent,
        Draggable = true,
        AddOnDrag = (x, y) => HandleSlide(),
      };

      OnLayoutUpdated += () =>
      {
        Vertical = Real.Height >= Real.Width;

        float minDimension = Math.Min(Real.Width, Real.Height);
        Slider.Absolute = Slider.Absolute with { Size = new Vector2(minDimension, minDimension) };

        if (pendingLambda != null)
        {
          if (Vertical)
          {
            Slider.Absolute = Slider.Absolute with
            {
              Top = pendingLambda * (Real.Height - Slider.Absolute.Height)
            };
          }
          else
          {
            Slider.Absolute = Slider.Absolute with
            {
              Left = pendingLambda * (Real.Width - Slider.Absolute.Width)
            };
          }

          pendingLambda = null;
        }
      };
    }
  }
}