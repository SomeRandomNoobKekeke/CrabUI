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
    public class AssemblyPackageDirLookup : IDisposable
    {
      private Dictionary<Assembly, string> Cache { get; } = new();

      public string GetPackageDir(Assembly assembly)
      {
        if (!Cache.ContainsKey(assembly))
        {
          ContentPackage package = ModInfo.GetPackageForAssembly(assembly);
          Cache[assembly] = package?.Dir ?? ""; // null == vanilla
        }

        return Cache[assembly];
      }

      public void Dispose() => Cache.Clear();
    }
  }
}