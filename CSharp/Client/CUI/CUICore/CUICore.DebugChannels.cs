using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUICore
  {
    //BRUH stupid part just for Init
    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {
        Self.DebugChannels.Route(Self.Main.DebugChannels);
      }
    }

    public DebugChannelsDict DebugChannels { get; } = new()
    {
      ["Child Added"] = new DebugNode<CUIComponent, CUIComponent>()
      {
        Factory = (parent, child) => new DebugEvent()
        {
          Msg = $"{Logger.White(child)} attached to {Logger.White(parent)}",
        },
      },
      ["Draw Visual Unit"] = new DebugNode<VisualUnit>()
      {
        Factory = (u) => new DebugEvent() { Msg = $"Unit Drawn [{u}]" },
      },
      ["Visual Unit Flattened"] = new DebugNode<VisualUnit>(),
    };


  }
}