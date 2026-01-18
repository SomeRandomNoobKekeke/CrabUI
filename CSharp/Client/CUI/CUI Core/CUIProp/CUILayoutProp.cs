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
    public LayoutMarkPattern Pattern { get; set; } = LayoutMarkPattern.None;
    public ILayoutHost LayoutHost { get; set; }

    public override T Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        if (LayoutHost == null)
        {
          CUI.Logger.Log($"Warning: CUILayoutProp isn't linked to ILayoutHost [{Host}] [{Name}]");
        }
        else
        {
          LayoutHost.MarkLayout(Pattern);
        }
      }
    }
  }
}