using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using CUILibs;

namespace CursedUI
{

  public partial class SoloCUIRunner
  {
    /// <summary>
    /// This one is supposed to be used by user
    /// It tracks assembly that it was called from
    /// </summary>
    public class CUITextureManager_PublicPart : Part, CUITextureManager
    {
      public bool DummyMode { get; set; }

      public CUIRenderTarget2D CreateNewRenderTarget(int width, int height, string key = null)
        => Self.TextureManager.CreateNewRenderTarget(width, height, key);

      public CUITexture2D CreateNew(int width, int height, string key = null)
        => Self.TextureManager.CreateNew(width, height, key);

      public CUITexture2D CreateNew(int width, int height, bool mipmap, SurfaceFormat format, string key = null)
        => Self.TextureManager.CreateNew(width, height, mipmap, format, key);

      public CUITexture2D Add(string key, CUITexture2D texture) => Self.TextureManager.Add(key, texture);
      public void Clear() => Self.TextureManager.Clear();
      public void Dispose() => Self.TextureManager.Dispose();
      public void Forget(string key) => Self.TextureManager.Forget(key);
      public bool Has(string key) => Self.TextureManager.Has(key);

      public CUITexture2D Get(string key)
      {
        Self.TextureManager.Context.PackageDir = Self.DirLookup.GetPackageDir(Assembly.GetCallingAssembly());

        CUITexture2D texture = Self.TextureManager.Get(key);

        Self.TextureManager.Context.PackageDir = null;
        return texture;
      }

      public CUITexture2D LoadAs(string path, string key)
      {
        Self.TextureManager.Context.PackageDir = Self.DirLookup.GetPackageDir(Assembly.GetCallingAssembly());

        CUITexture2D texture = Self.TextureManager.LoadAs(path, key);

        Self.TextureManager.Context.PackageDir = null;
        return texture;
      }

      public CUITexture2D Reload(string key)
      {
        Self.TextureManager.Context.PackageDir = Self.DirLookup.GetPackageDir(Assembly.GetCallingAssembly());

        CUITexture2D texture = Self.TextureManager.Reload(key);

        Self.TextureManager.Context.PackageDir = null;
        return texture;
      }


    }
  }
}