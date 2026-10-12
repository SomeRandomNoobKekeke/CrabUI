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
      //LINK:/ClientProject\ClientSource\CUI\Core\Animations\AnimatedValue.cs
      public static CUIComponent AnimatedValue()
      {
        CUIComponent frame = new CUIDefault.Frame("AnimatedValue", 600, 600)
        {
          Draggable = false,
        };




        frame["barodev"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.BaroDev },
          Absolute = new CUINullRect(w: 30, h: 30),
          Anchor = CUIAnchor.Center,
        };

        AnimatedValue<Vector2> pos = new(frame["barodev"].Absolute.Position);

        pos.Changed += (v) => frame["barodev"].Absolute = frame["barodev"].Absolute with
        {
          Position = v,
        };

        frame.MouseDown += (e) =>
        {
          pos.Target = e.Pos - frame.Rect.Center;
        };


        return frame;
      }
    }
  }
}