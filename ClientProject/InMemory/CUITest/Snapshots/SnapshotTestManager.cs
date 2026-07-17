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
  public partial class SnapshotTestManager
  {
    public ILogger Logger => CUI.Logger;

    public static string SnaphotsFolder => CUITest.SnaphotsFolder;

    public ComponentSnapshot CurrentSnapshot { get; set; }
    public SnapshotTest CurrentTest { get; set; }



    public SnapshotTestManager()
    {
      ConsoleInteface = new ConsoleIntefaceClass(this);
      Runner.Chamber = Chamber;
      UI = new SnapshotPage(this);
    }

    public SnapshotTestChamber Chamber { get; } = new();
    public SnapshotTestRepo Repo { get; } = new();
    public SnapshotTestRunner Runner { get; } = new();
    public SnapshotPage UI { get; }
    public ConsoleIntefaceClass ConsoleInteface { get; }

    public ClearableEvent<Event> Events { get; } = new();

    public void RunAll()
    {
      foreach (string name in Repo.Tests.Keys)
      {
        Run(name);
      }
    }
    public void Run(string name)
    {
      if (!Repo.Tests.ContainsKey(name))
      {
        Logger.Warning($"Can't find snapshot test: [{name}]");
        return;
      }

      ModStorage.Set("CUITest", name);

      if (!Chamber.IsSetup) Chamber.Setup();

      CurrentTest = Repo.Tests[name];
      CurrentSnapshot = Runner.Run(CurrentTest);
      ComponentSnapshot stored = GetStoredSnapshot(CurrentTest);

      if (stored is null)
      {
        Accept(CurrentTest, CurrentSnapshot);
        Events.Raise(new Event("passed", CurrentTest));
        return;
      }

      if (ComponentSnapshot.AreEqual(stored, CurrentSnapshot))
      {
        Events.Raise(new Event("passed", CurrentTest));
      }
      else
      {
        Events.Raise(new Event("failed", CurrentTest, ComponentSnapshot.CreateDiffString(CurrentSnapshot, stored)));
      }
    }

    public void Setup() => Chamber.Setup();
    public void Dismantle() => Chamber.Dismantle();

    public void AcceptCurrent()
    {
      if (CurrentTest is null) return;
      Accept(CurrentTest, CurrentSnapshot);
      Events.Raise(new Event("passed", CurrentTest));
    }

    public void Accept(SnapshotTest test, ComponentSnapshot snapshot)
    {
      string savePath = Path.Combine(SnaphotsFolder, $"{test.Name}.xml");
      snapshot.Save(savePath);
    }

    private ComponentSnapshot GetStoredSnapshot(SnapshotTest test)
    {
      string savePath = Path.Combine(SnaphotsFolder, $"{test.Name}.xml");
      return ComponentSnapshot.LoadSnapshot(savePath);
    }
  }
}