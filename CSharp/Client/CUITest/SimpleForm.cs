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
    public static CUIComponent SimpleForm()
    {
      CUIComponent frame = new()
      {
        BackgroundColor = Color.Gray,
        Draggable = true,
        Absolute = new CUINullRect(300, 300, 400, 600),
      };

      frame.AddChild(new CUIComponent()
      {
        BackgroundColor = Color.Red,
        Absolute = new CUINullRect(0, 0, 100, 100),
      });

      return frame;
    }
  }
}