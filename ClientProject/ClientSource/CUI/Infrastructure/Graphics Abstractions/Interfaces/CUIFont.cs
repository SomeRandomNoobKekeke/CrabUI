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
  public interface CUIFont
  {
    public static CUIFont Font => __CUIFont.Font;

    public float LineHeight { get; }
    public string WrapText(string text, float width);
    public Vector2 MeasureString(string str, bool removeExtraSpacing = false);

    public void DrawString(
      CUISpriteBatch sb,
      string text,
      Vector2 position,
      Color color,
      ForceUpperCase forceUpperCase = Barotrauma.ForceUpperCase.Inherit,
      bool italics = false
    );

    public void DrawString(
      CUISpriteBatch sb,
      string text,
      Vector2 position,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects spriteEffects,
      float layerDepth,
      Alignment alignment = Alignment.TopLeft,
      ForceUpperCase forceUpperCase = Barotrauma.ForceUpperCase.Inherit
    );
  }

}