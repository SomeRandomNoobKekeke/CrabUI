using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected virtual MainComponentTracker_Part MainComponentTracker { get; set; } = new();
    public class MainComponentTracker_Part : Part, IModule
    {
      public CUIMainComponent MainComponent { get; set; }

      public void OnAttachedTo(CUIComponent component)
      {
        if (component is not CUIMainComponent mainComponent) return;
        SetRec(mainComponent);
        Self.DebugChannel.AttachToMainComponent();
      }

      public void OnDetached()
      {
        Self.DebugChannel.DetachFromMainComponent();
        SetRec(null);
      }

      private void SetRec(CUIMainComponent mainComponent)
      {
        MainComponent = mainComponent;

        foreach (CUIComponent child in Self.Tree.Children)
        {
          child.MainComponentTracker.SetRec(mainComponent);
        }
      }


    }
  }


}