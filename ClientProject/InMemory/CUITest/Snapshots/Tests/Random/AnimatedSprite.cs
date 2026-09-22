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
      public static CUIComponent AnimatedSprite()
      {
        CUIComponent frame = new CUIDefault.Frame("AnimatedSprite", 400, 600);

        CUIAnimatedSprite sprite = CUIAnimatedSprite.FromSpriteAtlas(
          CUI.TextureManager.Get("Assets/PNG/For testing/CursorDefault.png"),
          frames: new Point(12, 1), frameSize: new Point(64, 64), gap: new Point(2, 0)
        );

        sprite.SecPerFrame = 1;
        CUI.Logger.Log(sprite.Animation.Duration);

        sprite.Animation.RunForward();

        frame["sprite"] = new CUIComponent()
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(w: 200, h: 200),
          Background = { Sprite = sprite },
          OnMouseDown = (e) => sprite.CurrentSource++,
        };



        return frame;
      }
    }
  }
}