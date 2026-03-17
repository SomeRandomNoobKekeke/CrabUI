using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected virtual Tree_Part Tree { get; } = new();
    public class Tree_Part : Part, IModule
    {
      [In] public MainComponentTracker_Part MainComponentTracker { get; set; }

      public bool Changed { get; set; }
      public event Action OnChanged;

      public CUIComponent Parent { get; set; }
      public List<CUIComponent> Children { get; } = new();

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

        OnChildAdded(child);
        child.Tree.OnAttachToParent(Self);
      }

      public void RemoveChild(CUIComponent child)
      {
        Children.Remove(child);
        child.Tree.Parent = null;
        PropogateTreeChanged();

        OnChildRemoved(child);
        child.Tree.OnDetachFromParent(Self);
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