using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public class LayoutSlot : IModule
  {
    [In] public Layout.Target Host { get; set; }

    private Layout layout;
    public Layout Layout
    {
      get => layout;
      set
      {
        if (layout is not null) layout.InjectHost(null);
        layout = value;
        if (layout is not null) layout.InjectHost(Host);
      }
    }
  }
}