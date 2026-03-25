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
      private List<EventSubscription> AttachedPins = new();

      public void Init()
      {
        Attach();
      }


      private void Output(DebugEvent e)
      {
        //TODO implement text channel handle in CUIRunner
        CUI.Logger.Log(e);
      }

      private void Attach()
      {
        AttachedPins.Add(Self.DebugChannels["Child Added"].Pin.Add(Output));
      }

      private void Detach()
      {
        foreach (EventSubscription subscription in AttachedPins)
        {
          subscription.Cancel();
        }

        AttachedPins.Clear();
      }
    }
  }
}