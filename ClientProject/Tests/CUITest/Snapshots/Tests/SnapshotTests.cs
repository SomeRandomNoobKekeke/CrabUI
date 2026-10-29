using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CUITests
{
  public partial class SnapshotTests
  {
    // public static string TempFolder => Mod.Instance.CUITest.SnaphotsTempFolder;

    public static VirtualFileSystem VirtualFileSystem => CUITestPlugin.Instance.CUITest.VirtualFileSystem;
  }
}