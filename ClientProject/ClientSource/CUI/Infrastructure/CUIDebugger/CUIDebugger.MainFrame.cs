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


namespace CrabUI
{
  public partial class CUIDebugger
  {
    public class MainFrameComponent : CUIFrame
    {
      public CUIPages Pages;
      public EventsPageComponent EventsPage = new();
      public ComponentsPageComponent ComponentsPage = new();
      public GatesPageComponent GatesPage = new();

      public MainFrameComponent() : base()
      {
        TargetMainComponent = CUI.TopMain;
        Anchor = CUIAnchor.LeftCenter;
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

        this["layout"]["handle"]["close"] = new CUICloseButton()
        {
          CrossRelative = new CUINullRect(w: 1),
        };


        this["layout"]["header"] = new CUIHorizontalList()
        {
          Background = { Color = new Color(0, 0, 220) },
          FitContent = new CUIBool2(false, true),
        };

        this["layout"]["header"]["Events"] = new CUIButton("Events")
        {
          Flex = 1,
          OnMouseDown = (c, e) => Pages!.Open(EventsPage),
        };

        this["layout"]["header"]["Components"] = new CUIButton("Components")
        {
          OnMouseDown = (c, e) => Pages!.Open(ComponentsPage),
          Flex = 1,
        };

        this["layout"]["header"]["Gates"] = new CUIButton("Gates")
        {
          OnMouseDown = (c, e) => Pages!.Open(GatesPage),
          Flex = 1,
        };


        this["layout"]["main"] = Pages = new CUIPages()
        {
          Flex = 1,
          Background = { Color = Color.Yellow },
        };

        IsDebugTool = true;
      }
    }
  }
}