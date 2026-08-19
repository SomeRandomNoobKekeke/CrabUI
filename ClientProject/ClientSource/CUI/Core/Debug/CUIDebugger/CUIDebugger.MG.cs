using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIDebugger : CUIDefault.Frame
  {
    private CUIFrame MGFrame;

    private CUIFrame CreateMGFrame()
    {
      CUIFrame frame = new CUIFrame()
      {
        Absolute = new CUINullRect(w: 256, h: 256),
        Anchor = CUIAnchor.Center,
        TargetMainComponent = CUI.TopMain,
        Borders = { Sizes = new CUISizes(5, 5, 5, 5), Color = new Color(0, 200, 200) }
      };

      frame["mg"] = new CUIMagnifyingGlass()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Size = new Point(40, 40),
      };

      frame.OnOpen += () => frame.Get<CUIMagnifyingGlass>("mg").Active = true;
      frame.OnClose += () => frame.Get<CUIMagnifyingGlass>("mg").Active = false;

      return frame;
    }
  }
}