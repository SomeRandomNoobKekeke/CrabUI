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
      public DebugChannels_Part()
      {
        Probe.Connect(ChildAdded);
        Probe.Connect(ChildRemoved);

        //TODO unhardcode
        Probe.Read.Add(e => CUI.Logger.Log(e));
      }

      public DebugProbe Probe { get; } = new();



      public DebugRouter<CUIComponent, CUIComponent> ChildAdded = new()
      {
        Name = "Child Added",
        ToText = (parent, child) => $"{parent} <- {child}",
      };
      public DebugRouter<CUIComponent, CUIComponent> ChildRemoved = new();

      public void RouteMainComponent(CUIMainComponent mainComponent)
      {
        ChildAdded.Route(mainComponent.CUIMainComponent_DebugChannel.ChildAdded);
        ChildRemoved.Route(mainComponent.CUIMainComponent_DebugChannel.ChildRemoved);

        // ChildAdded.Map((c1, c2) => CUI.Logger.LogVars(c1, c2));
      }
    }
  }
}