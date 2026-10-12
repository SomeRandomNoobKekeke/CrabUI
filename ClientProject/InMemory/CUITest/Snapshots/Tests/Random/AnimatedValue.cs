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


        AnimatedValue<Vector2> pos = new(new Vector2(50, 50));

        frame["barodev"] = new CUIComponent()
        {
          Background = { Sprite = CUISprite.BaroDev },
          Absolute = new CUINullRect(x: 50, y: 50, w: 30, h: 30),
        };

        pos.Changed += (v) => frame["barodev"].Absolute = frame["barodev"].Absolute with
        {
          Position = v,
        };

        frame.MouseDown += (e) =>
        {
          pos.Target = e.Pos - frame.Rect.LeftTop - new Vector2(15, 15);
        };


        return frame;
      }
    }
  }
}