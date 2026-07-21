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
  public class FilePathResolver
  {
    public string? LoaderPackageDir { get; set; }
    public string? LoadedFileDir { get; set; }

    public string FindBestMatchForSaving(string path)
    {
      if (Path.IsPathFullyQualified(path)) return path;
    }

    public string FindBestMatchForLoading(string path)
    {
      if (Path.IsPathFullyQualified(path)) return path;

      if (LoadedFileDir != null)
      {
        if (File.Exists(Path.Combine(LoadedFileDir, path)))
        {
          return Path.Combine(LoadedFileDir, path);
        }
      }

      if (LoaderPackageDir != null)
      {
        if (File.Exists(Path.Combine(LoaderPackageDir, path)))
        {
          return Path.Combine(LoaderPackageDir, path);
        }
      }

      // Relative to game folder or idk
      return path;
    }
  }
}