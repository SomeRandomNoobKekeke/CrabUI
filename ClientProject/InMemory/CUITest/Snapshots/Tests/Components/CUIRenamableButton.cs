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
        CUIFrame frame = new CUIDefault.Frame("CUIRenamableButton", 600, 600);


        frame["wrapper1"] = new CUIVerticalList
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(x: 100, y: 0, w: 200, h: 100),
          Background = { Color = Color.Lime },
        };

        frame["wrapper1"]["button"] = new CUIRenamableButton("bruh")
        {

        };
        frame["wrapper1"]["rename"] = new CUIButton("rename")
        {
          OnMouseDown = (e) => frame["wrapper1"].Get<CUIRenamableButton>("button").IsRenaming = true,
        };



        frame["wrapper2"] = new CUIComponent
        {
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(x: -100, y: 0, w: 200, h: 100),
          Background = { Color = Color.Lime },
        };

        frame["wrapper2"]["button"] = new CUIRenamableButton("bruh")
        {

        };
        frame["wrapper2"]["rename"] = new CUIButton("rename")
        {
          Absolute = new CUINullRect(x: 0, y: 30),
          OnMouseDown = (e) => frame["wrapper2"].Get<CUIRenamableButton>("button").IsRenaming = true,
        };



        return frame;
      }
    }
  }
}