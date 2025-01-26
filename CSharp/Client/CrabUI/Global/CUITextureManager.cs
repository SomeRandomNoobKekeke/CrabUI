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
  /// Allowing you to load textures safely  
  /// With texture caching, BackupTexture, and automatic Dispose
  /// </summary>
  public class CUITextureManager : IDisposable
  {
    public static Texture2D BackupTexture => GUI.WhiteTexture;
    public Dictionary<string, Texture2D> LoadedTextures = new();
    public void DisposeAllTextures()
    {
      foreach (Texture2D texture in LoadedTextures.Values) texture.Dispose();
    }

    public string NormalizePath(string path)
    {
      return path; //TODO
    }

    public CUISprite GetSprite(string path)
    {
      return new CUISprite(GetTexture(path), path);
    }

    public Texture2D GetTexture(string path)
    {
      path = NormalizePath(path);

      if (LoadedTextures.ContainsKey(path)) return LoadedTextures[path];

      try
      {
        if (!File.Exists(path))
        {
          CUI.Error($"{path} texture file not found");
          return BackupTexture;
        }

        Texture2D loaded = null;

        using (FileStream fs = File.OpenRead(path))
        {
          loaded = Texture2D.FromStream(GameMain.Instance.GraphicsDevice, fs);
        }

        if (loaded == null)
        {
          CUI.Error($"{path} loaded texture is null");
          return BackupTexture;
        }

        LoadedTextures[path] = loaded;
        return loaded;
      }
      catch (Exception e)
      {
        CUI.Error(e);
      }

      return BackupTexture;
    }

    public void Dispose()
    {
      DisposeAllTextures();

      LoadedTextures.Clear();
      LoadedTextures = null;
    }
  }

}