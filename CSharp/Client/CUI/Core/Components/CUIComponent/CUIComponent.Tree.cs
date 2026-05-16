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


      public List<CUIComponent> Children { get; } = new();
      public IReadOnlyList<CUIComponent> ReadOnlyChildren { get; private set; }

      private CUIComponent _Parent; public CUIComponent Parent
      {
        get => _Parent;
        set => SetParent(value);
      }

      private void SetParent(CUIComponent value)
      {
        if (_Parent == value) return;

        if (_Parent != null)
        {
          PropogateTreeChanged();

          _Parent.AKAPart.Forget(Self);
          _Parent.Tree.Children.Remove(Self);

          Self.Layout.RequireChildrenUpdate = true;
          _Parent.Tree.OnChildRemoved(Self);
          Self.Tree.OnDetachFromParent(_Parent);
        }

        _Parent = value;

        if (_Parent != null)
        {
          PropogateTreeChanged();
          if (Self.AKA != null) _Parent.AKAPart.Remember(Self);
          // parent.PassPropsToChild(this);
          _Parent.Layout.RequireChildrenUpdate = true;
          _Parent.Tree.OnChildAdded(Self);
          Self.Tree.OnAttachToParent(_Parent);
        }
      }


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

      public CUIComponent Append(CUIComponent child, string name = null)
      {
        if (child is null) return null;

        Children.Add(child);
        child.Tree.Parent = Self;
        if (name != null) Self.AKAPart.Remember(child, name);
        return child;
      }

      public CUIComponent Prepend(CUIComponent child, string name = null)
      {
        if (child is null) return null;

        Children.Insert(0, child);
        child.Tree.Parent = Self;
        if (name != null) Self.AKAPart.Remember(child, name);
        return child;
      }

      public CUIComponent Insert(CUIComponent child, int index, string name = null)
      {
        if (child is null) return null;

        index = Math.Clamp(index, 0, Children.Count);
        Children.Insert(index, child);
        child.Tree.Parent = Self;
        if (name != null) Self.AKAPart.Remember(child, name);
        return child;
      }
      public void RemoveSelf() => Parent?.RemoveChild(Self);
      public void RemoveChild(CUIComponent child)
      {
        Children.Remove(child);
        child.Tree.Parent = null;
      }

      public void RemoveAllChildren()
      {
        foreach (CUIComponent child in Children)
        {
          child.Tree._Parent = null;
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