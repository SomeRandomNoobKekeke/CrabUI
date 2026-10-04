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

        frame["dark1"] = new CUIDefault.Frame("Dark dithered vignette")
        {
          DeepPalette = CUIPalette.FromColor(dark),
          Absolute = new CUINullRect(-250, -250, 500, 500),
        };

        frame["light1"] = new CUIDefault.Frame("Light dithered vignette")
        {
          DeepPalette = CUIPalette.FromColor(light),
          Absolute = new CUINullRect(250, -250, 500, 500),
        };

        frame["dark2"] = new CUIDefault.Frame("Dark vignette")
        {
          DeepPalette = CUIPalette.FromColor(dark),
          Background = { Sprite = CUISprite.Vignette },
          Absolute = new CUINullRect(-250, 250, 500, 500),
        };

        frame["light2"] = new CUIDefault.Frame("Light vignette")
        {
          DeepPalette = CUIPalette.FromColor(light),
          Background = { Sprite = CUISprite.Vignette },
          Absolute = new CUINullRect(250, 250, 500, 500),
        };

        return frame;
      }
    }
  }
}