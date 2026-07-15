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
    public static partial class Components
    {
      public static CUIComponent CUIScrollBar()
      {
        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Absolute = new CUINullRect(0, 0, 400, 600),
          Anchor = CUIAnchor.Center,
        };

        frame["scrollbar"] = new CUIScrollBar()
        {
          Relative = new CUINullRect(0, 0, 0.6f, 0.6f),
          Anchor = CUIAnchor.Center,
          Background = { Color = Color.Pink },
          OnMoved = (l) => CUI.Logger.Log(l),
        };

        return frame;
      }
    }
  }
}