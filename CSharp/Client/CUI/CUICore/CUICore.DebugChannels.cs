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
    protected DebugChannels_Part DebugChannelsPart { get; } = new();

    public class DebugChannels_Part : Part
    {
      public void Init()
      {
        DebugChannels.Route(Self.Main.DebugChannels);

        foreach (DebugNode node in DebugChannels.Values)
        {
          node.Map(Pomoyka);
        }
      }

      public DebugNode<object, object, object> Pomoyka { get; } = new();
      public new DebugChannelsDict DebugChannels { get; } = new()
      {
        ["Child Added"] = new DebugNode<CUIComponent, CUIComponent>()
        {
          Factory = (parent, child) => new DebugEvent()
          {
            Msg = $"{Logger.White(child)} attached to {Logger.White(parent)}",
          },
        },
      };
    }
  }
}