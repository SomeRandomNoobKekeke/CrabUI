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

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Components
    {
      public static CUIComponent CUIToggleButton()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIToggleButton", 400, 600);

        frame["button"] = new CUIToggleButton("bruh")
        {
          Anchor = CUIAnchor.Center,
        };

        return frame;
      }
    }
  }
}