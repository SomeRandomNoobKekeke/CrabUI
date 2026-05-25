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
      public static CUIComponent BaroDev()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = Color.White,
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Resizable = true,
          BackgroundTexture = CUICore.GetTexture("Assets/dev.png"),
        };

        return frame;
      }
    }
  }
}