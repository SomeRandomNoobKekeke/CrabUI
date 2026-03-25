using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    protected InitDebugChannels_Part InitDebugChannels { get; } = new();
    public class InitDebugChannels_Part : Part
    {
      public void Init()
      {
        Self.ChainDrawer.Debug_DrawVisualUnit.Map(Self.DebugChannels["Draw Visual Unit"]);
        Self.VisualFlattener.Debug_UnitFlattened.Map(Self.DebugChannels["Visual Unit Flattened"]);
      }
    }


    public new DebugChannelsDict DebugChannels { get; } = new()
    {
      ["Child Added"] = new DebugNode<CUIComponent, CUIComponent>(),
      ["Draw Visual Unit"] = new DebugNode<VisualUnit>(),
      ["Visual Unit Flattened"] = new DebugNode<VisualUnit>(),
    };
  }
}