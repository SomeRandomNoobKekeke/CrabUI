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
  /// <summary>
  /// Not used
  /// It's very primitive, it assumes that all sprites are separated by 2px empty lines
  /// </summary>
  public class CUISpriteAtlas(CUITexture2D texture)
  {
    public Point TextureSize { get; set; } = new Point(64, 64);
    public CUITexture2D Texture { get; set; } = texture;

    public CUISprite Get(int x, int y)
    {
      return new CUISprite(Texture)
      {
        SourceRectangle = new Rectangle(
          1 + TextureSize.X * x,
          1 + TextureSize.Y * y,
          TextureSize.X,
          TextureSize.Y
        )
      };
    }

  }
}