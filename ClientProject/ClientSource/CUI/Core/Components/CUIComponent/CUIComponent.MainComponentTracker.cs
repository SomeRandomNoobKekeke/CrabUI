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


      //TODO find better place for these
      //TODO release all handles
      RightResizeHandle.ForceRelease();
      DragHandle.ForceRelease();
    }


    protected virtual MainComponentTracker_Part MainComponentTracker { get; set; } = new();
    public class MainComponentTracker_Part : Part, IModule
    {
      [In] public Tree_Part Tree { get; set; }


      public void Init()
      {
        Tree.OnAttachToParent.Add(OnAttachToParentHandler);
        Tree.OnDetachFromParent.Add(OnDetachFromParentHandler);
      }

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

      public void OnAttachToParentHandler(CUIComponent parent)
      {
        if (parent is CUIMainComponent mainComponent)
        {
          SetRec(mainComponent);
        }
        else
        {
          SetRec(parent.MainComponent);
        }
      }

      public void OnDetachFromParentHandler(CUIComponent parent)
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