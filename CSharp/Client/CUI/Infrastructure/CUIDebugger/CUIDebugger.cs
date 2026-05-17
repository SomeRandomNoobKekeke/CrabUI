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


namespace CrabUIUser
{
  public class CUIDebugger
  {
    public ClearableEvent<DebugEvent> Input { get; } = new();

    public CUIFrame DebugFrame { get; private set; }
    public bool IsOpen { get; private set; }

    public CUIVerticalList EventList { get; private set; }

    public void Init()
    {
      PluginCommands.Add("cuidebug", CUIDebug_Command, () => new string[][] { CUI.DebugHub.Gates.Names.ToArray() });

      DebugFrame = new CUIFrame()
      {
        Absolute = new CUINullRect(w: 400, h: 300),
        Anchor = CUIAnchor.LeftCenter,
        BackgroundColor = Color.Green,
        Debug = true,
      };

      // DebugFrame.DebugRelays.GetNodes("Funny Prop Set").First().Open();

      DebugFrame["layout"] = EventList = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      Input.Add(HandleDebugEvent);
    }

    public void HandleDebugEvent(DebugEvent e)
    {
      EventList.Append(new CUITextBlock()
      {
        Text = e.ToString(),
        Absolute = new CUINullRect(h: 20),
        TextAnchor = new Vector2(0, 0.5f),
      });

      if (EventList.Tree.Children.Count > 10)
      {
        EventList.RemoveChild(EventList.Tree.Children[0]);
      }
    }

    public void Toggle()
    {
      if (IsOpen) Close(); else Open();
    }
    public void Open()
    {
      IsOpen = true;
      DebugFrame.Open();
      CUI.DebugHub.Output.Map(Input);
    }
    public void Close()
    {
      IsOpen = false;
      DebugFrame.Close();
      CUI.DebugHub.Output.Unmap(Input);
    }




    public void CUIDebug_Command(string[] args)
    {
      if (args.Length == 0)
      {
        Toggle();
        return;
      }

      string gate = String.Join(' ', args);
      CUI.DebugHub.Gates[gate].Toggle();
    }
  }
}