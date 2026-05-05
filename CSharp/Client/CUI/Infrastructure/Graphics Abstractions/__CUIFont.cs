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
  public class __CUIFont(GUIFont font) : CUIFont
  {
    public static __CUIFont Font => new(GUIStyle.Font);

    public GUIFont GUIFont { get; } = font;


    public void DrawString(CUISpriteBatch sb, string text, Vector2 position, Color color)
    {
      if (sb is __CUISpriteBatch)
      {
        GUIFont.DrawString(((__CUISpriteBatch)sb).XNASpriteBatch, text, position, color);
      }
    }
  }
}