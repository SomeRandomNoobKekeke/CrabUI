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
using System.IO;

namespace CrabUIUser
{
  public class CUITest
  {
    public SnapshotTestManager SnapshotTestManager { get; } = new();
    public SnapshotConsoleInterface SnapshotConsoleInterface { get; } = new();
    public SnapshotGUI SnapshotGUI { get; } = new();

    public void Init()
    {
      SnapshotConsoleInterface.Init();
      SnapshotConsoleInterface.AttachTo(SnapshotTestManager);

      SnapshotGUI.Init();
      SnapshotGUI.AttachTo(SnapshotTestManager);

      SnapshotTestManager.SnaphotsFolder = Path.Combine(ModInfo.Dir, "Test Data", "Snapshots");
      SnapshotTestManager.Add(typeof(SnapshotTests));
      SnapshotTestManager.Init();
    }
  }
}