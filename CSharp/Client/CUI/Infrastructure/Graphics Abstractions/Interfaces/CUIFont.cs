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

    public void DrawString(CUISpriteBatch sb, string text, Vector2 position, Color color);
  }

}