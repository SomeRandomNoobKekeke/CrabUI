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
  //CRINGE or not? i guess i need MasterRunner to see how scales
  public class CUITextureManagerProxy(
    __CUITextureManager TextureManager,
    PathManager PathManager
  ) : CUITextureManager
  {
    public Dictionary<string, CUITexture2D> LoadedTextures => TextureManager.LoadedTextures;

    public CUITexture2D Add(CUITexture2D texture, string key) => TextureManager.Add(texture, key);
    public void Clear() => TextureManager.Clear();
    public void Dispose() => TextureManager.Dispose();
    public void Forget(string key) => TextureManager.Forget(key);
    public CUITexture2D Get(string key) => TextureManager.Get(key);
    public bool Has(string key) => TextureManager.Has(key);

    public CUITexture2D Load(string path, string key)
      => TextureManager.Load(PathManager.Normalize(path), key);
  }
}