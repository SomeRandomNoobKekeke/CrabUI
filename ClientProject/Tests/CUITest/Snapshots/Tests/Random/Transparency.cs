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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent Transparency()
      {
        CUIComponent frame = new CUIDefault.Frame("Transparency", 400, 600)
        {
          Background = { Sprite =
            {
              Texture = CUICore.TextureManager.Get("Test Chamber Background"),
              DrawMode = CUISpriteDrawMode.StaticAbove,
            }
          }
        };

        frame.Background.Animations.Transparency.Duration = 1;

        frame.Background.Animations.Transparency.Changed += (l) =>
        {
          frame.Get<CUIToggleButton>("toggle transparency").MasterColor = Color.Cyan.To(Color.Purple, l);
        };


        frame["toggle transparency"] = new CUIToggleButton("Transparency")
        {
          Anchor = CUIAnchor.Center,
          OnToggle = (state) => frame.DeepTransparency = state ? 1 : 0,
          TransparencyImmunity = true,
          MasterColor = Color.Cyan,
        };

        frame.Get<CUIDefault.FrameHandle>("layout.handle").Background.Animations.Transparency.Duration = 1;

        frame.DeepPalette = CUIPalette.FromColor(Color.Green);
        frame.Background.Color = Color.Green;

        return frame;
      }
    }
  }
}