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


namespace CrabUI
{
  public partial class CUIDebugger
  {
    public class MainFrameComponent : CUIFrame
    {
      public void CreateUI()
      {
        Anchor = CUIAnchor.Center;
        Background.Color = new Color(0, 0, 200);
        Absolute = new CUINullRect(w: 400, h: 600);

        this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
        this["layout"]["handle"] = new CUIHorizontalList()
        {
          Background = { Color = Color.Blue },
          FitContent = new CUIBool2(false, true),
        };

        this["layout"]["handle"]["caption"] = new CUITextBlock("Debug")
        {
          Flex = 1,
          TextAnchor = CUIAnchor.LeftCenter,
        };

        this["layout"]["handle"]["close"] = new CUICloseButton();



        this["layout"]["header"] = new CUIHorizontalList()
        {
          Background = { Color = new Color(0, 0, 220) },
          FitContent = new CUIBool2(false, true),
        };

        this["layout"]["header"]["events"] = new CUIButton("Events")
        {
          Flex = 1,
        };

        this["layout"]["header"]["events"] = new CUIButton("Events")
        {
          Flex = 1,
        };

        this["layout"]["header"]["events"] = new CUIButton("Events")
        {
          Flex = 1,
        };
      }

      public MainFrameComponent() : base()
      {
        CreateUI();
      }
    }
  }
}