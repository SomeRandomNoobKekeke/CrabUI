using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public class __CUIFont(GUIFont font) : CUIFont
  {
    public static __CUIFont Font => new(GUIStyle.Font);

    public GUIFont GUIFont { get; } = font;


    public float LineHeight => GUIFont.LineHeight;
    public string WrapText(string text, float width) => GUIFont.WrapText(text, width);
    public Vector2 MeasureString(string str, bool removeExtraSpacing = false)
      => GUIFont.MeasureString(str, removeExtraSpacing);

    public void DrawString(CUISpriteBatch sb, string text, Vector2 position, Color color, ForceUpperCase forceUpperCase = Barotrauma.ForceUpperCase.Inherit, bool italics = false)
    {
      if (text is null) return;

      if (sb is __CUISpriteBatch)
      {
        GUIFont.DrawString(((__CUISpriteBatch)sb).XNASpriteBatch, text, position, color, forceUpperCase, italics);
      }
    }

    public void DrawString(CUISpriteBatch sb, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, Alignment alignment = Alignment.TopLeft, ForceUpperCase forceUpperCase = Barotrauma.ForceUpperCase.Inherit)
    {
      if (text is null) return;

      if (sb is __CUISpriteBatch)
      {
        GUIFont.DrawString(((__CUISpriteBatch)sb).XNASpriteBatch, text, position, color, rotation, origin, scale, spriteEffects, layerDepth, alignment, forceUpperCase);
      }
    }
  }
}