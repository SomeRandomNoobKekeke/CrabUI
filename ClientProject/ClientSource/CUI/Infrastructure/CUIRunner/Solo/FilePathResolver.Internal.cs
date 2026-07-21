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
  public class FilePathResolver_InternalPart : FilePathResolverInternal
  {
    public SoloCUIRunner Self { get; set; }

    public string LoadedFileDir
    {
      set => Self.FilePathResolver.LoadedFileDir = value;
    }

    public Assembly CallingAssembly
    {
      set
      {
        Self.FilePathResolver.LoaderPackageDir = Self.DirLookup.GetPackageDir(value);
      }
    }

    public string Resolve(string path) => Self.FilePathResolver.Resolve(path);
  }
}