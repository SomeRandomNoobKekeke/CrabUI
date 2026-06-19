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
  public class PathManager
  {
    public required string ModDir { get; set; }

    public string Normalize(string path)
      => Path.IsPathFullyQualified(path) ? path : Path.Combine(ModDir, path);
  }
}