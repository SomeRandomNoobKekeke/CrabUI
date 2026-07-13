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
    public CUIComponent Parent
    {
      get => Tree.Parent;
      set => Tree.Parent = value;
    }
    public ChildrenListProxy Children { get; } = new();

    public void RemoveSelf() => Parent?.Children.Remove(this);
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

    public Dictionary<string, CUIComponent> NamedChildren
    {
      set
      {
        foreach (var (name, component) in value)
        {
          this[name] = component;
        }
      }
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

    //---------------------------------------------------------------------------
    #region ChildrenListProxy
    #endregion
    public class ChildrenListProxy : Part, IList<CUIComponent>
    {
      private List<CUIComponent> _Children = new();


      public CUIComponent this[int i]
      {
        get => _Children[i];
        set
        {
          if (value is null) return;

          _Children[i] = value;
          value.Parent = Self;
        }
      }

      public int Count => _Children.Count;
      public bool IsReadOnly => false;

      public void MoveChildTo(CUIComponent child, int i)
      {
        ArgumentNullException.ThrowIfNull(child);
        Remove(child);
        Insert(i, child);
      }

      public void Add(CUIComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);
        _Children.Add(child);
        child.Parent = Self;
      }

      //TODO this can be optimized, but not now
      public void Clear()
      {
        foreach (CUIComponent child in _Children)
        {
          child.Parent = null;
        }

        _Children.Clear();
      }

      public bool Contains(CUIComponent child) => _Children.Contains(child);
      public void CopyTo(CUIComponent[] array, int arrayIndex) => _Children.CopyTo(array, arrayIndex);
      public IEnumerator<CUIComponent> GetEnumerator() => _Children.GetEnumerator();
      public int IndexOf(CUIComponent child) => _Children.IndexOf(child);

      public void Insert(int i, CUIComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);
        _Children.Insert(i, child);
        child.Parent = Self;
      }

      public bool Remove(CUIComponent child)
      {
        ArgumentNullException.ThrowIfNull(child);
        bool result = _Children.Remove(child);
        if (result) child.Parent = null;
        return result;
      }

      public void RemoveAt(int i)
      {
        CUIComponent child = _Children[i];
        child.Parent = null;
        _Children.RemoveAt(i);
      }

      IEnumerator IEnumerable.GetEnumerator() => _Children.GetEnumerator();
    }

    //---------------------------------------------------------------------------
    #region Tree_Part
    #endregion

    protected Tree_Part Tree { get; } = new();
    public class Tree_Part : Part, IModule
    {
      public void Init()
      {
        Debug_ChildAdded.Map(Self.DebugRelays[DebugCategory.TreeChanged]);
        Debug_ChildRemoved.Map(Self.DebugRelays[DebugCategory.TreeChanged]);

        Debug_LayoutMarked.Map(Self.DebugRelays[DebugCategory.LayoutMarked]);
      }


      public DebugNode<CUIComponent, CUIComponent> Debug_ChildAdded = new(
        DebugCategory.TreeChanged, CUI.DebugHub,
        (parent, child) => $"{parent} <= {child}"
      );
      public DebugNode<CUIComponent, CUIComponent> Debug_ChildRemoved = new(
        DebugCategory.TreeChanged, CUI.DebugHub,
        (parent, child) => $"{parent} => {child}"
      );

      public DebugNode<CUIComponent, LayoutMarker.Pattern, string> Debug_LayoutMarked { get; } = new(
        DebugCategory.LayoutMarked, CUI.DebugHub,
        (host, pattern, reason) => $"{host} {reason} {pattern}"
      );

      public LayoutMarker.Pattern MarkPattern { get; } = LayoutMarker.Pattern.UpAndDown;


      private CUIComponent _Parent; public CUIComponent Parent
      {
        get => _Parent;
        set => SetParent(value);
      }

      public bool Changed { get; set; }
      public ClearableEvent OnChanged { get; } = new();
      public ClearableEvent<CUIComponent> OnChildAdded { get; } = new();
      public ClearableEvent<CUIComponent> OnChildRemoved { get; } = new();
      public ClearableEvent<CUIComponent> OnAttachToParent { get; } = new();
      public ClearableEvent<CUIComponent> OnDetachFromParent { get; } = new();

      private void SetParent(CUIComponent value)
      {
        if (_Parent == value) return;

        if (_Parent != null)
        {
          PropogateTreeChanged();

          _Parent.Forget(Self);
          _Parent.Children.Remove(Self);

          _Parent.LayoutMarker.Mark(MarkPattern);
          Debug_LayoutMarked.Send(_Parent, MarkPattern, "Detaching old parent");

          _Parent.Tree.OnChildRemoved.Raise(Self);
          Self.Tree.OnDetachFromParent.Raise(_Parent);

          _Parent.Tree.Debug_ChildRemoved.Send(_Parent, Self);
        }

        _Parent = value;

        if (_Parent != null)
        {
          PropogateTreeChanged();
          if (Self.AKA != null) _Parent.Remember(Self);
          // parent.PassPropsToChild(this);

          _Parent.LayoutMarker.Mark(MarkPattern);
          Debug_LayoutMarked.Send(_Parent, MarkPattern, "Attaching new parent");
          _Parent.Tree.OnChildAdded.Raise(Self);
          Self.Tree.OnAttachToParent.Raise(_Parent);

          _Parent.Tree.Debug_ChildAdded.Send(_Parent, Self);
        }
      }

      private void PropogateTreeChanged()
      {
        Changed = true;
        OnChanged.Raise();
        Parent?.Tree.PropogateTreeChanged();
      }


    }
  }
}