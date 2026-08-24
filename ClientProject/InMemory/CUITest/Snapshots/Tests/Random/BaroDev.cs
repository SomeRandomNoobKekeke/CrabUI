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
          Background = {
            Sprite = new CUISprite(CUICore.TextureManager.Get("BaroDev"))
            {
              ColorTL = Color.Red,
              ColorTR = Color.Green,
              ColorBR = Color.Blue,
              ColorBL = Color.Transparent,
            },
          },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
          Resizable = true,
          UseReactiveStyles = false,
        };

        return frame;
      }
    }
  }
}