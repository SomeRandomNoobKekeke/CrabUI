using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class SimpleTexture : IVisualElement
  {
    public Rectangle Rect { get; set; }
    public CUITexture Texture { get; set; } = CUITexture.White;
    public Color Color { get; set; }

    public event Action OnClick;


    public void Draw(CUISpriteBatch spriteBatch)
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