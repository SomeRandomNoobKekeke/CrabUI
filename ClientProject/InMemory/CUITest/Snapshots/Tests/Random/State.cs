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
    public static partial class Random
    {
      public static CUIComponent State()
      {
        CUIFrame frame = new()
        {
          Background = { Color = Color.Gray },
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 400, 600),
        };


        frame["save"] = new CUIButton()
        {
          Text = "Save",
          Absolute = new CUINullRect(w: 40, h: 20),
          Anchor = CUIAnchor.LeftCenter,
          OnMouseDown = (e) => frame.SaveState("bruh"),
        };

        frame["load"] = new CUIButton()
        {
          Text = "Load",
          Absolute = new CUINullRect(w: 40, h: 20),
          Anchor = CUIAnchor.RightCenter,
          OnMouseDown = (e) => frame.RestoreState("bruh"),
        };


        return frame;
      }
    }
  }
}