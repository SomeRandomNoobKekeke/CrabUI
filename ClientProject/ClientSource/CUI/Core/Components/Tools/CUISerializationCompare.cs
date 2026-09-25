using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public class CUISerializationCompare : CUIDefault.Frame
  {
    public static CUISerializationCompare Instance
      => CUICore.Singletons.GetOrUpdate<CUISerializationCompare>(static () => new());


    private CUIVisualComponent _Component; public CUIVisualComponent Component
    {
      get => _Component;
      set
      {
        _Component = value;
        ViewBefore.Root = value;
        ViewAfter.Root = CUIVisualComponent.Deserialize(ViewBefore.Serialize());
      }
    }

    public CUIVisualComponent ComponentBefore
    {
      get => ViewBefore.Root;
      set => ViewBefore.Root = value;
    }

    public CUIVisualComponent ComponentAfter
    {
      get => ViewAfter.Root;
      set => ViewAfter.Root = value;
    }

    public CUIGroupPropView ViewBefore { get; }
    public CUIGroupPropView ViewAfter { get; }


    public CUISerializationCompare() : base("Serialization Compare")
    {
      Absolute = new CUINullRect(50, 0, 600, 800);
      TargetMainComponent = CUI.TopMain;
      Anchor = CUIAnchor.LeftTop;

      this["layout"]["split"] = new CUIHorizontalList() { Flex = 1 };

      this["layout"]["split"]["before"] = new CUIVerticalList()
      {
        Flex = 1,
        Border = new CUISizes(right: 2),
      };
      this["layout"]["split"]["after"] = new CUIVerticalList()
      {
        Flex = 1,
      };

      this["layout"]["split"]["before"]["header"] = new CUIDefault.SolidTextBlock("Before");
      this["layout"]["split"]["after"]["header"] = new CUIDefault.SolidTextBlock("After");

      this["layout"]["split"]["before"]["main"] = ViewBefore = new CUIGroupPropView() { Flex = 1 };
      this["layout"]["split"]["after"]["main"] = ViewAfter = new CUIGroupPropView() { Flex = 1 };

      ViewBefore.ComponentTree.Selected += (c) =>
      {
        ViewAfter.ComponentTree.Select(c.RelativeAKA(ComponentBefore));
      };

      ViewBefore.PropView.Props.MouseScroll += (e) => ViewAfter.PropView.Props.Scroll(e.Scroll);

    }
  }
}