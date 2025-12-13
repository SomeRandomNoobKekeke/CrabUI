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
  public class TreeNodeModule : IModule
  {
    public IComponentTreeNode Host { get; }

    public bool TreeChanged { get; set; }
    public event Action OnTreeChanged;

    public IComponentTreeNode Parent { get; set; }


    private List<IComponentTreeNode> _topChildren { get; set; } = new();
    private List<IComponentTreeNode> _children { get; set; } = new();

    public ReadOnlyCollection<IComponentTreeNode> TopChildren => _topChildren.AsReadOnly();
    public ReadOnlyCollection<IComponentTreeNode> Children => _children.AsReadOnly();

    public void AddChild(IComponentTreeNode child)
    {
      _children.Add(child);
      child.TreeNodeModule.Parent = Host;
      PropogateTreeChanged();
    }

    public void RemoveChild(IComponentTreeNode child)
    {
      _children.Remove(child);
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