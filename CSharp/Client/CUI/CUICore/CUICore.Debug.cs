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
        Attach();
      }


      private void Output(DebugEvent e)
      {
        CUI.Logger.Log(e);
      }

      private void Attach()
      {
        Self.DebugChannel.Pomoyka.Pin.Add(Output);
      }

      private void Detach()
      {
        Self.DebugChannel.Pomoyka.Pin.Remove(Output);
      }
    }
  }
}