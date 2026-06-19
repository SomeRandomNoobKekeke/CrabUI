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
    public Dictionary<string, CUITexture2D> LoadedTextures
      => TextureManager.LoadedTextures;

    public CUITexture2D Add(CUITexture2D texture, string path)
      => TextureManager.Add(texture, PathManager.Normalize(path));

    public void Clear()
      => TextureManager.Clear();

    public void Dispose()
      => TextureManager.Dispose();

    public void Forget(string path)
      => TextureManager.Forget(PathManager.Normalize(path));

    public CUITexture2D Get(string path)
      => TextureManager.Get(PathManager.Normalize(path));

    public bool Has(string path)
      => TextureManager.Has(PathManager.Normalize(path));

    public CUITexture2D Load(string path, string name = null)
      => TextureManager.Load(PathManager.Normalize(path), PathManager.Normalize(name));
  }
}