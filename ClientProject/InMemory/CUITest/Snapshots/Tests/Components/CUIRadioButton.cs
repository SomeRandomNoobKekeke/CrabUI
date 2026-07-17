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
    public static partial class Components
    {
      public static CUIComponent CUIRadioButton()
      {
        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
        };

        frame["A"] = new CUIRadioButton("Option A")
        {
          Anchor = CUIAnchor.LeftCenter,
          Group = "bruh",
        };

        frame["B"] = new CUIRadioButton("Option B")
        {
          Anchor = CUIAnchor.Center,
          Group = "bruh",
        };

        frame["C"] = new CUIRadioButton("Option C")
        {
          Anchor = CUIAnchor.RightCenter,
          Group = "bruh",
        };

        return frame;
      }
    }
  }
}