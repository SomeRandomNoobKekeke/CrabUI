using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    protected Tree_Part Tree { get; } = new();
    public class Tree_Part : Part, IModule
    {
      public void Init()
      {
        ReadOnlyChildren = Children.AsReadOnly();

        Debug_ChildAdded.Map(Self.DebugRelays["Child Added"]);
        Debug_ChildRemoved.Map(Self.DebugRelays["Child Removed"]);
      }

      public DebugNode<CUIComponent, CUIComponent> Debug_ChildAdded = new(
        "Tree Changed", CUI.DebugHub,
        (parent, child) => $"{parent} <- {child}"
      );
      public DebugNode<CUIComponent, CUIComponent> Debug_ChildRemoved = new(
        "Tree Changed", CUI.DebugHub,
        (parent, child) => $"{parent} => {child}"
      );
      [In] public MainComponentTracker_Part MainComponentTracker { get; set; }

      public bool Changed { get; set; }
      public event Action OnChanged;

      public CUIComponent Parent { get; set; }
      public List<CUIComponent> Children { get; } = new();
      public IReadOnlyList<CUIComponent> ReadOnlyChildren { get; private set; }


      public virtual void OnChildAdded(CUIComponent child) { }
      public virtual void OnChildRemoved(CUIComponent child) { }
      public virtual void OnAttachToParent(CUIComponent parent)
      {
        MainComponentTracker.OnAttachedTo(parent);
      }

      public virtual void OnDetachFromParent(CUIComponent parent)
      {
        MainComponentTracker.OnDetached();
      }

      public void AddChild(CUIComponent child)
      {
        Children.Add(child);
        child.Tree.Parent = Self;
        PropogateTreeChanged();
        Self.Layout.RequireChildrenUpdate = true;

        OnChildAdded(child);
        child.Tree.OnAttachToParent(Self);
        Debug_ChildAdded.Send(Self, child);
      }

      public void RemoveChild(CUIComponent child)
      {
        Children.Remove(child);
        child.Tree.Parent = null;
        PropogateTreeChanged();
        Self.Layout.RequireChildrenUpdate = true;

        OnChildRemoved(child);
        child.Tree.OnDetachFromParent(Self);

      }

      public void RemoveAllChildren()
      {
        foreach (CUIComponent child in Children)
        {
          child.Tree.Parent = null;
          OnChildRemoved(child);
          child.Tree.OnDetachFromParent(Self);
        }

        PropogateTreeChanged();
        Self.Layout.RequireChildrenUpdate = true;
        Children.Clear();
      }


      private void PropogateTreeChanged()
      {
        Changed = true;
        OnChanged?.Invoke();
        Parent?.Tree.PropogateTreeChanged();
      }


    }
  }
}