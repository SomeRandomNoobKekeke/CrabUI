using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class CUIDebug
  {
    static CUIDebug()
    {
      RegisterDebugChannels();
    }

    private static void RegisterDebugChannels()
    {
      DebugHub.RegisterChannel(CUIComponent.Tree_Part.Debug_ChildAdded);
      DebugHub.RegisterChannel(CUIComponent.Tree_Part.Debug_ChildRemoved);
    }

    public CUITextDebugger CUITextDebugger { get; } = new();
    public void Open()
    {
      foreach (DebugChannelBase channel in DebugHub.Channels.Values)
      {
        channel.Open = true;
      }
    }

    public void Close()
    {
      foreach (DebugChannelBase channel in DebugHub.Channels.Values)
      {
        channel.Open = false;
      }
    }
  }

  public class CUITextDebugger
  {
    public CUITextDebugger()
    {
      DebugHub.OnMsg.Add(channel =>
      {
        CUI.Logger.Log($"{channel.Name} >> {channel.Msg}");
      });
    }
  }
}