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

        frame["button"] = new CUIToggleButton("button")
        {
          Anchor = CUIAnchor.Center,
          OnState = { Text = "On", TextColor = Color.Black, BackgroundColor = Color.Orange },
          OffState = { Text = "Off34r234r234r234r23", BackgroundColor = Color.Green, BackgroundColorHovered = Color.Lime },
        };

        return frame;
      }
    }
  }
}