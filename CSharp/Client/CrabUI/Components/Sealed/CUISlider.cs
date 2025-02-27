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
  /// Horizontal range input
  /// </summary>
  public class CUISlider : CUIComponent
  {
    /// <summary>
    /// Happens when handle is dragged, value is [0..1]
    /// </summary>
    public event Action<float> OnSlide;
    public Action<float> AddOnSlide { set { OnSlide += value; } }
    public float InOutMult => (Real.Width - Real.Height) / Real.Width;

    private float lambda;
    private float? pendingLambda;
    public float Lambda
    {
      get => lambda;
      set
      {
        lambda = Math.Clamp(value, 0, 1);
        pendingLambda = lambda;
      }
    }

    [CUISerializable] public FloatRange Range { get; set; } = new FloatRange(0, 1);
    [CUISerializable] public int? Precision { get; set; } = 2;


    /// <summary>
    /// The handle
    /// </summary>
    public CUIComponent Handle;

    public CUIComponent LeftEnding;
    public CUIComponent RightEnding;
    public CUISprite MiddleSprite;

    public CUIRect MiddleRect;

    public Color MasterColor
    {
      set
      {
        if (LeftEnding != null) LeftEnding.BackgroundColor = value;
        if (RightEnding != null) RightEnding.BackgroundColor = value;
        if (Handle != null) Handle.BackgroundColor = value;
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      CUI.DrawRectangle(spriteBatch, MiddleRect, LeftEnding.BackgroundColor, MiddleSprite);
    }

    public CUISlider() : base()
    {
      ChildrenBoundaries = CUIBoundaries.Box;
      BreakSerialization = true;

      this["LeftEnding"] = LeftEnding = new CUIComponent()
      {
        Anchor = CUIAnchor.CenterLeft,
        Relative = new CUINullRect(h: 1),
        CrossRelative = new CUINullRect(w: 1),
        BackgroundSprite = CUI.TextureManager.GetCUISprite(2, 2, CUISpriteDrawMode.Resize, SpriteEffects.FlipHorizontally),
        Style = new CUIStyle()
        {
          ["Border"] = "Transparent",
          ["BackgroundColor"] = "CUIPalette.Slider",
        },
      };

      this["RightEnding"] = RightEnding = new CUIComponent()
      {
        Anchor = CUIAnchor.CenterRight,
        Relative = new CUINullRect(h: 1),
        CrossRelative = new CUINullRect(w: 1),
        BackgroundSprite = CUI.TextureManager.GetCUISprite(2, 2),
        Style = new CUIStyle()
        {
          ["Border"] = "Transparent",
          ["BackgroundColor"] = "CUIPalette.Slider",
        },
      };


      this["handle"] = Handle = new CUIComponent()
      {
        Style = new CUIStyle()
        {
          ["Border"] = "Transparent",
          ["BackgroundColor"] = "CUIPalette.Slider",
        },
        Draggable = true,
        BackgroundSprite = CUI.TextureManager.GetCUISprite(0, 2),
        Relative = new CUINullRect(h: 1),
        CrossRelative = new CUINullRect(w: 1),
        AddOnDrag = (x, y) =>
        {
          lambda = Math.Clamp(x / InOutMult, 0, 1);
          OnSlide?.Invoke(lambda);
          if (Command != null)
          {
            float value = Range.PosOf(lambda);
            if (Precision.HasValue) value = (float)Math.Round(value, Precision.Value);
            DispatchUp(new CUICommand(Command, value));
          }


        },
      };

      Handle.DragHandle.DragRelative = true;

      MiddleSprite = CUI.TextureManager.GetSprite("CUI.png", new Rectangle(44, 64, 6, 32));

      OnLayoutUpdated += () =>
      {
        MiddleRect = new CUIRect(
          Real.Left + Real.Height,
          Real.Top,
          Real.Width - 2 * Real.Height,
          Real.Height
        );

        if (pendingLambda.HasValue)
        {
          Handle.Relative = Handle.Relative with
          {
            Left = Math.Clamp(pendingLambda.Value, 0, 1) * InOutMult,
          };
          pendingLambda = null;
        }
      };

      OnConsume += (o) =>
      {
        if (float.TryParse(o.ToString(), out float value))
        {
          Lambda = Range.LambdaOf(value);
        }
      };


    }
  }
}