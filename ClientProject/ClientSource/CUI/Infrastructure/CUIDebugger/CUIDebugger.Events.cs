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
    public class EventsPageComponent : CUIPage
    {
      public enum ShouldClearEnum
      {
        Never, AfterUpdate, AfterDraw, AfterNEvents
      }

      public ClearableEvent<DebugEvent> Input { get; } = new();
      public CUIVerticalList EventList;

      public ShouldClearEnum ShouldClear = ShouldClearEnum.AfterUpdate;

      public bool ClearRequested;

      public void HandleDebugEvent(DebugEvent e)
      {
        if (ClearRequested)
        {
          EventList.Clear();
          ClearRequested = false;
        }

        if (ShouldClear == ShouldClearEnum.AfterNEvents)
        {
          if (EventList.Children.Count > 30)
          {
            EventList.Children.Remove(EventList.Children.Last());
          }
        }

        EventList.Children.Insert(0, new CUITextBlock()
        {
          Text = e.ToString(),
          TextAnchor = new Vector2(0, 0.5f),
        });
      }

      public void OnOpenHandler()
      {
        CUI.DebugHub.Output.Map(Input);
        CUICore.OnUpdate += UpdateHook;
        CUICore.OnDrawAfterGUI += DrawHook;

        EventList.Clear();
      }

      public void OnCloseHandler()
      {
        CUI.DebugHub.Output.Unmap(Input);
      }


      public void UpdateHook(double totalTime)
      {
        if (ShouldClear == ShouldClearEnum.AfterUpdate)
        {
          ClearRequested = true;
        }
      }
      public void DrawHook(CUISpriteBatch spriteBatch)
      {
        if (ShouldClear == ShouldClearEnum.AfterDraw)
        {
          ClearRequested = true;
        }
      }

      public EventsPageComponent() : base()
      {
        Background.Color = new Color(255, 0, 200);

        OnOpen.Add(OnOpenHandler);
        OnClose.Add(OnCloseHandler);

        Input.Add(HandleDebugEvent);

        this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };

        this["layout"]["header"] = new CUIHorizontalList()
        {
          FitContent = new CUIBool2(false, true),
          Background = { Color = Color.Red }
        };

        this["layout"]["header"]["Update"] = new CUIButton("Update")
        {
          OnMouseDown = (c, e) => ShouldClear = ShouldClearEnum.AfterUpdate,
          Flex = 1,
        };

        this["layout"]["header"]["Draw"] = new CUIButton("Draw")
        {
          OnMouseDown = (c, e) => ShouldClear = ShouldClearEnum.AfterDraw,
          Flex = 1,
        };

        this["layout"]["header"]["AfterN"] = new CUIButton("AfterN")
        {
          OnMouseDown = (c, e) => ShouldClear = ShouldClearEnum.AfterNEvents,
          Flex = 1,
        };

        this["layout"]["list"] = EventList = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Scrollable = true,
          Flex = 1,
        };
      }
    }
  }
}