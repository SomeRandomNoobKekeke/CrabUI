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
  public class TextBlock : IVisualElement, IMouseEventConsumer
  {
    public string Text { get; set; } = "";
    public CUIRect Rect { get; set; }
    public Color TextColor { get; set; } = Color.White;
    public float Scale { get; set; } = 1.0f;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.1f;
    public ForceUpperCase ForceUpperCase { get; set; } = ForceUpperCase.Inherit;

    public CUIFont Font { get; set; } = CUIFont.Font;

    public void Draw(CUISpriteBatch spriteBatch)
    {
      // Font.DrawString(
      //   spriteBatch,
      //   Text,
      //   Rect.LeftTop,
      //   TextColor,
      //   Rotation,
      //   Origin,
      //   Scale,
      //   SpriteEffects,
      //   LayerDepth,
      //   Alignment,
      //   ForceUpperCase
      // );
    }

    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();
  }
}