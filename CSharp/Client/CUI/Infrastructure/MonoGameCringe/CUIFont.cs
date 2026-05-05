using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public class CUIFont(GUIFont font) : ICUIFont
  {
    public static CUIFont Font { get; } = new(GUIStyle.Font);

    public GUIFont GUIFont { get; } = font;


    public void DrawString(ICUISpriteBatch sb, string text, Vector2 position, Color color)
    {
      if (sb is CUISpriteBatch)
      {
        GUIFont.DrawString(((CUISpriteBatch)sb).XNASpriteBatch, text, position, color);
      }
    }
  }
}