using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CrabUI
{
  public partial class CUI
  {
    public const float Pi2 = (float)Math.PI / 2;

    public static void DrawTexture(SpriteBatch sb, CUIRect cuirect, Color cl, Texture2D texture, float depth = 0.0f)
    {
      Rectangle sourceRect = new Rectangle(0, 0, (int)cuirect.Width, (int)cuirect.Height);

      sb.Draw(texture, cuirect.Box, sourceRect, cl, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
    }
    public static void DrawRectangle(SpriteBatch sb, CUIRect cuirect, Color cl, CUISprite sprite, float depth = 0.0f)
    {
      Rectangle sourceRect = sprite.DrawMode switch
      {
        CUISpriteDrawMode.Resize => sprite.SourceRect,
        CUISpriteDrawMode.Wrap => new Rectangle(0, 0, (int)cuirect.Width, (int)cuirect.Height),
        CUISpriteDrawMode.Static => cuirect.Box,
        CUISpriteDrawMode.StaticDeep => cuirect.Zoom(0.9f),
        _ => sprite.SourceRect,
      };

      sb.Draw(sprite.Texture, cuirect.Box, sourceRect, cl, 0.0f, Vector2.Zero, sprite.Effects, depth);
    }

    /*
        public static void DrawBorders(SpriteBatch sb, CUIRect cuirect, Color cl, CUISprite sprite, float thickness, float depth = 0.0f)
        {
          Texture2D texture = sprite?.Texture ?? CUISprite.BackupTexture;

          Rectangle rect;
          Rectangle source;

          // top
          rect = CUIRect.CreateRect(
            cuirect.Left - thickness - 1, cuirect.Top - thickness - 1,
            cuirect.Width + 2 * thickness, thickness
          );
          source = new Rectangle(0, 0, rect.Width, rect.Height);
          sb.Draw(texture, rect, source, cl, 0.0f, Vector2.Zero, SpriteEffects.None, depth);

          // right
          rect = CUIRect.CreateRect(
            cuirect.Right, cuirect.Top - 1,
            thickness, cuirect.Height
          );
          source = new Rectangle(0, 0, rect.Width, rect.Height);
          sb.Draw(texture, rect, source, cl, 0.0f, Vector2.Zero, SpriteEffects.None, depth);

          // bottom
          rect = CUIRect.CreateRect(
            cuirect.Left - thickness - 1, cuirect.Bottom - 1,
            cuirect.Width + 2 * thickness, thickness
          );
          source = new Rectangle(0, 0, rect.Width, rect.Height);
          sb.Draw(texture, rect, source, cl, 0.0f, Vector2.Zero, SpriteEffects.None, depth);

          // left
          rect = CUIRect.CreateRect(
            cuirect.Left - thickness - 1, cuirect.Top - 1,
            thickness + 1, cuirect.Height
          );
          source = new Rectangle(0, 0, rect.Width, rect.Height);
          sb.Draw(texture, rect, source, cl, 0.0f, Vector2.Zero, SpriteEffects.None, depth);

        }
    */

  }
}