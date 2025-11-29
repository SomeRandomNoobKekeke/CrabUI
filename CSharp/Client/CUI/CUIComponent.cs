using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public partial class CUIComponent : CUIComponentCore, IVisibleTreeNode
  {
    public List<IVisibleTreeNode> TopChildren { get; } = new();
    public List<IVisibleTreeNode> Children { get; } = new();



    public void Draw()
    {

    }

    private void InitModules()
    {

    }

    public CUIComponent()
    {

    }


    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}