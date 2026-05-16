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
    Ignore, Rescale, Wrap
  }

  public class TextBlock : IVisualElement, IMouseEventConsumer
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



    public Color TextColor { get; set; } = Color.White;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.1f;


    public CUIFont Font { get; set; } = CUIFont.Font;

    public Vector2 RawTextSize { get; private set; }

    private string RealText;
    private Vector2 ReadTextDrawPosition;
    private float RealTextScale;
    public void Draw(CUISpriteBatch spriteBatch)
    {
      Font.DrawString(
        spriteBatch,
        RealText,
        ReadTextDrawPosition,
        TextColor,
        rotation: 0,
        origin: Vector2.Zero,
        RealTextScale,
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
      RealText = Text;
      RealTextScale = Scale;
      Vector2 RealTextSize = RawTextSize * RealTextScale;

      ReadTextDrawPosition = CUIAnchor.ChildPosIn(Rect, Anchor, RealTextSize);
    }

    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
    public ClearableEvent<CUIMouseEnterEvent> MouseEnter { get; } = new();
    public ClearableEvent<CUIMouseLeaveEvent> MouseLeave { get; } = new();
  }
}