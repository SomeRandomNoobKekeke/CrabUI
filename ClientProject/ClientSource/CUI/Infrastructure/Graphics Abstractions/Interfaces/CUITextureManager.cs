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
  public interface CUITextureManager
  {
    Dictionary<string, CUITexture2D> LoadedTextures { get; }

    CUITexture2D Add(CUITexture2D texture, string path);
    void Clear();
    void Dispose();
    void Forget(string path);
    CUITexture2D Get(string path);
    bool Has(string path);
    CUITexture2D Load(string path, string name = null);
  }
}