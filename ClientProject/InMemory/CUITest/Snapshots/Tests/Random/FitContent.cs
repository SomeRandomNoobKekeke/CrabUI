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
      public static CUIComponent FitContent()
      {
        // Fit in plain layout
        CUIFrame frame = new CUIDefault.Frame("FitContent")
        {
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
        };

        frame["container1"] = new CUIComponent()
        {
          Absolute = new CUINullRect(x: 40, y: 40),
          Background = {
            Sprite = CUISprite.Vignette,
            Color = new Color(0, 64, 0)
          },
          FitContent = new CUIBool2(true, true),
        };

        frame["container1"]["a"] = new CUIComponent()
        {
          Background = { Color = Color.Yellow },
          Absolute = new CUINullRect(20, 20, 10, 10),
        };

        frame["container1"]["b"] = new CUIComponent()
        {
          Background = { Color = Color.Red },
          Absolute = new CUINullRect(100, 100, 10, 10),
        };


        // Fit to nested
        frame["container2"] = new CUIComponent()
        {
          Absolute = new CUINullRect(x: 40, y: 200, h: 50),
          Background = { Sprite = CUISprite.Vignette, Color = new Color(0, 0, 64) },
          FitContent = new CUIBool2(true, false),
        };

        frame["container2"]["a"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(64, 0, 64) },
          Anchor = CUIAnchor.CenterTop,
          ParentAnchor = CUIAnchor.CenterBottom,
          FitContent = new CUIBool2(true, false),
          Absolute = new CUINullRect(h: 20),
        };

        frame["container2"]["a"]["b"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(64, 64, 0) },
          Anchor = CUIAnchor.CenterTop,
          ParentAnchor = CUIAnchor.CenterBottom,
          Absolute = new CUINullRect(w: 50, h: 20),
        };


        // Fit to text
        frame["container3"] = new CUIComponent()
        {
          Absolute = new CUINullRect(x: 150, y: 200, h: 50),
          Background = { Sprite = CUISprite.Vignette, Color = new Color(0, 0, 64) },
          FitContent = new CUIBool2(true, false),
        };

        frame["container3"]["a"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(64, 0, 64) },
          Anchor = CUIAnchor.CenterTop,
          ParentAnchor = CUIAnchor.CenterBottom,
          FitContent = new CUIBool2(true, false),
          Absolute = new CUINullRect(h: 20),
        };

        frame["container3"]["a"]["b"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(64, 64, 0) },
          Anchor = CUIAnchor.CenterTop,
          ParentAnchor = CUIAnchor.CenterBottom,
          FitContent = new CUIBool2(true, true),
        };

        frame["container3"]["a"]["b"]["text"] = new CUITextBlock("bruh")
        {
          Padding = new CUISizes(4, 10, 4, 10),
        };


        //fit in list
        frame["container4"] = new CUIComponent()
        {
          Absolute = new CUINullRect(x: 250, y: 200, h: 50),
          Background = { Sprite = CUISprite.Vignette, Color = new Color(0, 0, 64) },
          FitContent = new CUIBool2(true, false),
        };

        frame["container4"]["a"] = new CUIVerticalList()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(64, 0, 64) },
          Anchor = CUIAnchor.CenterTop,
          ParentAnchor = CUIAnchor.CenterBottom,
          FitContent = new CUIBool2(true, false),
          Absolute = new CUINullRect(h: 20),
          // CullChildren = false,
        };

        frame["container4"]["a"]["b"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(0, 255, 0) },
          Absolute = new CUINullRect(w: 30, h: 30)
        };

        frame["container4"]["a"]["c"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.Vignette, Color = new Color(64, 64, 0) },
          // FitContent = new CUIBool2(true, true),
          Absolute = new CUINullRect(w: 30, h: 30)
        };

        frame["container4"]["a"]["c"]["text"] = new CUITextBlock("bruh")
        {
          Padding = new CUISizes(4, 10, 4, 10),
        };




        return frame;
      }
    }
  }
}