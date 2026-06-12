using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public CUIMainComponent MainComponent => MainComponentTracker.MainComponent;

    protected virtual void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      DebugRelays.Map(MainComponent.DebugRelays);
    }
    protected virtual void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      DebugRelays.Unmap(MainComponent.DebugRelays);
    }


    protected virtual MainComponentTracker_Part MainComponentTracker { get; set; } = new();
    public class MainComponentTracker_Part : Part, IModule
    {
      private CUIMainComponent _MainComponent;
      public CUIMainComponent MainComponent
      {
        get => _MainComponent;
        set
        {
          if (MainComponent is not null) Self.OnDetachedFromMainComponent(MainComponent);
          _MainComponent = value;
          if (MainComponent is not null) Self.OnAttachedToMainComponent(MainComponent);
        }
      }

      public void OnAttachedTo(CUIComponent component)
      {
        if (component is CUIMainComponent mainComponent)
        {
          SetRec(mainComponent);
        }
        else
        {
          SetRec(component.MainComponent);
        }
      }

      public void OnDetached()
      {
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