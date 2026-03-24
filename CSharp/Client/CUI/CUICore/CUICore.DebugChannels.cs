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
    public DebugChannels_Part DebugChannel { get; } = new();

    public class DebugChannels_Part : Part
    {
      public void Init() => RouteMainComponents();

      public DebugNode<CUIComponent, CUIComponent> ChildAdded { get; } = new()
      {
        Factory = (parent, child) => new DebugEvent()
        {
          Msg = $"{Logger.White(child)} attached to {Logger.White(parent)}",
        }
      };

      public void RouteMainComponents()
      {
        ChildAdded.Route(Self.Main.DebugChannel.ChildAdded);
      }
    }
  }
}