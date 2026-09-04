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
      public static CUIComponent SpriteDrawModes()
      {
        CUIComponent wrapper = new CUIComponent() { Relative = new CUINullRect(0, 0, 1, 1) };

        wrapper["frame 1"] = new CUIDefault.Frame("Resize")
        {
          Background = {
            Sprite = new CUISprite(CUICore.TextureManager.Get("BaroDev")),
            DrawMode = CUISpriteDrawMode.Resize,
          },
          Absolute = new CUINullRect(-300, -150, 400, 200)
        };

        wrapper["frame 2"] = new CUIDefault.Frame("Wrap")
        {
          Background = {
            Sprite = new CUISprite(CUICore.TextureManager.Get("BaroDev")),
            SourceRectangle = new(-100,-100,0,0),
            DrawMode = CUISpriteDrawMode.Wrap,
          },
          Absolute = new CUINullRect(300, -150, 400, 200)
        };

        wrapper["frame 3"] = new CUIDefault.Frame("Static")
        {
          Background = {
            Sprite = new CUISprite(CUICore.TextureManager.Get("BaroDev")),
            DrawMode = CUISpriteDrawMode.Static,
          },
          Absolute = new CUINullRect(-300, 150, 400, 200)
        };

        wrapper["frame 4"] = new CUIDefault.Frame("StaticDeep")
        {
          Background = {
            Sprite = new CUISprite(CUICore.TextureManager.Get("BaroDev")),
            DrawMode = CUISpriteDrawMode.StaticDeep,
          },
          Absolute = new CUINullRect(300, 150, 400, 200)
        };

        return wrapper;
      }
    }
  }
}