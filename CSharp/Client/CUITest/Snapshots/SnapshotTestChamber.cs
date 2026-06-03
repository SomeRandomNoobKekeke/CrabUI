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
  public class SnapshotTestChamber : CUIComponent
  {
    public SnapshotTestChamber()
    {
      Relative = new CUINullRect(0, 0, 1, 1);
      BackgroundColor = new Color(64, 0, 64, 64);
    }

    public bool IsSetup => Parent != null;


    public void Setup()
    {
      if (!IsSetup) CUI.Main.Append(this);
    }

    public void Dismantle()
    {
      RemoveSelf();
    }
  }
}