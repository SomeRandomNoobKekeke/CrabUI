using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;

namespace CrabUIUser
{
  public static partial class CUITestFactories
  {
    public static CUIComponent SimpleList()
    {
      CUIComponent frame = new()
      {
        BackgroundColor = Color.Gray,
        Draggable = true,
        Absolute = new CUINullRect(300, 300, 400, 600),
      };

      CUIVerticalList list = new CUIVerticalList()
      {
        BackgroundColor = Color.Blue,
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      frame.AddChild(list);

      list.AddChild(new CUIComponent()
      {
        BackgroundColor = Color.Red,
        Absolute = new CUINullRect(0, 0, 300, 100),
      });

      list.AddChild(new CUIComponent()
      {
        BackgroundColor = Color.Yellow,
        Absolute = new CUINullRect(30, 0, 350, 100),
      });

      list.AddChild(new CUIComponent()
      {
        BackgroundColor = Color.Green,
        Absolute = new CUINullRect(0, 0, 300, 100),
      });

      return frame;
    }
  }
}