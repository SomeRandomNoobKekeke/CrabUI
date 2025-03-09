using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
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
  /// Wrapper Containing link to texture and drawing settings,  
  /// like SourceRedt, DrawMode, Effects, Rotation...  
  /// Multiple sprites can use the same texture
  /// </summary>
  public class CUISprite : ICloneable
  {
    /// <summary>
    /// 1x1 white texture
    /// </summary>
    public static Texture2D BackupTexture => GUI.WhiteTexture;
    /// <summary>
    /// new Sprite that uses 1x1 default texture 
    /// </summary>
    public static CUISprite Default => new CUISprite();

    /// <summary>
    /// Set when you load it from some path
    /// </summary>
    public string Path = "";
    /// <summary>
    /// None, FlipHorizontally, FlipVertically
    /// </summary>
    public SpriteEffects Effects;
    /// <summary>
    /// Resize - will resize the sprite to component  
    /// Wrap - will loop the texture  
    /// Static - sprite ignores component position
    /// </summary>
    public CUISpriteDrawMode DrawMode;
    /// <summary>
    /// Part of the texture that is drawn  
    /// Won't work in Wrap mode becase it will loop the whole texture
    /// </summary>
    public Rectangle SourceRect;
    private Texture2D texture = BackupTexture;
    /// <summary>
    /// The link to the texture  
    /// Multiple sprites can use the same texture
    /// </summary>
    public Texture2D Texture
    {
      get => texture;
      set
      {
        texture = value;
        SourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
      }
    }
    /// <summary>
    /// In radians
    /// </summary>
    public float Rotation;
    /// <summary>
    /// In degree
    /// </summary>
    public float RotationAngle
    {
      get => (float)(Rotation * 180 / Math.PI);
      set => Rotation = (float)(value * Math.PI / 180);
    }
    /// <summary>
    /// Origin of rotation in pixels
    /// </summary>
    public Vector2 Origin;
    /// <summary>
    /// Origin of rotation in [0..1] of texture size
    /// </summary>
    public Vector2 RelativeOrigin
    {
      set
      {
        if (Texture == null) return;
        Origin = new Vector2(value.X * Texture.Width, value.Y * Texture.Height);
      }
    }
    private Vector2 offset;
    /// <summary>
    /// Draw offset from CUIComponent Position  
    /// For your convenience also sets origin
    /// </summary>
    public Vector2 Offset
    {
      get => offset;
      set
      {
        offset = value;
        RelativeOrigin = value;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj is not CUISprite b) return false;

      return Texture == b.Texture &&
        SourceRect == b.SourceRect &&
        DrawMode == b.DrawMode &&
        Effects == b.Effects &&
        Rotation == b.Rotation &&
        Origin == b.Origin &&
        Offset == b.Offset;
    }

    /// <summary>
    /// Creates a CUISprite from vanilla Sprite
    /// </summary>
    public static CUISprite FromVanilla(Sprite sprite)
    {
      if (sprite == null) return Default;

      return new CUISprite(sprite.Texture, sprite.SourceRect)
      {
        Path = sprite.FullPath,
      };
    }

    /// <summary>
    /// Uses vanilla GUI sprite from GUIStyle.ComponentStyles with this name
    /// </summary>
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

    //TODO add using construction
    /// <summary>
    /// When you load sprite from file, relative paths are considered relative to barotrauma folder  
    /// if BaseFolder != null sprite will check files in BaseFolder first
    /// Don't forget to set it back to null
    /// </summary>
    public static string BaseFolder { get; set; }

    /// <summary>
    /// Default 1x1 white sprite
    /// </summary>
    public CUISprite()
    {
      Texture = BackupTexture;
      SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
    }
    public CUISprite(string path, Rectangle? sourceRect = null, string baseFolder = null)
    {
      baseFolder ??= BaseFolder;
      string realpath = path;

      if (!System.IO.Path.IsPathRooted(path) && baseFolder != null)
      {
        string localPath = System.IO.Path.Combine(baseFolder, path);
        if (File.Exists(localPath)) realpath = localPath;
      }

      Path = path;
      Texture = CUI.TextureManager.GetTexture(realpath);
      if (sourceRect.HasValue) SourceRect = sourceRect.Value;
    }
    public CUISprite(Texture2D texture, Rectangle? sourceRect = null)
    {
      Texture = texture ?? BackupTexture;
      if (sourceRect.HasValue) SourceRect = sourceRect.Value;
    }

    public object Clone()
    {
      CUISprite sprite = new CUISprite(Texture, SourceRect)
      {
        Path = this.Path,
        Rotation = this.Rotation,
        Offset = this.Offset,
        Origin = this.Origin,
        Effects = this.Effects,
        DrawMode = this.DrawMode,
      };

      return sprite;
    }

    public override string ToString()
    {
      string mode = DrawMode != CUISpriteDrawMode.Resize ? $", Mode: {DrawMode}" : "";
      string rect = SourceRect != Texture.Bounds ? $", SourceRect: {CUIExtensions.RectangleToString(SourceRect)}" : "";
      string effect = Effects != SpriteEffects.None ? $", Effects: {CUIExtensions.SpriteEffectsToString(Effects)}" : "";

      string rotation = Rotation != 0.0f ? $", Rotation: {Rotation}" : "";
      string offset = Offset != Vector2.Zero ? $", Offset: {CUIExtensions.Vector2ToString(Offset)}" : "";
      string origin = Origin != Vector2.Zero ? $", Origin: {CUIExtensions.Vector2ToString(Origin)}" : "";

      return $"{{ Path: {Path}{mode}{rect}{effect}{rotation}{offset}{origin} }}";
    }

    //BUG it can't use absolute paths because of the c://
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

      if (props.ContainsKey("rotation"))
      {
        float r;
        float.TryParse(props["rotation"], out r);
        sprite.Rotation = r;
      }

      if (props.ContainsKey("offset"))
      {
        sprite.Offset = CUIExtensions.ParseVector2(props["offset"]);
      }

      if (props.ContainsKey("origin"))
      {
        sprite.Origin = CUIExtensions.ParseVector2(props["origin"]);
      }

      return sprite;
    }

    //TODO find less hacky solution
    public static CUISprite ParseWithContext(string raw, string baseFolder = null)
    {
      Dictionary<string, string> props = CUIExtensions.ParseKVPairs(raw);

      if (!props.ContainsKey("path")) return new CUISprite();

      if (!System.IO.Path.IsPathRooted(props["path"]) && baseFolder != null)
      {
        string localPath = System.IO.Path.Combine(baseFolder, props["path"]);

        if (File.Exists(localPath)) props["path"] = localPath;
      }

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

      if (props.ContainsKey("rotation"))
      {
        float r;
        float.TryParse(props["rotation"], out r);
        sprite.Rotation = r;
      }

      if (props.ContainsKey("offset"))
      {
        sprite.Offset = CUIExtensions.ParseVector2(props["offset"]);
      }

      if (props.ContainsKey("origin"))
      {
        sprite.Origin = CUIExtensions.ParseVector2(props["origin"]);
      }

      return sprite;
    }
  }
}