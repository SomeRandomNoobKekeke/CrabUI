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
    public static partial class Components
    {
      public static CUIComponent CUIToggleButton()
      {
        CUIFrame frame = new()
        {
          BackgroundColor = new Color(0, 0, 64),
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
        };

        frame["button"] = new CUIToggleButton("bruh")
        {
          Anchor = CUIAnchor.Center,
        };

        return frame;
      }
    }
  }
}