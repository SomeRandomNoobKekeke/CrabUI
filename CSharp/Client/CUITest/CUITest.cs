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
  public class CUITest
  {
    public SnapshotTestManager SnapshotTestManager { get; } = new();

    public void Init()
    {
      SnapshotTestManager.Add(typeof(SnapshotTests));
      SnapshotTestManager.Init();
    }
  }
}