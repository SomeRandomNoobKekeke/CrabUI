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

namespace CrabUI
{

  public partial class SoloCUIRunner
  {
    /// <summary>
    /// This one is supposed to be used by CUICore
    /// It must set caller info before trying to get the textures, it's very hacky
    /// </summary>
    public class CUITextureManager_InternalPart : CUITextureManagerInternal
    {
      public SoloCUIRunner Self { get; set; }
      public string LoadedFileDir
      {
        set => Self.TextureManager.Context.LoadedFileDir = value;
      }

      public Assembly CallingAssembly
      {
        set
        {
          Self.TextureManager.Context.LoaderPackageDir = Self.DirLookup.GetPackageDir(value);
        }
      }

      public CUITexture2D Add(CUITexture2D texture, string key) => Self.TextureManager.Add(texture, key);
      public void Clear() => Self.TextureManager.Clear();
      public void Dispose() => Self.TextureManager.Dispose();
      public void Forget(string key) => Self.TextureManager.Forget(key);
      public bool Has(string key) => Self.TextureManager.Has(key);
      public CUITexture2D Get(string key) => Self.TextureManager.Get(key);
      public CUITexture2D Reload(string key) => Self.TextureManager.Reload(key);
      public CUITexture2D LoadAs(string path, string key) => Self.TextureManager.LoadAs(path, key);
    }
  }
}