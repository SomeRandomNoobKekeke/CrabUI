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
    public class ComponentsPageComponent : CUIPage
    {
      public class ComponentButton : CUIButton
      {
        public CUIComponent Component;

        public void UpdateColor()
        {
          MasterColor = Component.Debug ? new Color(0, 200, 0) : Color.Blue;
        }

        public ComponentButton(CUIComponent component) : base()
        {
          Component = component;

          Text = Component.ToString();
          TextAnchor = CUIAnchor.LeftCenter;
          IsDebugTool = true;
          UpdateColor();

          MouseDown += (c, e) => Commands.SendUp("Toggle Debug", Component);
          MouseEnter += (c, e) => Commands.SendUp("Highlight", Component);
          MouseLeave += (c, e) => Commands.SendUp("Remove Highlight", Component);
        }
      }

      public CUIComponent HighlightOverlay;


      public CUIVerticalList ComponentList;

      public void OnOpenHandler()
      {
        Refresh();
        CUI.TopMain.Children.Insert(0, HighlightOverlay);
        HighlightOverlay.Rect = new CUIRect(0, 0, 0, 0);
      }

      public void OnCloseHandler()
      {
        HighlightOverlay.RemoveSelf();
      }

      public void Refresh()
      {
        ComponentList.Clear();

        foreach (
          CUIComponent child in CUI.Main.DeepChildren.Where(c => !c.IsDebugTool).ToList()
        )
        {
          ComponentList.Add(new ComponentButton(child));
        }
      }

      public ComponentsPageComponent() : base()
      {
        OnOpen.Add(OnOpenHandler);
        OnClose.Add(OnCloseHandler);

        Commands.ListenFor<CUIComponent>("Highlight", component =>
        {
          HighlightOverlay.Rect = component.Rect;
        });

        Commands.ListenFor<CUIComponent>("Remove Highlight", component =>
        {
          HighlightOverlay.Rect = new CUIRect(0, 0, 0, 0);
        });



        Commands.ListenFor<CUIComponent>("Toggle Debug", component =>
        {
          component.DeepDebug = !component.DeepDebug;
          Refresh();
        });

        Background.Color = new Color(0, 255, 200);

        this["list"] = ComponentList = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Scrollable = true,
        };

        HighlightOverlay = new CUIComponent()
        {
          Background = {
            Sprite = CUISprite.BaroDev,
            Color = new Color(255,255,255,64),
          }
        };
      }
    }
  }
}