using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using System.Collections;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public bool TreeChanged { get; set; }
    public event Action OnTreeChanged;

    public CUIComponent Parent { get; set; }

    protected List<CUIComponent> children = new();
    public ReadOnlyCollection<CUIComponent> Children => children.AsReadOnly();


    protected virtual void OnChildAdded(CUIComponent child) { }
    protected virtual void OnChildRemoved(CUIComponent child) { }
    protected virtual void OnAttachToParent(CUIComponent parent)
    {
      Protected.MainComponentTracker.OnAttachedTo(parent);
    }

    protected virtual void OnDetachFromParent(CUIComponent parent)
    {
      Protected.MainComponentTracker.OnDetached();
    }

    public void AddChild(CUIComponent child)
    {
      children.Add(child);
      child.Parent = this;
      PropogateTreeChanged();

      OnChildAdded(child);
      child.OnAttachToParent(this);
    }

    public void RemoveChild(CUIComponent child)
    {
      children.Remove(child);
      child.Parent = null;
      PropogateTreeChanged();

      OnChildRemoved(child);
      child.OnDetachFromParent(this);
    }

    private void PropogateTreeChanged()
    {
      TreeChanged = true;
      OnTreeChanged?.Invoke();
      Parent?.PropogateTreeChanged();
    }
  }
}