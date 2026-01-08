using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public abstract partial class CUIVisualComponent : CUIComponentCore, IVisualComponent
  {
    public bool TreeChanged { get; set; }
    public event Action OnTreeChanged;

    public CUIVisualComponent Parent { get; set; }

    protected List<CUIVisualComponent> children = new();
    public ReadOnlyCollection<CUIVisualComponent> Children => children.AsReadOnly();

    public void AddChild(CUIVisualComponent child)
    {
      children.Add(child);
      child.Parent = this;
      PropogateTreeChanged();
    }

    public void RemoveChild(CUIVisualComponent child)
    {
      children.Remove(child);
      child.Parent = null;
      PropogateTreeChanged();
    }

    private void PropogateTreeChanged()
    {
      TreeChanged = true;
      OnTreeChanged?.Invoke();
      Parent?.PropogateTreeChanged();
    }
  }
}