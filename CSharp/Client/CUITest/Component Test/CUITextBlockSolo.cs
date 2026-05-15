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
    public static CUIComponent CUITextBlockSolo()
    {
      return new CUITextBlock()
      {
        Anchor = CUIAnchor.Center,
        TextAnchor = CUIAnchor.Center,
        Absolute = new CUINullRect(0, 0, 200, 100),
        BackgroundColor = Color.Blue,
        Text = "bruh",
        Scale = 10,
        Draggable = true,
      };
    }
  }
}