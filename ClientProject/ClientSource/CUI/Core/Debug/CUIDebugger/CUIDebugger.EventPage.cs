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
    public class EventsPageComponent : CUIPage
    {
      public class DebugEventBlock : CUITextBlock
      {
        protected override bool _IsDebugTool { get; set; } = true;
      }


      public DebugHub DebugHub => CUICore.DebugHub;
      public ClearableEvent<DebugEvent> Input { get; } = new();

      public CUIVerticalList EventList { get; private set; }

      private CUIRadioButton FreeEventFlow;
      private CUIRadioButton DrawEventFlow;
      private CUIRadioButton UpdateEventFlow;

      public int MaxEvents = 50;
      public CUIDirection Direction => FreeEventFlow.Selected ? CUIDirection.Reverse : CUIDirection.Straight;

      private bool ClearRequested;
      private bool CreatedFromHandleDebugEvent; //HACK
      public void HandleDebugEvent(DebugEvent e)
      {
        if (CreatedFromHandleDebugEvent) return;
        CreatedFromHandleDebugEvent = true;
        // BreakTheLoop.After(200);

        if (ClearRequested)
        {
          ClearEventList();
          ClearRequested = false;
        }

        if (Direction == CUIDirection.Straight)
        {
          if (EventList.Children.Count > MaxEvents)
          {
            EventList.Children.Remove(EventList.Children.First());
          }

          EventList.Children.Add(new DebugEventBlock()
          {
            Text = e.ToString(),
            TextAnchor = CUIAnchor.LeftCenter,
          });
        }

        if (Direction == CUIDirection.Reverse)
        {
          if (EventList.Children.Count > MaxEvents)
          {
            EventList.Children.Remove(EventList.Children.Last());
          }

          EventList.Children.Insert(0, new DebugEventBlock()
          {
            Text = e.ToString(),
            TextAnchor = CUIAnchor.LeftCenter,
          });
        }

        CreatedFromHandleDebugEvent = false;
      }


      private void ClearEventList()
      {
        EventList.Clear();
        EventList.Scroll = 0;
      }

      public override void Refresh()
      {
        RefreshNodes();
        ClearEventList();
      }

      private void RefreshNodes()
      {
        CUIVerticalList nodes = this["panels"]["nodes"].Get<CUIVerticalList>("list");

        nodes.Clear();
        foreach (string name in DebugHub.Gates.Names)
        {
          nodes.Children.Add(new CUIToggleButton(name)
          {
            OnToggle = (state) =>
            {
              DebugHub.Gates[name].Toggle();
              ClearEventList();
            }
          });
        }
      }

      private CUIComponent CreateEventList()
      {
        CUIVerticalList wrapper = new CUIVerticalList()
        {
          Flex = 1,
        };

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
          btn.Group = "debugger event flow controls";
          btn.Palette = CUICore.Palettes.Secondary;
          btn.Changed += (state) =>
          {
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


      private void UpdateHook(double totalTime)
      {
        if (UpdateEventFlow.Selected) ClearRequested = true;
      }
      private void DrawHook(CUISpriteBatch spriteBatch)
      {
        if (DrawEventFlow.Selected) ClearRequested = true;
      }



      public void HandleOpen()
      {
        DebugHub.Output.Map(Input);

        CUICore.OnUpdate += UpdateHook;
        CUICore.OnDrawAfterGUI += DrawHook;
        Refresh();
      }

      public void HandleClose()
      {
        DebugHub.Output.Unmap(Input);

        CUICore.OnUpdate += UpdateHook;
        CUICore.OnDrawAfterGUI += DrawHook;
      }

      public EventsPageComponent()
      {
        OnOpen.Add(HandleOpen);
        OnClose.Add(HandleClose);

        Input.Add(HandleDebugEvent);


        this["panels"] = new CUIHorizontalList() { Relative = new CUINullRect(0, 0, 1, 1) };
        this["panels"]["nodes"] = new CUIVerticalList()
        {
          Absolute = new(w: 150),
          Borders = { Right = 3 }
        };
        this["panels"]["nodes"]["header"] = new CUITextBlock("Nodes:");
        this["panels"]["nodes"]["list"] = new CUIVerticalList() { Flex = 1 };


        this["panels"]["events"] = CreateEventList();
      }
    }

  }
}