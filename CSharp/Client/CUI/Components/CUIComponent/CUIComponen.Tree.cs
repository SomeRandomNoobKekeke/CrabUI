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
    public virtual Tree_Part Tree { get; } = new();
    public class Tree_Part : Part
    {

      public virtual string Name => "CUIComponent";

      public bool TreeChanged { get; set; }
      public event Action OnTreeChanged;

      public CUIComponent Parent { get; set; }
      private List<CUIComponent> children { get; } = new();
      public IReadOnlyList<CUIComponent> Children { get; }

      public Tree_Part()
      {
        Children = children.AsReadOnly();
      }


      public virtual void OnChildAdded(CUIComponent child) { }
      public virtual void OnChildRemoved(CUIComponent child) { }
      public virtual void OnAttachToParent(CUIComponent parent)
      {
        // Host.MainComponentTracker.OnAttachedTo(parent);
      }

      public virtual void OnDetachFromParent(CUIComponent parent)
      {
        // Host.MainComponentTracker.OnDetached();
      }

      public void AddChild(CUIComponent child)
      {
        children.Add(child);
        child.Tree.Parent = Self;
        PropogateTreeChanged();

        OnChildAdded(child);
        child.Tree.OnAttachToParent(Self);
      }

      public void RemoveChild(CUIComponent child)
      {
        children.Remove(child);
        child.Tree.Parent = null;
        PropogateTreeChanged();

        OnChildRemoved(child);
        child.Tree.OnDetachFromParent(Self);
      }

      private void PropogateTreeChanged()
      {
        TreeChanged = true;
        OnTreeChanged?.Invoke();
        Parent?.Tree.PropogateTreeChanged();
      }


    }
  }
}