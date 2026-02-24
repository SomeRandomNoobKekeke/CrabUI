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
  public class CUILayoutProp<T> : CUIReactiveProp<T>
  {
    public LayoutMarker.Pattern Pattern { get; set; } = LayoutMarker.Pattern.None;
    public LayoutMarker.ILayoutMarkable LayoutHost { get; set; }

    public override object Host
    {
      get => base.Host;
      set
      {
        base.Host = value;

        if (Host is LayoutMarker.ILayoutMarkable)
        {
          LayoutHost = Host as LayoutMarker.ILayoutMarkable;
        }
      }
    }

    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        LayoutHost.Mark(Pattern);
      }
    }
  }
}