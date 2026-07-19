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
    public CUITexture2D BackupTexture => __CUITexture2D.White;
    public Dictionary<string, CUITexture2D> LoadedTextures { get; } = new();

    public CUITexture2D Add(CUITexture2D texture, string key)
    {
      if (LoadedTextures.ContainsKey(key))
      {
        LoadedTextures[key].Dispose();
      }

      texture.Key = key;

      return LoadedTextures[key] = texture;
    }

    public CUITexture2D Load(string path, string key = null)
    {
      if (!File.Exists(path)) return BackupTexture;

      key ??= path;

      if (LoadedTextures.ContainsKey(key))
      {
        return LoadedTextures[key];
      }

      using (FileStream fs = File.OpenRead(path))
      {
        return LoadedTextures[key] = new __CUITexture2D(
          Texture2D.FromStream(GameMain.Instance.GraphicsDevice, fs)
        )
        {
          Key = key,
        };
      }
    }

    public CUITexture2D GetByPath(string path) => Get(path); //Same thing
    public CUITexture2D Get(string key)
    {
      if (LoadedTextures.ContainsKey(key)) return LoadedTextures[key];
      return BackupTexture;
    }

    public bool Has(string key) => LoadedTextures.ContainsKey(key);

    public void Forget(string key)
    {
      LoadedTextures[key].Dispose();
      LoadedTextures.Remove(key);
    }

    public void Clear()
    {
      foreach (CUITexture2D texture in LoadedTextures.Values)
      {
        texture.Dispose();
      }
      LoadedTextures.Clear();
    }

    public void Dispose() => Clear();
  }
}