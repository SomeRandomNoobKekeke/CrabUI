using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public class TreeModule : IModule
    {

      public bool TreeChanged { get; set; }
      public event Action OnTreeChanged;

      public CUIComponent Parent { get; set; }
      public CUIComponent Host { get; set; }
      private List<CUIComponent> children { get; } = new();
      public IReadOnlyList<CUIComponent> Children { get; }

      public TreeModule()
      {
        Children = children.AsReadOnly();
      }


      public virtual void OnChildAdded(CUIComponent child) { }
      public virtual void OnChildRemoved(CUIComponent child) { }
      public virtual void OnAttachToParent(CUIComponent parent)
      {
        Host.MainComponentTracker.OnAttachedTo(parent);
      }

      public virtual void OnDetachFromParent(CUIComponent parent)
      {
        Host.MainComponentTracker.OnDetached();
      }

      public void AddChild(CUIComponent child)
      {
        children.Add(child);
        child.Tree.Parent = this.Host;
        PropogateTreeChanged();

        OnChildAdded(child);
        child.Tree.OnAttachToParent(this.Host);
      }

      public void RemoveChild(CUIComponent child)
      {
        children.Remove(child);
        child.Tree.Parent = null;
        PropogateTreeChanged();

        OnChildRemoved(child);
        child.Tree.OnDetachFromParent(this.Host);
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