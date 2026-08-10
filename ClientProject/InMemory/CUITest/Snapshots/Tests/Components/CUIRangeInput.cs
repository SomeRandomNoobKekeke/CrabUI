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
      public static CUIComponent CUIRangeInput()
      {
        CUIFrame frame = new CUIDefault.Frame()
        {
          Caption = { Text = "CUIRangeInput" },
          Absolute = new CUINullRect(w: 400, h: 600),
        };

        frame["CUIRangeInput"] = new CUIRangeInput()
        {
          Anchor = CUIAnchor.Center,
          Relative = new CUINullRect(w: 0.8f),
          Absolute = new CUINullRect(h: 50),
          PinCount = 5,
          Pin = 3,
        };

        return frame;
      }
    }
  }
}