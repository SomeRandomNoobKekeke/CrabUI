using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIDebugger : CUIDefault.Frame
  {
    private CUIPages Pages;
    private EventsPageComponent EventsPage = new();
    private ComponentsPageComponent ComponentsPage = new();

    private void CreateUI()
    {
      TargetMainComponent = CUI.TopMain;

      Absolute = new CUINullRect(w: 400, h: 600);
      Background.Sprite = CUISprite.White;
      Palette = CUICore.Palettes.Primary;

      Anchor = CUIAnchor.LeftCenter;

      OnOpen += DeepRefresh;

      MGFrame = CreateMGFrame();
      this["layout"]["tools"] = CreateToolsPanel();

      this["layout"]["page buttons"] = new CUIHorizontalList() { FitContent = new CUIBool2(false, true) };
      this["layout"]["page buttons"]["events"] = new CUIButton("Events")
      {
        Flex = 1,
        OnMouseDown = (e) => Pages.Open(EventsPage),
      };
      this["layout"]["page buttons"]["components"] = new CUIButton("Components")
      {
        Flex = 1,
        OnMouseDown = (e) => Pages.Open(ComponentsPage),
      };
      this["layout"]["page buttons"].DeepPalette = CUICore.Palettes.Secondary;


      this["layout"]["pages"] = Pages = new CUIPages() { Flex = 1, };

      Pages.Open(EventsPage);
    }


    public CUIDebugger() : base("Debug")
    {
      CreateUI();
    }
  }
}