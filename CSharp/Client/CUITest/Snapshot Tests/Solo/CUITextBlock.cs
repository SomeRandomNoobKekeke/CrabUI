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
  public partial class SnapshotTests
  {
    public partial class Solo
    {
      public static CUIComponent CUITextBlock()
      {
        return new CUITextBlock()
        {
          Anchor = CUIAnchor.Center,
          TextAnchor = CUIAnchor.Center,
          Absolute = new CUINullRect(0, 0, 200, 100),
          BackgroundColor = Color.Blue,
          Text = "bruh",
          Scale = 2,
          Draggable = true,
        };
      }
    }
  }
}