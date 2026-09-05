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
  public partial class CUIComponent
  {
    [InitMethod]
    protected virtual void InitLayout()
    {
      Layout = new CUIPlainLayout();
      Layout.ConnectTo(new Adapters_Part.CUIPlainLayout_Host_Part() { Self = this });

      MapLayoutDebugChannels();
    }

    protected void MapLayoutDebugChannels()
    {
      Layout.Debug_MarkedForChildrenUpdate.Map(DebugRelays[DebugCategory.LayoutMarked]);
      Layout.Debug_MarkedForParentUpdate.Map(DebugRelays[DebugCategory.LayoutMarked]);

      LayoutMarker.Debug_Marking_Start.Map(DebugRelays[DebugCategory.LayoutMarked]);
      LayoutMarker.Debug_Marking_End.Map(DebugRelays[DebugCategory.LayoutMarked]);

      Layout.Debug_Calculations.Map(DebugRelays[DebugCategory.LayoutCalculations]);
    }

    public override Layout? Layout { get; protected set; }
  }
}