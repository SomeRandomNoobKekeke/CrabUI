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
  public partial class SoloCUIRunner
  {
    public class ResourceIOContextHandle_Part : ResourceIOContextHandle
    {
      public SoloCUIRunner Self { get; set; }
      public Assembly CallingAssembly
      {
        set
        {
          Self.ResourceIOContext.PackageDir = value is null ?
            "" :
            Self.DirLookup.GetPackageDir(value);
        }
      }
      public string? FileDir
      {
        set
        {
          Self.ResourceIOContext.FileDir = value;
        }
      }
    }
  }
}