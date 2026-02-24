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
  public class LayoutSlot
  {
    public Layout.ILayoutHost Host { get; set; }

    private Layout layout;
    public Layout Layout
    {
      get => layout;
      set
      {
        if (layout is not null) layout.AbstractHost = null;
        layout = value;
        if (layout is not null) layout.AbstractHost = Host;
      }
    }
  }
}