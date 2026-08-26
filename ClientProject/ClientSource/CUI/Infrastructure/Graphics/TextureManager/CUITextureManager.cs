using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CursedUI
{
  public interface CUITextureManager
  {
    public bool DummyMode { get; set; }

    public CUIRenderTarget2D CreateNewRenderTarget(int width, int height, string key = null);
    public CUITexture2D CreateNew(int width, int height, string key = null);
    public CUITexture2D CreateNew(int width, int height, bool mipmap, SurfaceFormat format, string key = null);

    CUITexture2D Add(string key, CUITexture2D texture);
    void Clear();
    void Dispose();
    void Forget(string key);
    bool Has(string key);
    CUITexture2D Get(string key);
    public CUITexture2D Reload(string key);
    public CUITexture2D LoadAs(string path, string key);
  }
}