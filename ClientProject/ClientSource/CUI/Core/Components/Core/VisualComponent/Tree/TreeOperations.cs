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
namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    protected TreeOperations_Part TreeOperations { get; } = new();
    public class TreeOperations_Part : Part
    {
      private void ValidateI(int i)
      {
        if (i < 0 || i >= Self.ChildrenContainer.Count) throw new ArgumentException($"{Self}| child index out of bounds [{i}]");
      }

      public void RemoveChild(CUIVisualComponent child)
      {
        Self.ChildrenContainer.Remove(child);
        child._Parent = null;

        child.Tree.OnDetachFromParent(Self);
        Self.Tree.OnChildRemoved(child);
      }

      public void SetChild(int i, CUIVisualComponent newChild)
      {
        if (newChild is null)
        {
          RemoveChildAt(i); return;
        }

        ValidateI(i);

        CUIVisualComponent prevchild = Self.ChildrenContainer[i];

        newChild._Parent?.TreeOperations.RemoveChild(newChild);

        prevchild._Parent = null;
        newChild._Parent = Self;
        Self.ChildrenContainer[i] = newChild;

        prevchild.Tree.OnDetachFromParent(Self);
        Self.Tree.OnChildRemoved(prevchild);
        newChild.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(newChild);
      }

      public void InsertChild(int i, CUIVisualComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);
        if (i < 0) throw new ArgumentException($"{Self}| child insert index out of bounds [{i}]");

        child._Parent?.TreeOperations.RemoveChild(child);
        child._Parent = Self;
        Self.ChildrenContainer.Insert(i, child);

        child.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(child);
      }

      public void MoveChildTo(CUIVisualComponent child, int i)
      {
        ArgumentNullException.ThrowIfNull(child);
        ValidateI(i);

        if (!Self.ChildrenContainer.Remove(child))
        {
          throw new ArgumentException($"{child} is not in the child list");
        }

        Self.ChildrenContainer.Insert(i, child);
        Self.Tree.OnChildrenRearranged();
      }

      public void AddChild(CUIVisualComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);

        child._Parent?.TreeOperations.RemoveChild(child);

        child._Parent = Self;
        Self.ChildrenContainer.Add(child);

        child.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(child);
      }

      public void RemoveChildAt(int i)
      {
        ValidateI(i);

        CUIVisualComponent child = Self.ChildrenContainer[i];
        Self.ChildrenContainer.RemoveAt(i);
        child._Parent = null;

        child.Tree.OnDetachFromParent(Self);
        Self.Tree.OnChildRemoved(child);
      }

      public void SetParent(CUIVisualComponent newParent)
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
        for (int i = Self.ChildrenContainer.Count - 1; i >= 0; i--)
        {
          RemoveChildAt(i);
        }
      }

      /// <summary>
      /// Very cursed
      /// </summary>
      public void AttachChild(CUIVisualComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);

        child._Parent?.TreeOperations.RemoveChild(child);

        child._Parent = Self;
        // Self._Children.Add(child);

        child.Tree.OnAttachToParent(Self);
        Self.Tree.OnChildAdded(child);
      }
    }
  }
}