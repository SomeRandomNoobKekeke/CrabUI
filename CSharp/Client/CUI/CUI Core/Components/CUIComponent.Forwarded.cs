using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;


namespace CrabUI
{

  //TODO rethink
  public partial class CUIComponent
  {
    public IComponentTreeNode Parent
    {
      get => TreeNodeModule.Parent;
      set => TreeNodeModule.Parent = value;
    }
    public ReadOnlyCollection<IComponentTreeNode> TopChildren => TreeNodeModule.TopChildren;
    public ReadOnlyCollection<IComponentTreeNode> Children => TreeNodeModule.Children;

    public void AddChild(IComponentTreeNode child) => TreeNodeModule.AddChild(child);
    public void RemoveChild(IComponentTreeNode child) => TreeNodeModule.RemoveChild(child);
  }
}