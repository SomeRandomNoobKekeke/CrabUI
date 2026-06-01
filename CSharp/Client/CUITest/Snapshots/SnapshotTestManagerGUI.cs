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
  public class SnapshotTestManagerGUI : CUIPage
  {
    public SnapshotTestManagerGUI()
    {
      this["burh"] = new CUITextBlock()
      {
        Text = "bruh",
        Absolute = new CUINullRect(0, 0, 100, 20),
      };
    }
  }
}