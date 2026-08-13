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
      public static CUIComponent ColorBanding()
      {
        CUIFrame frame = new()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Draggable = false,
          Resizable = false,
        };

        Color dark = new Color(48, 48, 48);
        Color light = Color.Cyan;

        frame["dark"] = new CUIDefault.Frame("Dark")
        {
          DeepPalette = CUIPalette.FromColor(dark),
          Absolute = new CUINullRect(w: 600, h: 600),
          Anchor = CUIAnchor.RightCenter,
          ParentAnchor = CUIAnchor.Center,
        };

        frame["light"] = new CUIDefault.Frame("Light")
        {
          DeepPalette = CUIPalette.FromColor(light),
          Absolute = new CUINullRect(w: 600, h: 600),
          Anchor = CUIAnchor.LeftCenter,
          ParentAnchor = CUIAnchor.Center,
        };

        return frame;
      }
    }
  }
}