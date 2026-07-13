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
    #region Public
    #endregion
    public CUIComponent Parent
    {
      get => Tree.Parent;
      set => Tree.Parent = value;
    }
    //TODO make some As<T> extention to Children

    public void Append(CUIComponent child, string name = null) => Tree.Append(child, name);
    public void Prepend(CUIComponent child, string name = null) => Tree.Prepend(child, name);
    public void Insert(CUIComponent child, int index, string name = null) => Tree.Insert(child, index, name);
    public void RemoveSelf() => Tree.RemoveSelf();
    public void RemoveChild(CUIComponent child) => Tree.RemoveChild(child);
    public void RemoveAt(int i) => Tree.RemoveAt(i);
    public void RemoveAllChildren() => Tree.RemoveAllChildren();
    public void MoveChildTo(CUIComponent child, int i) => Tree.MoveChildTo(child, i);

    public void MoveToTop() => Parent?.MoveChildTo(this, 0);
    public void MoveToBottom()
    {
      if (Parent is null) return;
      Parent.MoveChildTo(this, Parent.Children.Count - 1);
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
        foreach (CUIComponent child in Tree.Children)
        {
          yield return child;
          foreach (CUIComponent deepChild in child.DeepChildren)
          {
            yield return deepChild;
          }
        }
      }
    }

    public IReadOnlyList<CUIComponent> Children => Tree.ReadOnlyChildren;

    public ChildrenListProxy Children2 { get; } = new();
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


    #region Protected
    #endregion
    protected Tree_Part Tree { get; } = new();
    public class Tree_Part : Part, IModule
    {
      public void Init()
      {
        ReadOnlyChildren = Children.AsReadOnly();

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




      public List<CUIComponent> Children { get; } = new();
      public IReadOnlyList<CUIComponent> ReadOnlyChildren { get; private set; }

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
          _Parent.Tree.Children.Remove(Self);

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

      public CUIComponent Append(CUIComponent child, string name = null)
      {
        if (child is null) return null;

        Children.Add(child);
        child.Tree.Parent = Self;
        if (name != null) Self.Remember(child, name);
        return child;
      }

      public CUIComponent Prepend(CUIComponent child, string name = null)
      {
        if (child is null) return null;

        Children.Insert(0, child);
        child.Tree.Parent = Self;
        if (name != null) Self.Remember(child, name);
        return child;
      }

      public CUIComponent Insert(CUIComponent child, int index, string name = null)
      {
        if (child is null) return null;

        index = Math.Clamp(index, 0, Children.Count);
        Children.Insert(index, child);
        child.Tree.Parent = Self;
        if (name != null) Self.Remember(child, name);
        return child;
      }
      public void RemoveSelf() => Parent?.RemoveChild(Self);
      public void RemoveChild(CUIComponent child)
      {
        child.Tree.Parent = null;
      }

      public void RemoveAt(int i)
      {
        if (0 > i || i >= Children.Count) return;
        Children[i].Tree.Parent = null;
      }

      public void RemoveAllChildren()
      {
        foreach (CUIComponent child in Children)
        {
          child.Tree._Parent = null;
          OnChildRemoved.Raise(child);
          child.Tree.OnDetachFromParent.Raise(Self);
        }

        PropogateTreeChanged();
        Self.Layout.RequireChildrenUpdate = true;
        Children.Clear();
      }

      public void MoveChildTo(CUIComponent child, int i)
      {
        Children.Remove(child);
        Children.Insert(i, child);

        PropogateTreeChanged();
        Self.LayoutMarker.Mark(MarkPattern);
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