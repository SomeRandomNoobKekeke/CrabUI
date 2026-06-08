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

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent MG()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = new Color(32, 32, 32),
          Absolute = new CUINullRect(0, 0, 200, 200),
          Anchor = CUIAnchor.Center,
        };

        CUIMagnifyingGlass mg = new CUIMagnifyingGlass()
        {
          Relative = new(0, 0, 1, 1),
          Size = new Point(40, 40),
        };

        frame["mg"] = mg;

        return frame;
      }
    }
  }
}