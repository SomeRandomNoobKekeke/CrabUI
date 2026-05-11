using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUICore
  {
    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {
        Self.DebugChannels.Route(Self.Main.DebugChannels);
      }
    }

    public Dictionary<string, IDebugNode> DebugChannels { get; } = new()
    {
      ["Prop Set"] = new DebugNode<CUIComponent, Type, string, object>()
      {
        MsgFactory = (component, propType, propName, value)
         => $"{component} {propType} {propName} {value}",
      },
      ["Child Added"] = new DebugNode<CUIComponent, CUIComponent>()
      {
        MsgFactory = (parent, child)
          => $"{Logger.White(child)} attached to {Logger.White(parent)}",
      },
      ["Draw Visual Unit"] = new DebugNode<VisualUnit>()
      {
        MsgFactory = (u) => $"Unit Drawn [{u}]",
      },
      ["Visual Unit Flattened"] = new DebugNode<VisualUnit>(),
    };


  }
}