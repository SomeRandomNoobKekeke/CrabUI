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
    /// <summary>
    /// This is just convenient accessor, it doesn't do the work
    /// </summary>
    public class ChildrenListProxy : Part, IEndPart, IList<CUIVisualComponent>
    {
      private IList<CUIVisualComponent> Children => Self.ChildrenContainer;
      private TreeOperations_Part Operations => Self.TreeOperations;

      public IEnumerable<CUIVisualComponent> AddBulk
      {
        set
        {
          foreach (CUIVisualComponent child in value)
          {
            Operations.AddChild(child);
          }
        }
      }

      public CUIVisualComponent this[int i]
      {
        get => Children[i];
        set => Operations.SetChild(i, value);
      }

      public int Count => Children.Count;
      public bool IsReadOnly => false;

      public void MoveChildTo(CUIVisualComponent child, int i) => Operations.MoveChildTo(child, i);
      public void Add(CUIVisualComponent child) => Operations.AddChild(child);
      public void Clear() => Operations.RemoveAllChildren();
      public bool Contains(CUIVisualComponent child) => Children.Contains(child);
      public void CopyTo(CUIVisualComponent[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
      public IEnumerator<CUIVisualComponent> GetEnumerator() => Children.GetEnumerator();
      public int IndexOf(CUIVisualComponent child) => Children.IndexOf(child);

      public void Insert(int i, CUIVisualComponent child) => Operations.InsertChild(i, child);
      public bool Remove(CUIVisualComponent child)
      {
        Operations.RemoveChild(child);
        return true; //BRUH
      }
      public void RemoveAt(int i) => Operations.RemoveChildAt(i);

      IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();

      public override string ToString() => Logger.Wrap.IEnumerable(Children);
    }
  }
}