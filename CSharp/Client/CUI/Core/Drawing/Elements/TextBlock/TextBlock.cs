using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class TextBlock : IVisualElement, IMouseEventConsumer
  {
    public CUIRect Rect { get; set; }
    public Color TextColor { get; set; } = Color.White;
    public string Text { get; set; } = "";

    public CUIFont Font { get; set; } = CUIFont.Font;


    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();


    public void Draw(ICUISpriteBatch spriteBatch)
    {
      Font.DrawString(spriteBatch, Text, Rect.LeftTop, TextColor);
    }
  }
}