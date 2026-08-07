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
  public partial class TestManager
  {
    public SnapshotTestManager SnapshotTestManager { get; } = new();
    public E2ETestManager E2ETestManager { get; } = new();



    public void Init()
    {
      CreateUI();

      if (ModStorage.Has("CUITest"))
      {
        IsOpen = true;

        var (type, name) = ((string, string))ModStorage.Get("CUITest");

        if (type == "snapshot")
        {
          Pages.Open(SnapshotTestManager);
          SnapshotTestManager.Run(name);
        }

        if (type == "e2e")
        {
          Pages.Open(E2ETestManager);
          E2ETestManager.Run(name);
        }
      }
      else
      {
        IsOpen = false;
      }
    }
  }
}