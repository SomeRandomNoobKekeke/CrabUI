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
    public class CUITextureManagerProxy(
        __CUITextureManager TextureManager,
        AssemblyPackageLookup assemblyPackageLookup
      ) : CUITextureManager
    {
      AssemblyPackageLookup lookup = assemblyPackageLookup;

      //supported formats bmp, gif, jpg, png, tif and dds (only for simple textures).
      public bool IsRelTexturePath(string path)
      {
        if (
          !path.EndsWith(".bmp") && !path.EndsWith(".gif") && !path.EndsWith(".jpg") && !path.EndsWith(".png")
        ) return false;

        return !Path.IsPathFullyQualified(path);
      }

      public Dictionary<string, CUITexture2D> LoadedTextures => TextureManager.LoadedTextures;

      public CUITexture2D Add(CUITexture2D texture, string key) => TextureManager.Add(texture, key);
      public void Clear() => TextureManager.Clear();
      public void Dispose() => TextureManager.Dispose();
      public void Forget(string key) => TextureManager.Forget(key);
      public CUITexture2D Get(string key) => TextureManager.Get(key);
      public CUITexture2D GetByPath(string path)
      {
        string callerRoot = lookup.GetPackage(Assembly.GetCallingAssembly()).Dir;
        return TextureManager.Get(Path.Combine(callerRoot, path));
      }
      public bool Has(string key)
      {
        if (IsRelTexturePath(key))
        {
          string callerRoot = lookup.GetPackage(Assembly.GetCallingAssembly()).Dir;
          return TextureManager.Has(Path.Combine(callerRoot, key));
        }
        else
        {
          return TextureManager.Has(key);
        }
      }

      public CUITexture2D Load(string path, string key)
      {
        if (IsRelTexturePath(path))
        {
          string callerRoot = lookup.GetPackage(Assembly.GetCallingAssembly()).Dir;
          return TextureManager.Load(Path.Combine(callerRoot, path), key);
        }
        else
        {
          return TextureManager.Load(path, key);
        }
      }
    }
  }
}