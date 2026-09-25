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
  public class CUISerializationCompare : CUIPage
  {

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


    public CUISerializationCompare() : base()
    {
      this["layout"] = new CUIHorizontalList() { Relative = new CUINullRect(0, 0, 1, 1) };

      this["layout"]["before"] = new CUIVerticalList()
      {
        Flex = 1,
        Border = new CUISizes(right: 2),
      };
      this["layout"]["after"] = new CUIVerticalList()
      {
        Flex = 1,
      };

      this["layout"]["before"]["header"] = new CUIDefault.SolidTextBlock("Before");
      this["layout"]["after"]["header"] = new CUIDefault.SolidTextBlock("After");

      this["layout"]["before"]["main"] = ViewBefore = new CUIGroupPropView() { Flex = 1 };
      this["layout"]["after"]["main"] = ViewAfter = new CUIGroupPropView() { Flex = 1 };

      ViewBefore.ComponentTree.Selected += (c) =>
      {
        ViewAfter.ComponentTree.Select(c.RelativeAKA(ComponentBefore));
      };

      ViewBefore.PropView.Props.MouseScroll += (e) => ViewAfter.PropView.Props.Scroll(e.Scroll);

    }
  }
}