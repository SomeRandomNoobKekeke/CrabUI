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
    #region public
    #endregion
    public CUIMainComponent MainComponent => MainComponentTracker.MainComponent;




    #region protected
    #endregion
    protected virtual MainComponentTracker_Part MainComponentTracker { get; set; } = new();
    public class MainComponentTracker_Part : Part, IModule
    {
      public CUIMainComponent MainComponent { get; set; }

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

        if (MainComponent is not null)
        {
          Self.DebugRelays.Map(MainComponent.DebugRelays);
        }
      }

      public void OnDetached()
      {
        if (MainComponent is not null)
        {
          Self.DebugRelays.Unmap(MainComponent.DebugRelays);
        }

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