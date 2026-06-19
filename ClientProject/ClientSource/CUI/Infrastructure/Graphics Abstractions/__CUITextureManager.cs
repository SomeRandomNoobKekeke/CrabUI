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

    public CUITexture2D Add(CUITexture2D texture, string path)
    {
      return LoadedTextures[path] = texture;
    }
    public CUITexture2D Load(string path, string name = null)
    {
      if (!File.Exists(path)) return BackupTexture;

      using (FileStream fs = File.OpenRead(path))
      {
        return Add(new __CUITexture2D(
          Texture2D.FromStream(GameMain.Instance.GraphicsDevice, fs)
        ), name ?? path);
      }
    }

    public CUITexture2D Get(string path)
    {
      if (LoadedTextures.ContainsKey(path)) return LoadedTextures[path];
      return BackupTexture;
    }

    public bool Has(string path) => LoadedTextures.ContainsKey(path);

    public void Forget(string path)
    {
      LoadedTextures[path].Dispose();
      LoadedTextures.Remove(path);
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