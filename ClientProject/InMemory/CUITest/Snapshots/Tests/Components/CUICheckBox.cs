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
      public static CUIComponent CUICheckBox()
      {
        var frame = new CUIDefault.Frame("CUICheckBox", 400, 600);

        frame["checkbox"] = new CUICheckBox()
        {
          Anchor = CUIAnchor.Center,
          OnToggle = (state) => frame.Caption = $"Checked: [{state}]",
        };

        return frame;
      }
    }
  }
}