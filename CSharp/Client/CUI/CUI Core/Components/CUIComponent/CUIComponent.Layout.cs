using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent : ILayoutHost
  {
    public Layout Layout { get; set; }
    protected LayoutMarker LayoutMarker { get; }

    void ILayoutHost.MarkLayout(LayoutMarkPattern pattern)
    {
      if (pattern.Empty) return;
      this.MainComponent?.LayoutChanged();
      LayoutMarker.Mark(pattern);
    }
    void ILayoutHost.UpdateChildren() => Layout.UpdateChildren();
    void ILayoutHost.UpdateParent() => Layout.UpdateParent();
    void ILayoutHost.MarkAsRequireChildrenUpdate()
    {
      this.MainComponent?.LayoutChanged();
      Layout.RequireChildrenUpdate = true;
    }
    void ILayoutHost.MarkAsRequireParentUpdate()
    {
      this.MainComponent?.LayoutChanged();
      Layout.RequireParentUpdate = true;
    }
  }
}