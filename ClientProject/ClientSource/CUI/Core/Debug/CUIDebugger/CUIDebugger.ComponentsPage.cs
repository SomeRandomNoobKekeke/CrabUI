using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CrabUI
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

        foreach (CUIVisualComponent child in CUI.Main.DeepChildren)
        {
          if (child.IsDebugTool) continue;

          ComponentList.Add(
            new CUITextBlock(child.ToString())
            {
              TextAnchor = CUIAnchor.LeftCenter,
            }
          );
        }
      }


      public ComponentsPageComponent()
      {
        OnOpen.Add(HandleOpen);
        OnClose.Add(HandleClose);

        HighlightOverlay = new CUIComponent()
        {
          Background = { Sprite = CUISprite.BaroDev }
        };

        this["panels"] = new CUIHorizontalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
        };

        this["panels"]["components"] = ComponentList = new CUIVerticalList()
        {
          FitContent = new CUIBool2(true, false),
          Background = { Color = Color.Green },
        };
      }
    }

  }
}