using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class MainComponentTracker : IModule
  {
    public interface Target
    {
      public IReadOnlyList<Target> Children { get; }
      public MainComponentTracker Tracker { get; }
    }


    public Target Host { get; set; }

    public CUIMainComponent MainComponent { get; set; }

    public void OnAttachedTo(object component)
    {
      if (component is not CUIMainComponent mainComponent) return;
      SetRec(mainComponent);
    }

    public void OnDetached()
    {
      SetRec(null);
    }

    private void SetRec(CUIMainComponent mainComponent)
    {
      MainComponent = mainComponent;

      foreach (Target child in Host.Children)
      {
        child.Tracker.SetRec(mainComponent);
      }
    }


  }
}