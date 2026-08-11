using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Collections;
namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    private CUIVisualComponent _Parent; public CUIVisualComponent Parent
    {
      get => _Parent;
      set => TreeOperations.SetParent(value);
    }
    private List<CUIVisualComponent> _Children = new();
    public ChildrenListProxy Children { get; } = new();

    public CUIVisualComponent this[int i]
    {
      get => Children[i];
      set => Children[i] = value;
    }

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

    public IEnumerable<CUIVisualComponent> DeepChildren
    {
      get
      {
        foreach (CUIVisualComponent child in Children)
        {
          yield return child;
          foreach (CUIVisualComponent deepChild in child.DeepChildren)
          {
            yield return deepChild;
          }
        }
      }
    }
  }
}