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
    protected TreeOperations_Part TreeOperations { get; } = new();
    public class TreeOperations_Part : Part
    {
      private void ValidateI(int i)
      {
        if (i < 0 || i >= Self._Children.Count) throw new ArgumentException($"{Self}| child index out of bounds [{i}]");
      }

      public void RemoveChild(CUIComponent child)
      {
        Self._Children.Remove(child);
        child._Parent = null;

        child.Tree.OnDetachFromParent(Self);
        Self.Tree.OnChildRemoved(child);
      }

      public void SetChild(int i, CUIComponent newChild)
      {
        if (newChild is null)
        {
          RemoveChildAt(i); return;
        }

        ValidateI(i);

        CUIComponent prevchild = Self._Children[i];

        newChild._Parent?.TreeOperations.RemoveChild(newChild);

        prevchild._Parent = null;
        newChild._Parent = Self;
        Self._Children[i] = newChild;

        prevchild.Tree.OnDetachFromParent(Self);
        Self.Tree.OnChildRemoved(prevchild);
        newChild.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(newChild);
      }

      public void InsertChild(int i, CUIComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);
        if (i < 0) throw new ArgumentException($"{Self}| child insert index out of bounds [{i}]");

        child._Parent?.TreeOperations.RemoveChild(child);
        child._Parent = Self;
        Self._Children.Insert(i, child);

        child.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(child);
      }

      public void MoveChildTo(CUIComponent child, int i)
      {
        ArgumentNullException.ThrowIfNull(child);
        ValidateI(i);

        if (!Self._Children.Remove(child))
        {
          throw new ArgumentException($"{child} is not in the child list");
        }

        Self._Children.Insert(i, child);
        Self.Tree.OnChildrenRearranged();
      }

      public void AddChild(CUIComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);

        child._Parent?.TreeOperations.RemoveChild(child);

        child._Parent = Self;
        Self._Children.Add(child);

        child.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(child);
      }

      public void RemoveChildAt(int i)
      {
        ValidateI(i);

        CUIComponent child = Self._Children[i];
        Self._Children.RemoveAt(i);
        child._Parent = null;

        child.Tree.OnDetachFromParent(Self);
        Self.Tree.OnChildRemoved(child);
      }

      public void SetParent(CUIComponent newParent)
      {
        if (newParent is null)
        {
          Self._Parent?.TreeOperations.RemoveChild(Self);
          return;
        }

        newParent.TreeOperations.AddChild(Self);
      }


      public void RemoveAllChildren()
      {
        for (int i = Self._Children.Count - 1; i >= 0; i--)
        {
          RemoveChildAt(i);
        }
      }
    }
  }
}