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
    public class AssemblyPackageLookup : IDisposable
    {
      private Dictionary<Assembly, ContentPackage> Cache { get; } = new();

      public ContentPackage GetPackage(Assembly assembly)
      {
        if (!Cache.ContainsKey(assembly))
        {
          Cache[assembly] = ModInfo.GetPackageForAssembly(assembly);
        }

        return Cache[assembly];
      }

      public void Dispose() => Cache.Clear();
    }
  }
}