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
    public class EventsPageComponent : CUIPage
    {
      public class DebugEventBlock : CUITextBlock
      {
        protected override bool _IsDebugTool { get; set; } = true;
        public DebugEventBlock(DebugEvent e)
        {
          Text = e.ToString();
          TextAnchor = CUIAnchor.LeftCenter;
        }
      }

      public class CUIDebugEventBlock : CUITextBlock
      {
        public CUIVisualComponent Component { get; set; }
        protected override bool _IsDebugTool { get; set; } = true;
        public CUIDebugEventBlock(CUIDebugEvent cuievent)
        {
          Text = cuievent.ToString();
          Component = cuievent.RelatedComponent;
          TextAnchor = CUIAnchor.LeftCenter;

          MouseEnter += (e) => Commands.SendUp("highlight", Component);
          MouseLeave += (e) => Commands.SendUp("unhighlight", Component);

          Background.Color = CUIColor.FromSeed(Component.ID, 1.0f, 0.5f);
        }
      }


      public DebugHub DebugHub => CUICore.DebugHub;
      public ClearableEvent<DebugEvent> Input { get; } = new();

      public CUIVerticalList EventList { get; private set; }

      private CUIRadioButton FreeEventFlow;
      private CUIRadioButton DrawEventFlow;
      private CUIRadioButton UpdateEventFlow;

      private CUIComponent HighlightOverlay = new CUIComponent()
      {
        Background = {
          Sprite = CUISprite.BaroDev, Color = Color.White *0.5f,
        }
      };

      public int MaxEvents = 1000;
      public CUIDirection Direction => FreeEventFlow.IsSelected ? CUIDirection.Straight : CUIDirection.Straight;

      private bool ClearRequested;
      private bool CreatedFromHandleDebugEvent; //HACK


      private CUIComponent CreateEventBlock(DebugEvent e) => e switch
      {
        CUIDebugEvent cuievent => new CUIDebugEventBlock(cuievent),
        DebugEvent => new DebugEventBlock(e),
      };

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

          EventList.Children.Add(CreateEventBlock(e));
        }

        if (Direction == CUIDirection.Reverse)
        {
          if (EventList.Children.Count > MaxEvents)
          {
            EventList.Children.Remove(EventList.Children.Last());
          }

          EventList.Children.Insert(0, CreateEventBlock(e));
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
              DebugHub.Gates[name].IsOpen = state;
              // ClearEventList();
            },
            InheritPalette = true,
            State = DebugHub.Gates[name].IsOpen,
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
          Background = { Sprite =
            {
              ColorBL = Color.Transparent,
              ColorBR = Color.Red,
            } },
          Style = (c) => c.Background.Color = c.Palette["panel"],
        };

        using (new CUIContextStyle<CUIRadioButton>(btn =>
        {
          btn.Flex = 1;
          btn.GroupName = "debugger event flow controls";
          btn.Palette = CUICore.Palettes.Secondary;
          btn.Toggled += (state) =>
          {
            ClearEventList();
          };
        }))
        {
          wrapper["controls"]["free"] = FreeEventFlow = new CUIRadioButton("Free");
          wrapper["controls"]["draw"] = DrawEventFlow = new CUIRadioButton("Draw");
          wrapper["controls"]["update"] = UpdateEventFlow = new CUIRadioButton("Update") { IsSelected = true };
        }

        wrapper.DeepPalette = CUICore.Palettes.Quaternary;


        return wrapper;
      }


      private void UpdateHook(double totalTime)
      {
        if (UpdateEventFlow.IsSelected) ClearRequested = true;
      }
      private void DrawHook(CUISpriteBatch spriteBatch)
      {
        if (DrawEventFlow.IsSelected) ClearRequested = true;
      }



      public void HandleOpen()
      {
        CUI.TopMain.Children.Insert(0, HighlightOverlay);

        DebugHub.Output.Map(Input);

        CUICore.OnUpdate += UpdateHook;
        CUICore.OnDrawAfterGUI += DrawHook;
        Refresh();
      }

      public void HandleClose()
      {
        HighlightOverlay.RemoveSelf();
        HighlightOverlay.Absolute = new CUINullRect();

        DebugHub.Output.Unmap(Input);

        CUICore.OnUpdate += UpdateHook;
        CUICore.OnDrawAfterGUI += DrawHook;
      }

      public EventsPageComponent()
      {
        OnOpen += HandleOpen;
        OnClose += HandleClose;

        Input.Add(HandleDebugEvent);

        Commands.ListenFor<CUIComponent>("highlight", (c) => HighlightOverlay.Rect = c.OuterRect);
        Commands.ListenFor<CUIComponent>("unhighlight", (c) => HighlightOverlay.Rect = new CUIRect(0, 0, 0, 0));

        this["panels"] = new CUIHorizontalList() { Relative = new CUINullRect(0, 0, 1, 1) };
        this["panels"]["nodes"] = new CUIDefault.VerticalPanel()
        {
          Absolute = new(w: 150),
          Borders = { Right = 3 }
        };
        this["panels"]["nodes"]["header"] = new CUITextBlock("Nodes:");
        this["panels"]["nodes"]["list"] = new CUIVerticalList() { Flex = 1 };
        this["panels"]["nodes"].DeepPalette = CUICore.Palettes.Tertiary;

        this["panels"]["events"] = CreateEventList();
      }
    }

  }
}