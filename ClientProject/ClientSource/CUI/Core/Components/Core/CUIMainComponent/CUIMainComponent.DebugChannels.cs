using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public partial class CUIMainComponent
  {
    public CUIDebugNode Debug_LayoutUpdated = new(DebugCategory.LayoutUpdated) { IsOpen = true };


    [InitMethod]
    protected override void InitDebugChannels()//CRINGE i have no idea why this works
    {
      // base.InitDebugChannels(); 
      Debug_LayoutUpdated.Map(DebugRelays[DebugCategory.LayoutUpdated]);
    }

    public new DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
    };
  }
}