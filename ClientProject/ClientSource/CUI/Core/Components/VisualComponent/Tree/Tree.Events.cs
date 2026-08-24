using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Collections;
namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    protected TreeEvents_Part Tree { get; } = new();
    public class TreeEvents_Part : Part, IModule
    {
      public LayoutMarker.Pattern MarkPattern { get; } = LayoutMarker.Pattern.UpAndDown;

      public void OnChildAdded(CUIVisualComponent child)
      {
        PropogateTreeChanged();
        Self.Remember(child);
        Self.LayoutMarker.Mark(MarkPattern);
      }

      public void OnChildRemoved(CUIVisualComponent child)
      {
        PropogateTreeChanged();
        Self.Forget(child);
        Self.LayoutMarker.Mark(MarkPattern);
      }

      public void OnAttachToParent(CUIVisualComponent parent)
      {
        Self.MainComponentTracker.OnAttachToParentHandler(parent);
        Self.ProtectedCommands.OnAttachToParentHandler(parent);
        Self.InheritProps(parent);
      }

      public void OnDetachFromParent(CUIVisualComponent parent)
      {
        Self.MainComponentTracker.OnDetachFromParentHandler(parent);
        Self.ProtectedCommands.OnDetachFromParentHandler(parent);
      }

      public void OnChildrenRearranged()
      {
        PropogateTreeChanged();
        Self.LayoutMarker.Mark(MarkPattern);
      }


      public bool Changed { get; set; }
      public void PropogateTreeChanged()
      {
        Changed = true;
        Self.Parent?.Tree.PropogateTreeChanged();
      }
    }
  }
}