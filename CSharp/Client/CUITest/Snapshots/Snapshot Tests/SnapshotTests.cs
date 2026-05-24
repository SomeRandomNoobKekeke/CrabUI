using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    // public static string TempFolder => Mod.Instance.CUITest.SnaphotsTempFolder;

    public static VirtualFileSystem VirtualFileSystem => Mod.Instance.CUITest.VirtualFileSystem;
  }
}