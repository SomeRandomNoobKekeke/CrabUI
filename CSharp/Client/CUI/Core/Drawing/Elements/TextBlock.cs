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
    public Rectangle Rect { get; set; }
    public Color TextColor { get; set; }
    public string Text { get; set; }


    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();


    public void Draw(ICUISpriteBatch spriteBatch)
    {
      // spriteBatch.Draw(Texture, Rect, Color);
    }
  }
}