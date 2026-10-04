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
using System.IO;

namespace CursedUIUser
{
  public partial class TestManager
  {
    public SnapshotTestManager SnapshotTestManager { get; } = new();
    public E2ETestManager E2ETestManager { get; } = new();
    public UTestManager UTestManager { get; } = new();




    public void Init()
    {
      CreateUI();

      if (ModStorage.Has("CUITest.LastTest"))
      {
        IsOpen = true;

        var (type, name) = ModStorage.Get<(string, string)>("CUITest.LastTest");

        if (type == "snapshot")
        {
          Pages.Open(SnapshotTestManager);
          SnapshotTestManager.OpenGroupByTestName(name);
          SnapshotTestManager.Run(name);
        }
        if (type == "e2e")
        {
          Pages.Open(E2ETestManager);
          E2ETestManager.Run(name);
        }
      }
    }
  }
}