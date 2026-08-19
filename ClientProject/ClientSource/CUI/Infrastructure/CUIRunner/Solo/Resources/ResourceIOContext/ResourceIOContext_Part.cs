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
    public class ResourceIOContext_Part : Part, ResourceIOContext
    {
      public string? PackageDir
      {
        set
        {
          Self.TextureManager.Context.PackageDir = value;
          Self.FilePathResolver.PackageDir = value;
        }
      }
      public string? FileDir
      {
        set
        {
          Self.TextureManager.Context.FileDir = value;
          Self.FilePathResolver.FileDir = value;
        }
      }
    }
  }
}