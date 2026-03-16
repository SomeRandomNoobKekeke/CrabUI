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
    public Protected_Part Protected { get; set; } = new();
    public class Protected_Part : Part
    {
      public MainComponentTracker_Part MainComponentTracker { get; set; } = new();
    }


    public class MainComponentTracker_Part : Part
    {
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

        foreach (CUIComponent child in Self.Children)
        {
          child.Protected.MainComponentTracker.SetRec(mainComponent);
        }
      }


    }
  }


}