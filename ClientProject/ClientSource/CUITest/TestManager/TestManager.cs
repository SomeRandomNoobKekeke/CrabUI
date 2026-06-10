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
    public E2ETestManager E2ETestManager { get; } = new();



    public void Init()
    {


      if (ModStorage.Has("CUITest"))
      {
        IsOpen = true;

        string name = (string)ModStorage.Get("CUITest");

        if (SnapshotTestManager.Repo.Tests.ContainsKey(name))
        {
          Pages.Open(SnapshotTestManager.UI);
          SnapshotTestManager.Run(name);
        }

        if (E2ETestManager.Repo.Tests.ContainsKey(name))
        {
          Pages.Open(E2ETestManager.UI);
          E2ETestManager.Run(name);
        }
      }
      else
      {
        IsOpen = false;
      }
    }

    public TestManager()
    {
      CreateUI();
    }
  }
}