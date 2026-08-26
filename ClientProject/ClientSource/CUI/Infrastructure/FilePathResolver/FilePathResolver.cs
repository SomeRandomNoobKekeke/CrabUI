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
  public class FilePathResolver
  {
    public string? PackageDir { get; set; }
    public string? FileDir { get; set; }

    public string FindBestMatchForSaving(string path)
    {
      if (Path.IsPathFullyQualified(path)) return path;

      if (PackageDir != null)
      {
        return Path.Combine(PackageDir, path);
      }

      return path;
    }

    public string FindBestMatchForLoading(string path)
    {
      if (Path.IsPathFullyQualified(path)) return path;

      if (FileDir != null)
      {
        if (File.Exists(Path.Combine(FileDir, path)))
        {
          return Path.Combine(FileDir, path);
        }
      }

      if (PackageDir != null)
      {
        if (File.Exists(Path.Combine(PackageDir, path)))
        {
          return Path.Combine(PackageDir, path);
        }
      }

      // Relative to game folder or idk
      return path;
    }
  }
}