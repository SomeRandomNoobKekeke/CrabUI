using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{

  public class CUISlider : CUIComponent
  {
    public event Action<float> OnSlide;

    private float? pendingLambda;

    private float lambda; public float Lambda
    {
      get => lambda;
      set
      {
        lambda = value;
        pendingLambda = value;
      }
    }

    public CUIComponent Slider;
    public bool Vertical;

    public void HandleSlide()
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