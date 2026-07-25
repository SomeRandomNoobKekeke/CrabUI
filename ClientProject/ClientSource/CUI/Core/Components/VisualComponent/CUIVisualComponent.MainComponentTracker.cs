using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    public CUIMainComponent MainComponent => MainComponentTracker.MainComponent;

    protected virtual void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {

    }
    protected virtual void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      DragHandle.ForceRelease();
    }


    protected MainComponentTracker_Part MainComponentTracker { get; } = new();
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

      public void OnAttachToParentHandler(CUIVisualComponent parent)
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

      public void OnDetachFromParentHandler(CUIVisualComponent parent)
      {
        SetRec(null);
      }

      private void SetRec(CUIMainComponent mainComponent)
      {
        MainComponent = mainComponent;

        foreach (CUIVisualComponent child in Self.Children)
        {
          child.MainComponentTracker.SetRec(mainComponent);
        }
      }


    }
  }


}