using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public partial class CUICore
  {
    public Debugger_Part Debugger { get; } = new();

    public class Debugger_Part : Part
    {
      public void Init()
      {
        Output.Add(e => CUI.Logger.Log(e));
      }
      public ClearableEvent<DebugEvent> Output { get; } = new();

      public IEnumerable<string> ChannelNames => Self.DebugChannels.Keys;
      public void Open(string name)
      {
        if (!Self.DebugChannels.ContainsKey(name)) return;
        if (Output.IsRouted(Self.DebugChannels[name].Pin)) return;
        Output.Route(Self.DebugChannels[name].Pin);
      }

      public void Close(string name)
      {
        if (!Self.DebugChannels.ContainsKey(name)) return;
        if (!Output.IsRouted(Self.DebugChannels[name].Pin)) return;
        Output.Unroute(Self.DebugChannels[name].Pin);
      }

      public void Toggle(string name)
      {
        if (!Self.DebugChannels.ContainsKey(name)) return;
        if (Output.IsRouted(Self.DebugChannels[name].Pin))
        {
          Output.Unroute(Self.DebugChannels[name].Pin);
        }
        else
        {
          Output.Route(Self.DebugChannels[name].Pin);
        }
      }

      public bool IsOpen(string name)
      {
        if (!Self.DebugChannels.ContainsKey(name)) return false;
        return Output.IsRouted(Self.DebugChannels[name].Pin);
      }

    }
  }
}