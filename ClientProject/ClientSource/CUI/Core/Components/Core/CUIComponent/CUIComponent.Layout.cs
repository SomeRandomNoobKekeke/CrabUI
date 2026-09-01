using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIComponent
  {
    [InitMethod]
    protected virtual void InitLayout()
    {
      Layout = new CUIPlainLayout();
      Layout.ConnectTo(new Adapters_Part.CUIPlainLayout_Host_Part() { Self = this });

      Layout.Debug_MarkedForChildrenUpdate.Map(DebugRelays[DebugCategory.LayoutMarked]);
      Layout.Debug_MarkedForParentUpdate.Map(DebugRelays[DebugCategory.LayoutMarked]);

      LayoutMarker.Debug_Marked.Map(DebugRelays[DebugCategory.LayoutMarked]);
    }

    public override Layout? Layout { get; protected set; }
  }
}