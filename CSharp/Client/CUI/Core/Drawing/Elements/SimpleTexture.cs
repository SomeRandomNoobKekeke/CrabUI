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
  public class SimpleTexture : IVisualElement, IMouseEventConsumer
  {
    public Rectangle Rect { get; set; }
    public CUITexture2D Texture { get; set; } = CUITexture2D.White;
    public Color Color { get; set; }


    public ClearableEvent<CUIMouseDownEvent> MouseDown { get; } = new();
    public ClearableEvent<CUIMouseUpEvent> MouseUp { get; } = new();
    public ClearableEvent<CUIMouseClickEvent> MouseClick { get; } = new();
    public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick { get; } = new();
    public ClearableEvent<CUIMouseMovedEvent> MouseMoved { get; } = new();


    public void Draw(ICUISpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Texture, Rect, Color);
    }

    //TODO this is massive code duplication, why every visual element has to define that m1down should trigger onclick?
    public void HandleInput(CUIInput input)
    {
      // if (input.M1Down) OnClick?.Invoke();
    }
  }
}