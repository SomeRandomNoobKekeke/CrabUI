using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CrabUI
{


  public class __CUITextureManager : IDisposable, CUITextureManager
  {
    public class Context_Part
    {
      public string? PackageDir { get; set; }
      public string? FileDir { get; set; }
    }
    public Context_Part Context { get; } = new();

    public CUITexture2D BackupTexture => __CUITexture2D.White;
    public Dictionary<string, CUITexture2D> LoadedTextures { get; } = new();

    private static int MaxID;
    public CUIRenderTarget2D CreateNewRenderTarget(int width, int height, string key = null)
    {
      CUIRenderTarget2D texture = new __CUIRenderTarget2D(width, height);
      key ??= $"__{MaxID++}";
      Add(texture, key);

      return texture;
    }

    public CUITexture2D CreateNew(int width, int height, string key = null)
    {
      CUITexture2D texture = new __CUITexture2D(width, height);
      key ??= $"__{MaxID++}";
      Add(texture, key);
      return texture;
    }

    public CUITexture2D CreateNew(int width, int height, bool mipmap, SurfaceFormat format, string key = null)
    {
      CUITexture2D texture = new __CUITexture2D(width, height, mipmap, format);
      key ??= $"__{MaxID++}";
      Add(texture, key);
      return texture;
    }

    public CUITexture2D Add(CUITexture2D texture, string key)
    {
      key ??= $"__{MaxID++}";
      texture.Key = key;

      if (LoadedTextures.ContainsKey(key))
      {
        LoadedTextures[key].Dispose();
      }

      return LoadedTextures[key] = texture;
    }


    public bool Has(string key) => LoadedTextures.ContainsKey(key);

    private bool IsTexturePath(string path)
      => path.EndsWith(".bmp") || path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".png");
    public CUITexture2D Get(string key)
    {
      if (LoadedTextures.ContainsKey(key)) return LoadedTextures[key];
      if (IsTexturePath(key)) return LoadAs(key, key);
      return BackupTexture;
    }

    public CUITexture2D Reload(string key)
    {
      if (Has(key)) Forget(key);
      return LoadAs(key, key);
    }

    public CUITexture2D LoadAs(string path, string key)
    {
      CUITexture2D texture = _LoadFrom(path);

      // CUI.Logger.LogVars(key, Context.PackageDir, Context.FileDir);

      if (texture is null)
      {
        CUI.Logger.Warning($"Failed to load CUITexture from [{path}]");
        return BackupTexture;
      }

      return Add(texture, key);
    }

    private CUITexture2D _LoadFrom(string path)
    {
      CUITexture2D? texture;

      if (Path.IsPathFullyQualified(path))
      {
        texture = TryLoadFrom(path);
        if (texture is not null) return texture;
      }

      if (Context.FileDir != null)
      {
        texture = TryLoadFrom(Path.Combine(Context.FileDir, path));
        if (texture is not null) return texture;
      }

      if (Context.PackageDir != null)
      {
        texture = TryLoadFrom(Path.Combine(Context.PackageDir, path));
        if (texture is not null) return texture;
      }

      // Relative to game folder?
      texture = TryLoadFrom(path);

      return texture;
    }

    private CUITexture2D TryLoadFrom(string path)
    {
      if (!File.Exists(path)) return null;

      using (FileStream fs = File.OpenRead(path))
      {
        return new __CUITexture2D(
          Texture2D.FromStream(GameMain.Instance.GraphicsDevice, fs)
        );
      }
    }

    public void Forget(string key)
    {
      if (key is not null && LoadedTextures.ContainsKey(key))
      {
        LoadedTextures.Remove(key);
      }
    }

    public void Clear()
    {
      foreach (var (key, texture) in LoadedTextures)
      {
        texture.Dispose();
      }
      LoadedTextures.Clear();
    }

    public void Dispose() => Clear();
  }
}