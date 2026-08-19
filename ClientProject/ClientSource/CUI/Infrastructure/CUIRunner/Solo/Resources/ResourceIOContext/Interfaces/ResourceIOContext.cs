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
  /// <summary>
  /// This is a shared context for all resource loaders like texture loaders, xml loaders
  /// </summary>
  public interface ResourceIOContext
  {
    public string? PackageDir { set; }
    public string? FileDir { set; }
  }
}