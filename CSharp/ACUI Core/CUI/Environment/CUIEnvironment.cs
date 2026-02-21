using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public class CUIEnvironment
  {
    public CUILifeCycle LifeCycle { get; set; } = new CUILifeCycle();

    public InputScanner InputScanner { get; set; } = new InputScanner();

    //BRUH
    public double TotalTime => Timing.TotalTime;

    public string GetID() => ModInfo.HookId;
  }
}