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
  public interface CUILayoutProp
  {
    public interface Target
    {
      public void Mark(LayoutMarker.Pattern pattern);
    }
  }

  public class CUILayoutProp<T> : CUIReactiveProp<T>, CUILayoutProp
  {
    public LayoutMarker.Pattern Pattern { get; set; } = LayoutMarker.Pattern.None;
    public CUILayoutProp.Target MarkableHost { get; set; }

    public override object Host
    {
      get => base.Host;
      set
      {
        base.Host = value;

        if (Host is CUILayoutProp.Target)
        {
          MarkableHost = Host as CUILayoutProp.Target;
        }
      }
    }

    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        MarkableHost.Mark(Pattern);
      }
    }
  }
}