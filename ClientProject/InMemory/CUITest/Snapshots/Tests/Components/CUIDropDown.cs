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
      public static CUIComponent CUIDropDown()
      {
        var frame = new CUIDefault.Frame("CUIDropDown", 400, 600);

        frame["dropdown"] = new CUIDropDown()
        {
          Anchor = CUIAnchor.Center,
          Selected = "bruh",
          Options = ["lol", "123424234fqwef"],
          OnSelect = (s) => frame.Caption = s,
        };

        return frame;
      }
    }
  }
}