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
      public void Init()
      {
        RouteMainComponents();

        Pomoyka.Route(ChildAdded, (CUIComponent c1, CUIComponent c2) => Pomoyka.Send(c1, c2));
      }
      public void RouteMainComponents()
      {
        ChildAdded.Route(Self.Main.DebugChannel.ChildAdded);
      }

      public DebugNode<object, object> Pomoyka { get; } = new();

      public DebugNode<CUIComponent, CUIComponent> ChildAdded { get; } = new()
      {
        Factory = (parent, child) => new DebugEvent()
        {
          Msg = $"{Logger.White(child)} attached to {Logger.White(parent)}",
        }
      };


    }
  }
}