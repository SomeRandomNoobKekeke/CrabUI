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
  /// <summary>
  /// This is what CUICore has
  /// CUICore doesn't know how to resolve Assembly -> PackageDir
  /// </summary>
  public interface ResourceIOContextHandle
  {
    // public string? PackageDir { set; }
    public Assembly CallingAssembly { set; }
    public string? FileDir { set; }
  }
}