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
      public static CUIComponent CUIMultiSprite()
      {
        CUIComponent frame = new CUIDefault.Frame("CUIMultiSprite", 400, 600);

        CUIMultiSprite sprite = new CUIMultiSprite()
        {
          CUISprite.BaroDev,
          CUISprite.Window,
        };

        frame["sprite"] = new CUIComponent()
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(w: 200, h: 200),
          Background ={
            Sprite = sprite
          },
          OnMouseDown = (e) => sprite.CurrentSource++,
        };



        return frame;
      }
    }
  }
}