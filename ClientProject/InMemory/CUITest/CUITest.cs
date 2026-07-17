using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public class CUITest
  {
    public static Logger Logger { get; } = new()
    {
      PrintFilePath = false
    };

    public TestManager TestManager { get; } = new();

    public static string TestDataFolder => Path.Combine(ModInfo.Dir, "Test Data");
    public static string SnaphotsFolder => Path.Combine(TestDataFolder, "Snapshots");
    public static string SnaphotsTempFolder => Path.Combine(TestDataFolder, "Temp");

    public VirtualFileSystem VirtualFileSystem { get; } = new();

    public void Init()
    {
      TestManager.SnapshotTestManager.Repo.Add(typeof(SnapshotTests));
      TestManager.E2ETestManager.Repo.AddPack(typeof(E2ETestPack));

      TestManager.Init();
    }

    public void Dispose()
    {

    }
  }
}