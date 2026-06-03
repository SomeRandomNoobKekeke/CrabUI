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
  public partial class TestManager
  {
    public SnapshotTestManager SnapshotTestManager { get; } = new();


    public void Init()
    {
      IsOpen = false;

      if (ModStorage.Has("CUITest"))
      {
        string name = (string)ModStorage.Get("CUITest");

        if (SnapshotTestManager.Repo.Tests.ContainsKey(name))
        {
          IsOpen = true;
          Pages.Open(SnapshotTestManager.UI);
          SnapshotTestManager.Run(name);
        }
      }
    }

    public TestManager()
    {
      CreateUI();
    }
  }
}