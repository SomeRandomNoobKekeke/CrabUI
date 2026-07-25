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

      public void Init()
      {
        Debug_ChildAdded.Map(Self.DebugRelays[DebugCategory.TreeChanged]);
        Debug_ChildRemoved.Map(Self.DebugRelays[DebugCategory.TreeChanged]);

        Debug_LayoutMarked.Map(Self.DebugRelays[DebugCategory.LayoutMarked]);
      }


      public DebugNode<CUIComponent, CUIComponent> Debug_ChildAdded = new(
        DebugCategory.TreeChanged, CUI.DebugHub,
        (parent, child) => $"{parent} <= {child}"
      );
      public DebugNode<CUIComponent, CUIComponent> Debug_ChildRemoved = new(
        DebugCategory.TreeChanged, CUI.DebugHub,
        (parent, child) => $"{parent} => {child}"
      );

      public DebugNode<CUIComponent, LayoutMarker.Pattern, string> Debug_LayoutMarked { get; } = new(
        DebugCategory.LayoutMarked, CUI.DebugHub,
        (host, pattern, reason) => $"{host} {reason} {pattern}"
      );



      public void OnChildAdded(CUIComponent child)
      {
        PropogateTreeChanged();
        Self.Remember(child);
        Self.LayoutMarker.Mark(MarkPattern);
      }

      public void OnChildRemoved(CUIComponent child)
      {
        PropogateTreeChanged();
        Self.Forget(child);
        Self.LayoutMarker.Mark(MarkPattern);
      }

      public void OnAttachToParent(CUIComponent parent)
      {
        Self.MainComponentTracker.OnAttachToParentHandler(parent);
        Self.ProtectedCommands.OnAttachToParentHandler(parent);
      }

      public void OnDetachFromParent(CUIComponent parent)
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