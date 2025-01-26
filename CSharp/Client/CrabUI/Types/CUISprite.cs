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
  public class CUISprite
  {
    public static Texture2D BackupTexture => GUI.WhiteTexture;
    private string path = ""; public string Path
    {
      get => path;
      set
      {
        path = value;
        Texture = CUI.TextureManager.GetTexture(value);
      }
    }

    public Texture2D Texture { get; set; }
    public CUISpriteDrawMode DrawMode { get; set; } = CUISpriteDrawMode.Resize;

    public Rectangle? SourceRect { get; set; }

    public CUISprite()
    {
      Texture = BackupTexture;
      this.path = "";
    }
    public CUISprite(string path)
    {
      this.path = path;
      Texture = CUI.TextureManager.GetTexture(path);
    }
    public CUISprite(Texture2D texture, string path)
    {
      Texture = texture ?? BackupTexture;
      this.path = path ?? "";
    }

    public override string ToString() => Path;
    public static CUISprite Parse(string path) => CUI.TextureManager.GetSprite(path);
  }
}