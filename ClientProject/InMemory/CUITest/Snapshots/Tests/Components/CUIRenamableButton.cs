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
    public static partial class Components
    {
      public static CUIComponent CUIRenamableButton()
      {
        CUIFrame frame = new CUIDefault.Frame("CUIRenamableButton", 400, 600);

        frame["button"] = new CUIRenamableButton("bruh")
        {
          Anchor = CUIAnchor.Center,
        };

        frame["rename"] = new CUIButton("rename")
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(y: -100),
        };

        frame.Commands.ListenFor("rename", () =>
        {
          frame.Get<CUIRenamableButton>("button").IsRenaming = true;
        });

        return frame;
      }
    }
  }
}