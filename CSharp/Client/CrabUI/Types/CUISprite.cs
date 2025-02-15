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
    //TODO serialize
    public SpriteEffects Effects;

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
      Effects = SpriteEffects.None;
      DrawMode = CUISpriteDrawMode.Resize;
      Texture = BackupTexture;
      SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
    }
    public CUISprite(string path, Rectangle? sourceRect = null)
    {
      DrawMode = CUISpriteDrawMode.Resize;
      Effects = SpriteEffects.None;
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
      Effects = SpriteEffects.None;
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

    public override string ToString()
    {
      string mode = DrawMode != CUISpriteDrawMode.Resize ? $", Mode: {DrawMode}" : "";
      string rect = SourceRect != Texture.Bounds ? $", SourceRect: {CUIExtensions.RectangleToString(SourceRect)}" : "";
      string effect = Effects != SpriteEffects.None ? $", Effects: {CUIExtensions.SpriteEffectsToString(Effects)}" : "";

      return $"{{ Path: {Path}{mode}{rect}{effect} }}";
    }
    public static CUISprite Parse(string raw)
    {
      Dictionary<string, string> props = CUIExtensions.ParseKVPairs(raw);

      if (!props.ContainsKey("path")) return new CUISprite();

      CUISprite sprite = CUI.TextureManager.GetSprite(props["path"]);
      if (props.ContainsKey("mode"))
      {
        sprite.DrawMode = Enum.Parse<CUISpriteDrawMode>(props["mode"]);
      }
      if (props.ContainsKey("sourcerect"))
      {
        sprite.SourceRect = CUIExtensions.ParseRectangle(props["sourcerect"]);
      }
      else
      {
        sprite.SourceRect = new Rectangle(0, 0, sprite.Texture.Width, sprite.Texture.Height);
      }
      if (props.ContainsKey("effects"))
      {
        sprite.Effects = CUIExtensions.ParseSpriteEffects(props["effects"]);
      }


      return sprite;
    }
  }
}