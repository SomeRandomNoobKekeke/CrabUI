using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIDebugger
  {
    public class ComponentsPageComponent : CUIPage
    {

      private CUIVerticalList ComponentList;
      private CUIComponent HighlightOverlay;

      private void HandleOpen()
      {
        CUI.TopMain.Children.Insert(0, HighlightOverlay);
        Refresh();
      }

      private void HandleClose()
      {
        HighlightOverlay.RemoveSelf();
        HighlightOverlay.Absolute = new CUINullRect();
      }

      protected override void Refresh()
      {
        ComponentList.Clear();

        foreach (CUIVisualComponent child in CUI.Main.DeepStructuralSplit)
        {
          if (child.IsDebugTool) continue;

          CUIButton btn = new CUIButton(child.ToString())
          {
            TextAnchor = CUIAnchor.LeftCenter,
            Palette = CUICore.Palettes.Secondary,
            MasterColor = child.Debug ? new Color(0, 200, 200) : new Color(0, 0, 200),
          };

          btn.MouseDown += (e) =>
          {
            child.DeepDebug = !child.DeepDebug;
            Refresh();
          };

          btn.MouseEnter += (e) => HighlightOverlay.Rect = child.OuterRect;
          btn.MouseLeave += (e) => HighlightOverlay.Rect = new CUIRect(0, 0, 0, 0);

          ComponentList.Add(btn);
        }
      }


      public ComponentsPageComponent()
      {
        OnOpen.Add(HandleOpen);
        OnClose.Add(HandleClose);

        HighlightOverlay = new CUIComponent()
        {
          Background = {
            Sprite = CUISprite.BaroDev,
            Color = Color.White *0.5f,
          }
        };

        this["components"] = ComponentList = new CUIDefault.VerticalPanel()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Scrollable = true,
        };
      }
    }

  }
}