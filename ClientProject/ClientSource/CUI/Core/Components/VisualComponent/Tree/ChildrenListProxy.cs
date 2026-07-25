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
    /// <summary>
    /// This is just convenient accessor, it doesn't do the work
    /// </summary>
    public class ChildrenListProxy : Part, IList<CUIComponent>
    {
      public List<CUIComponent> Children => Self._Children;
      public TreeOperations_Part Operations => Self.TreeOperations;

      public CUIComponent this[int i]
      {
        get => Children[i];
        set => Operations.SetChild(i, value);
      }

      public int Count => Children.Count;
      public bool IsReadOnly => false;

      public void MoveChildTo(CUIComponent child, int i) => Operations.MoveChildTo(child, i);
      public void Add(CUIComponent child) => Operations.AddChild(child);
      public void Clear() => Operations.RemoveAllChildren();
      public bool Contains(CUIComponent child) => Children.Contains(child);
      public void CopyTo(CUIComponent[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
      public IEnumerator<CUIComponent> GetEnumerator() => Children.GetEnumerator();
      public int IndexOf(CUIComponent child) => Children.IndexOf(child);

      public void Insert(int i, CUIComponent child) => Operations.InsertChild(i, child);
      public bool Remove(CUIComponent child)
      {
        Operations.RemoveChild(child);
        return true; //BRUH
      }
      public void RemoveAt(int i) => Operations.RemoveChildAt(i);

      IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();
    }
  }
}