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
  /// <summary>
  /// Class for loading textures without duplicates  
  /// Also disposes all loaded textures automatically
  /// </summary>
  public class CUITextureManager : IDisposable
  {
    public static void CreateCheckers()
    {
      int w = 8;
      int h = 8;
      Checkers = new Texture2D(GameMain.Instance.GraphicsDevice, w, h);

      Color[] data = new Color[w * h];

      for (int y = 0; y < h; y++)
      {
        for (int x = 0; x < w; x++)
        {
          data[y * w + x] = (x < w / 2) ^ (y < h / 2) ? Color.White : Color.Black;
        }
      }

      Checkers.SetData(0, new Rectangle(0, 0, w, h), data, 0, w * h);
    }
    public static void InitStatic()
    {
      CreateCheckers();

      CUI.OnDispose += () =>
      {
        Checkers.Dispose();
        Checkers = null;
      };
    }

    // private static Texture2D backupTexture;
    // public static Texture2D BackupTexture
    // {
    //   get
    //   {
    //     if (backupTexture == null)
    //     {
    //       backupTexture = new Texture2D(GameMain.Instance.GraphicsDevice, 1, 1);
    //       backupTexture.SetData(new Color[] { Color.White });
    //     }
    //     return backupTexture;
    //   }
    // }

    /// <summary>
    /// Path to additional PNGs, it can be set in CUI
    /// </summary>
    /// 
    public static Texture2D Checkers;
    public static Texture2D BackupTexture => GUI.WhiteTexture;
    public Dictionary<string, Texture2D> LoadedTextures = new();
    public void DisposeAllTextures()
    {
      foreach (Texture2D texture in LoadedTextures.Values)
      {
        if (texture == BackupTexture) continue;
        texture.Dispose();
      }
    }

    public string NormalizePath(string path)
    {
      return path; //TODO
    }

    public CUISprite GetSprite(string path, Rectangle? sourceRect = null, CUISpriteDrawMode? drawMode = null, SpriteEffects? effects = null)
    {
      CUISprite sprite = new CUISprite(GetTexture(path))
      {
        Path = path,
      };

      if (sourceRect.HasValue) sprite.SourceRect = sourceRect.Value;
      if (drawMode.HasValue) sprite.DrawMode = drawMode.Value;
      if (effects.HasValue) sprite.Effects = effects.Value;

      return sprite;
    }

    /// <summary>
    /// 32x32 square on (x,y) position from CUI.png
    /// </summary>
    public CUISprite GetCUISprite(int x, int y, CUISpriteDrawMode? drawMode = null, SpriteEffects? effects = null)
    {
      return GetSprite(CUI.CUITexturePath, new Rectangle(x * 32, y * 32, 32, 32), drawMode, effects);
    }

    private Texture2D TryLoad(string path)
    {
      Texture2D texture = null;
      try
      {
        if (File.Exists(path))
        {
          using (FileStream fs = File.OpenRead(path))
          {
            texture = Texture2D.FromStream(GameMain.Instance.GraphicsDevice, fs);
          }
        }
      }
      catch { }
      return texture;
    }

    public Texture2D GetTexture(string path)
    {
      if (LoadedTextures == null)
      {
        CUI.Error($"LoadedTextures was null");
        return BackupTexture;
      }

      if (LoadedTextures.ContainsKey(path)) return LoadedTextures[path];

      Texture2D loaded = null;

      if (CUI.AssetsPath != null) loaded ??= TryLoad(Path.Combine(CUI.AssetsPath, path));
      if (CUI.PGNAssets != null) loaded ??= TryLoad(Path.Combine(CUI.PGNAssets, path));
      loaded ??= TryLoad(path);
      if (loaded == null)
      {
        CUI.Warning($"Coudn't find {path} texture, setting it to backup texture");
        loaded ??= BackupTexture;
        if (CUI.PGNAssets == null)
        {
          CUI.Warning($"Note: It was loaded before CUI.PGNAssets was set");
        }
      }

      LoadedTextures[path] = loaded;
      return loaded;
    }

    public void Dispose()
    {
      DisposeAllTextures();

      LoadedTextures.Clear();
      LoadedTextures = null;
    }
  }

}