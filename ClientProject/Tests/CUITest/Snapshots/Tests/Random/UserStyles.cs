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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent UserStyles()
      {
        CUIFrame frame = new CUIDefault.Frame("UserStyles", 400, 600);

        CUIActionStyle<CUIDefault.Frame> style = new CUIActionStyle<CUIDefault.Frame>("change caption", (frame) =>
        {
          frame.Background.Color = new Color(32, 0, 32);
          frame.Caption = "Does the Black Moon howl?";
        });

        frame["apply"] = new CUIButton("apply style")
        {
          OnMouseDown = (e) => CUICore.Styles.AddStyle(style),
          Anchor = CUIAnchor.LeftCenter,
        };

        frame["remove"] = new CUIButton("remove style")
        {
          OnMouseDown = (e) => CUICore.Styles.RemoveStyleById<CUIDefault.Frame>("change caption"),
          Anchor = CUIAnchor.RightCenter,
        };

        return frame;
      }
    }
  }
}