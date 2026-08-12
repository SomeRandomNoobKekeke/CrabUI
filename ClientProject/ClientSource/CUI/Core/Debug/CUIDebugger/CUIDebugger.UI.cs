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

    private void ToggleMG()
    {
      MGFrame.Absolute = MGFrame.Absolute with { Position = new Vector2(0, 0) };
      MGFrame.Toggle();
    }

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

    private CUIComponent CreateToolsPanel()
    {
      CUIHorizontalList list = new()
      {
        FitContent = new(false, true),
        Background = { Color = CUICore.Palettes.Secondary["panel"] },
        Borders = { Bottom = 3 }
      };
      list["Hint"] = new CUITextBlock("Tools: ") { };


      list["mg"] = new CUIButton("Magnifying Glass")
      {
        OnMouseDown = (e) => ToggleMG(),
      };
      list.DeepPalette = CUICore.Palettes.Secondary;

      return list;
    }

    private CUIComponent CreateNodesList()
    {
      CUIVerticalList wrapper = new CUIVerticalList()
      {
        Absolute = new(w: 150),
        Borders = { Right = 3 }
      };
      wrapper["header"] = new CUITextBlock("Nodes:");
      wrapper["list"] = new CUIVerticalList() { Flex = 1 };

      return wrapper;
    }

    private string EventFlowControlsGroup = "debugger event flow controls";
    private CUIComponent CreateEventList()
    {
      CUIVerticalList wrapper = new CUIVerticalList() { Flex = 1 };
      wrapper["controls"] = new CUIHorizontalList()
      {
        FitContent = new CUIBool2(false, true),
        Borders = { Bottom = 1 }
      };

      wrapper["list"] = EventList = new CUIVerticalList()
      {
        Flex = 1,
        Scrollable = true,
        Background = { Sprite = CUISprite.VerticalGradient, Color = new Color(0, 100, 100) }
      };

      using (new CUIContextStyle<CUIRadioButton>(btn =>
      {
        btn.Flex = 1;
        btn.Group = EventFlowControlsGroup;
        btn.MasterColor = btn.Selected ? new Color(0, 128, 128) : btn.Palette["main"];
        btn.Changed += (state) =>
        {
          btn.MasterColor = state ? new Color(0, 128, 128) : btn.Palette["main"];
          ClearEventList();
        };
      }))
      {
        wrapper["controls"]["free"] = FreeEventFlow = new CUIRadioButton("Free");
        wrapper["controls"]["draw"] = DrawEventFlow = new CUIRadioButton("Draw");
        wrapper["controls"]["update"] = UpdateEventFlow = new CUIRadioButton("Update") { Selected = true };
      }



      return wrapper;
    }

    private void ClearEventList()
    {
      EventList.Clear();
      EventList.Scroll = 0;
    }

    private void Refresh()
    {
      RefreshNodes();
      ClearEventList();
    }

    private void RefreshNodes()
    {
      CUIVerticalList nodes = this["layout"]["panels"]["nodes"].Get<CUIVerticalList>("list");

      nodes.Clear();
      foreach (string name in DebugHub.Gates.Names)
      {
        CUIButton btn = new CUIButton(name);

        btn.MouseDown += (e) =>
        {
          DebugHub.Gates[name].Toggle();
          btn.MasterColor = DebugHub.Gates[name].IsOpen ? new Color(0, 128, 128) : btn.Palette["main"];
          ClearEventList();
        };

        btn.MasterColor = DebugHub.Gates[name].IsOpen ? new Color(0, 128, 128) : btn.Palette["main"];

        nodes.Children.Add(btn);
      }
    }

    private void CreateUI()
    {
      TargetMainComponent = CUI.TopMain;

      Absolute = new CUINullRect(w: 400, h: 600);
      Background.Sprite = CUISprite.White;
      Palette = CUICore.Palettes.Primary;

      Anchor = CUIAnchor.LeftCenter;

      OnOpen += () =>
      {
        DebugHub.Output.Map(Input);
        Refresh();
      };
      OnClose += () => DebugHub.Output.Unmap(Input);

      MGFrame = CreateMGFrame();

      this["layout"]["tools"] = CreateToolsPanel();
      this["layout"]["panels"] = new CUIHorizontalList() { Flex = 1, };
      this["layout"]["panels"]["nodes"] = CreateNodesList();
      this["layout"]["panels"]["events"] = CreateEventList();
    }
  }
}