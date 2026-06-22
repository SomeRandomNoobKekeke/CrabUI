using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    #region Public
    #endregion
    public CUIComponent Parent => Tree.Parent;
    public IReadOnlyList<CUIComponent> Children => Tree.ReadOnlyChildren;
    public void Append(CUIComponent child, string name = null) => Tree.Append(child, name);
    public void Prepend(CUIComponent child, string name = null) => Tree.Prepend(child, name);
    public void Insert(CUIComponent child, int index, string name = null) => Tree.Insert(child, index, name);
    public void RemoveSelf() => Tree.RemoveSelf();
    public void RemoveChild(CUIComponent child) => Tree.RemoveChild(child);
    public void RemoveAt(int i) => Tree.RemoveAt(i);
    public void RemoveAllChildren() => Tree.RemoveAllChildren();

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

    #region Protected
    #endregion
    protected Tree_Part Tree { get; } = new();
    public class Tree_Part : Part, IModule
    {
      public void Init()
      {
        ReadOnlyChildren = Children.AsReadOnly();

        DebugRelay.Route(Debug_ChildAdded);
        DebugRelay.Route(Debug_ChildRemoved);
      }

      public DebugRelay DebugRelay { get; } = new();

      public DebugNode<CUIComponent, CUIComponent> Debug_ChildAdded = new(
        DebugCategory.TreeChanged, CUI.DebugHub,
        (parent, child) => $"{parent} <= {child}"
      );
      public DebugNode<CUIComponent, CUIComponent> Debug_ChildRemoved = new(
        DebugCategory.TreeChanged, CUI.DebugHub,
        (parent, child) => $"{parent} => {child}"
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


      private void PropogateTreeChanged()
      {
        Changed = true;
        OnChanged.Raise();
        Parent?.Tree.PropogateTreeChanged();
      }


    }
  }
}