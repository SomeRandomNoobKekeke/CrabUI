using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIDebugger
  {
    public class EventsPageComponent : CUIPage
    {

      public ClearableEvent<DebugEvent> Input { get; } = new();
      public CUIVerticalList EventList;

      public void HandleDebugEvent(DebugEvent e)
      {
        if (EventList.Children.Count > 10)
        {
          EventList.RemoveChild(EventList.Children.Last());
        }

        EventList.Insert(new CUITextBlock()
        {
          Text = e.ToString(),
          TextAnchor = new Vector2(0, 0.5f),
        }, 0);
      }

      public void OnOpenHandler()
      {
        CUI.DebugHub.Output.Map(Input);
      }

      public void OnCloseHandler()
      {
        CUI.DebugHub.Output.Unmap(Input);
      }

      public EventsPageComponent() : base()
      {
        Background.Color = new Color(255, 0, 200);

        OnOpen.Add(OnOpenHandler);
        OnClose.Add(OnCloseHandler);

        Input.Add(HandleDebugEvent);

        this["list"] = EventList = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          Scrollable = true,
        };
      }
    }
  }
}