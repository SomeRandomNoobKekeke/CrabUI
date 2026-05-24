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

    public string TestDataFolder => Path.Combine(ModInfo.Dir, "Test Data");
    public string SnaphotsFolder => Path.Combine(TestDataFolder, "Snapshots");
    public string SnaphotsTempFolder => Path.Combine(TestDataFolder, "Temp");

    public void Init()
    {
      SnapshotConsoleInterface.Init();
      SnapshotConsoleInterface.AttachTo(SnapshotTestManager);

      SnapshotTestManager.SnaphotsFolder = SnaphotsFolder;
      SnapshotTestManager.Add(typeof(SnapshotTests));
      SnapshotTestManager.Init();

      SnapshotGUI.AttachTo(SnapshotTestManager);
      SnapshotGUI.Init();

    }
  }
}