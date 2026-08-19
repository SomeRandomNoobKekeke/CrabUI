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
    public DebugHub DebugHub => CUICore.DebugHub;

    public ClearableEvent<DebugEvent> Input { get; } = new();

    public CUIVerticalList EventList { get; private set; }
    private CUIFrame MGFrame;

    public int MaxEvents = 50;

    private CUIRadioButton FreeEventFlow;
    private CUIRadioButton DrawEventFlow;
    private CUIRadioButton UpdateEventFlow;

    public CUIDirection Direction => FreeEventFlow.Selected ? CUIDirection.Reverse : CUIDirection.Straight;

    private bool ClearRequested;
    private void HandleDebugEvent(DebugEvent e)
    {
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

        EventList.Children.Add(new CUITextBlock()
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

        EventList.Children.Insert(0, new CUITextBlock()
        {
          Text = e.ToString(),
          TextAnchor = CUIAnchor.LeftCenter,
        });
      }


    }

    private void UpdateHook(double totalTime)
    {
      if (UpdateEventFlow.Selected) ClearRequested = true;
    }
    private void DrawHook(CUISpriteBatch spriteBatch)
    {
      if (DrawEventFlow.Selected) ClearRequested = true;
    }

    public CUIDebugger() : base("Debug")
    {
      Input.Add(HandleDebugEvent);
      CreateUI();

      CUICore.OnUpdate += UpdateHook;
      CUICore.OnDrawAfterGUI += DrawHook;
    }
  }
}