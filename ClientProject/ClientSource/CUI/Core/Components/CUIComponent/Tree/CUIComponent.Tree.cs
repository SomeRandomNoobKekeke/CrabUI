using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;
using System.Collections;
namespace CrabUI
{
  public partial class CUIComponent
  {
    private CUIComponent _Parent; public CUIComponent Parent
    {
      get => _Parent;
      set => TreeOperations.SetParent(value);
    }
    private List<CUIComponent> _Children = new();
    public ChildrenListProxy Children { get; } = new();

    public void RemoveSelf() => Parent = null;
    public void MoveToTop()
    {
      if (Parent is null || Parent.Children.Count == 0) return;
      Parent.Children.MoveChildTo(this, Parent.Children.Count - 1);
    }
    public void MoveToBottom()
    {
      if (Parent is null || Parent.Children.Count == 0) return;
      Parent.Children.MoveChildTo(this, 0);
    }

    public IEnumerable<CUIComponent> DeepChildren
    {
      get
      {
        foreach (CUIComponent child in Children)
        {
          yield return child;
          foreach (CUIComponent deepChild in child.DeepChildren)
          {
            yield return deepChild;
          }
        }
      }
    }
  }
}