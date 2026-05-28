using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaroJunk;

namespace CrabUI
{
  public enum OversizeBehaviour
  {
    Ignore, Resist, Rescale, Wrap
  }

  public class TextBlock : VisualElementBase, IVisualElement
  {
    private CUIRect _Rect; public CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        RecalcReal();
      }
    }

    private string _Text = ""; public string Text
    {
      get => _Text;
      set
      {
        _Text = value;
        MeasureText();
      }
    }

    private float _Scale = 1.0f; public float Scale
    {
      get => _Scale;
      set
      {
        _Scale = Math.Max(0, value);
        MeasureText();
      }
    }


    public Vector2 Anchor { get; set; } = new Vector2(0.5f, 0.5f);

    public float? ForcedMinWidth { get; private set; }
    public float? ForcedMinHeight { get; private set; }


    public Color TextColor { get; set; } = Color.White;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.1f;


    public CUIFont Font { get; set; } = CUIFont.Font;

    public Vector2 RawTextSize { get; private set; }

    private string RealText = "";
    private Vector2 TextDrawPosition;
    private float RealTextScale;
    public void Draw(CUISpriteBatch spriteBatch)
    {
      Font.DrawString(
        spriteBatch,
        Text,
        TextDrawPosition,
        TextColor,
        rotation: 0,
        origin: Vector2.Zero,
        Scale,
        SpriteEffects,
        LayerDepth
      );
    }

    private void MeasureText()
    {
      RawTextSize = Font.MeasureString(Text);
    }

    private void RecalcReal()
    {
      Vector2 RealTextSize = RawTextSize * Scale;

      TextDrawPosition = CUIAnchor.ChildPosIn(Rect, Anchor, RealTextSize);
    }
  }
}