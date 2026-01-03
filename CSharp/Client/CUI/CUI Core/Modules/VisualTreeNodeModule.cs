using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class VisualTreeNodeModule : IModule
  {
    public IVisualComponent Host { get; }

    public bool TreeChanged { get; set; }
    public event Action OnTreeChanged;

    public IVisualComponent Parent
    {
      get => Host.Parent;
      set => Host.Parent = value;
    }

    private List<IVisualComponent> children = new List<IVisualComponent>();
    public ReadOnlyCollection<IVisualComponent> Children => children.AsReadOnly();

    public void AddChild(IVisualComponent child)
    {
      children.Add(child);
      child.TreeNodeModule.Parent = Host;
      PropogateTreeChanged();
    }

    public void RemoveChild(IVisualComponent child)
    {
      children.Remove(child);
      child.TreeNodeModule.Parent = null;
      PropogateTreeChanged();
    }

    private void PropogateTreeChanged()
    {
      TreeChanged = true;
      OnTreeChanged?.Invoke();
      Parent?.TreeNodeModule.PropogateTreeChanged();
    }

    public TreeNodeModule(IComponentTreeNode host) => Host = host;
  }
}