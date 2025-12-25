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
  public partial class CUIComponent : CUIComponentCore, IDrawable, IComponentTreeNode
  {

    public TreeNodeModule TreeNodeModule { get; private set; }
    public SimpleDrawModule DrawModule { get; private set; }

    protected override void InitModules()
    {
      DrawModule = new SimpleDrawModule();
      TreeNodeModule = new TreeNodeModule(this);
    }

    public void Draw(CUISpriteBatch spriteBatch)
    {
      DrawModule.Draw(spriteBatch);
    }

    #region Forwarded Props
    #endregion
    public Rectangle DrawRect
    {
      get => DrawModule.DrawRect;
      set => DrawModule.DrawRect = value;
    }

    public event Action OnTreeChanged
    {
      add => TreeNodeModule.OnTreeChanged += value;
      remove => TreeNodeModule.OnTreeChanged -= value;
    }

    public IComponentTreeNode Parent
    {
      get => TreeNodeModule.Parent;
      set => TreeNodeModule.Parent = value;
    }
    public ReadOnlyCollection<IComponentTreeNode> Children => TreeNodeModule.Children;

    public void AddChild(IComponentTreeNode child) => TreeNodeModule.RemoveChild(child);
    public void RemoveChild(IComponentTreeNode child) => TreeNodeModule.RemoveChild(child);

  }
}