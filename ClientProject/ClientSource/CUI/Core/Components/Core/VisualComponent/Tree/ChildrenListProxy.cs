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
    public class ChildrenListProxy : Part, IEndPart, IList<CUIVisualComponent>, IList
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



      #region IList

      object? IList.this[int i]
      {
        get => this[i];
        set => this[i] = (CUIVisualComponent)value;
      }

      bool IList.IsFixedSize => false;
      bool IList.IsReadOnly => false;
      int ICollection.Count => Count;
      bool ICollection.IsSynchronized => (Children as IList).IsSynchronized;
      object ICollection.SyncRoot => (Children as IList).SyncRoot;

      int IList.Add(object? value)
      {
        Add((CUIVisualComponent)value);
        return Count - 1;
      }

      void IList.Clear() => Clear();

      bool IList.Contains(object? value) => Contains((CUIVisualComponent)value);

      void ICollection.CopyTo(Array array, int index)
      {
        throw new NotImplementedException("why are you using this?");
      }

      int IList.IndexOf(object? value) => IndexOf((CUIVisualComponent)value);

      void IList.Insert(int index, object? value) => Insert(index, (CUIVisualComponent)value);

      void IList.Remove(object? value) => Remove((CUIVisualComponent)value);

      void IList.RemoveAt(int index) => RemoveAt(index);
      #endregion

    }
  }
}