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
      public static CUIComponent CUIDropDown()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "CUIDropDown" },
          Absolute = new CUINullRect(w: 400, h: 600),
        };

        frame["dropbdown"] = new CUIDropDown()
        {
          Anchor = CUIAnchor.Center,
          Selected = "bruh",
          Options = new string[]{
            "lol","123424234fqwef"
          }
        };

        return frame;
      }
    }
  }
}