using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public enum CUISpriteDrawMode
  {
    Resize,
    Wrap,
    Static,
    StaticDeep,
  }

  /// <summary>
  /// Wrapper, containing link to texture, source rect, path, draw mode
  /// </summary>
  public struct CUISprite
  {
    public static Texture2D BackupTexture => GUI.WhiteTexture;
    public static CUISprite Default => new CUISprite();
    public string Path;
    public Texture2D Texture;
    public CUISpriteDrawMode DrawMode;
    public Rectangle SourceRect;

    public static CUISprite FromVanilla(Sprite sprite)
    {
      if (sprite == null) return Default;

      return new CUISprite(sprite.Texture, sprite.SourceRect)
      {
        Path = sprite.FullPath,
      };
    }

    public static CUISprite FromName(string name) => FromId(new Identifier(name));
    public static CUISprite FromId(Identifier id)
    {
      GUIComponentStyle? style = GUIStyle.ComponentStyles[id];
      if (style == null) return Default;

      return FromComponentStyle(style);
    }

    public static CUISprite FromComponentStyle(GUIComponentStyle style, GUIComponent.ComponentState state = GUIComponent.ComponentState.None)
    {
      return FromVanilla(style.Sprites[state].FirstOrDefault()?.Sprite);
    }


    public CUISprite()
    {
      Path = "";
      DrawMode = CUISpriteDrawMode.Resize;
      Texture = BackupTexture;
      SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
    }
    public CUISprite(string path, Rectangle? sourceRect = null)
    {
      DrawMode = CUISpriteDrawMode.Resize;
      Path = path;
      Texture = CUI.TextureManager.GetTexture(path);
      if (sourceRect.HasValue)
      {
        SourceRect = sourceRect.Value;
      }
      else
      {
        SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
      }
    }
    public CUISprite(Texture2D texture, Rectangle? sourceRect = null)
    {
      Path = "";
      DrawMode = CUISpriteDrawMode.Resize;
      Texture = texture ?? BackupTexture;
      if (sourceRect.HasValue)
      {
        SourceRect = sourceRect.Value;
      }
      else
      {
        SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
      }
    }

    public override string ToString() => Path;
    public static CUISprite Parse(string path) => CUI.TextureManager.GetSprite(path);
  }
}