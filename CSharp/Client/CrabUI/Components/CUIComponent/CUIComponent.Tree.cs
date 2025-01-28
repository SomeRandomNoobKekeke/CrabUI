using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent
  {
    #region Tree --------------------------------------------------------

    public List<CUIComponent> Children { get; set; } = new();

    private CUIComponent? parent; public CUIComponent? Parent
    {
      get => parent;
      set => SetParent(value);
    }

    //TODO DRY
    internal void SetParent(CUIComponent? value, [CallerMemberName] string memberName = "")
    {
      if (parent != null)
      {
        TreeChanged = true;
        OnPropChanged();
        parent.Forget(this);
        parent.Children.Remove(this);
        parent.OnChildRemoved?.Invoke(this);
      }

      parent = value;

      CUIDebug.Capture(null, this, "SetParent", memberName, "parent", $"{parent}");

      if (parent != null)
      {
        if (parent is CUIMainComponent main) MainComponent = main;
        if (parent?.MainComponent != null) MainComponent = parent.MainComponent;

        parent.Children.Add(this);
        TreeChanged = true;
        parent.PassPropsToChild(this);
        OnPropChanged();
        parent.OnChildAdded?.Invoke(this);
      }
    }


    private bool treeChanged = true; internal bool TreeChanged
    {
      get => treeChanged;
      set { treeChanged = value; if (value && Parent != null) Parent.TreeChanged = true; }
    }

    /// <summary>
    /// Allows you to add array of children
    /// </summary>
    public IEnumerable<CUIComponent> AddChildren
    {
      set
      {
        foreach (CUIComponent c in value) { Append(c, c.AKA); }
      }
    }

    public event Action<CUIComponent> OnChildAdded;
    public event Action<CUIComponent> OnChildRemoved;

    //TODO DRY
    /// <summary>
    /// Adds children to the end of the list
    /// </summary>
    /// <param name="child"></param>
    /// <param name="name"> AKA </param>
    /// <returns> child </returns>
    public virtual CUIComponent Append(CUIComponent child, string name = null, [CallerMemberName] string memberName = "")
    {
      if (child == null) return child;

      if (child.parent != null)
      {
        child.TreeChanged = true;
        child.OnPropChanged();
        child.parent.Forget(child);
        child.parent.Children.Remove(child);
        child.parent.OnChildRemoved?.Invoke(child);
      }

      child.parent = this;

      CUIDebug.Capture(null, this, "Append", memberName, "child", $"{child}");

      if (this != null) // kek
      {
        if (this is CUIMainComponent main) child.MainComponent = main;
        if (this.MainComponent != null) child.MainComponent = this.MainComponent;

        Children.Add(child);
        child.TreeChanged = true;
        if (name != null) Remember(child, name);
        PassPropsToChild(child);
        child.OnPropChanged();
        OnChildAdded?.Invoke(child);
      }

      return child;
    }

    //TODO DRY
    /// <summary>
    /// Adds children to the begining of the list
    /// </summary>
    /// <param name="child"></param>
    /// <param name="name"> AKA </param>
    /// <returns> child </returns>
    public virtual CUIComponent Prepend(CUIComponent child, string name = null, [CallerMemberName] string memberName = "")
    {
      if (child == null) return child;

      if (child.parent != null)
      {
        child.TreeChanged = true;
        child.OnPropChanged();
        child.parent.Forget(child);
        child.parent.Children.Remove(child);
        child.parent.OnChildRemoved?.Invoke(child);
      }

      child.parent = this;

      CUIDebug.Capture(null, this, "Prepend", memberName, "child", $"{child}");

      if (this != null) // kek
      {
        if (this is CUIMainComponent main) child.MainComponent = main;
        if (this.MainComponent != null) child.MainComponent = this.MainComponent;

        Children.Insert(0, child);
        child.TreeChanged = true;
        if (name != null) Remember(child, name);
        PassPropsToChild(child);
        child.OnPropChanged();
        OnChildAdded?.Invoke(child);
      }

      return child;
    }

    //TODO DRY
    public void RemoveSelf() => Parent?.RemoveChild(this);
    public CUIComponent RemoveChild(CUIComponent child, [CallerMemberName] string memberName = "")
    {
      if (child == null || !Children.Contains(child)) return child;

      if (this != null) // kek
      {
        child.TreeChanged = true;
        child.OnPropChanged();
        Forget(child);
        Children.Remove(child);
        OnChildRemoved?.Invoke(child);
      }

      child.parent = null;

      CUIDebug.Capture(null, this, "RemoveChild", memberName, "child", $"{child}");

      return child;
    }


    //TODO DRY
    public void RemoveAllChildren([CallerMemberName] string memberName = "")
    {
      foreach (CUIComponent c in Children)
      {
        if (this != null) // kek
        {
          c.TreeChanged = true;
          c.OnPropChanged();
          //Forget(c);
          //Children.Remove(c);
          OnChildRemoved?.Invoke(c);
        }

        c.parent = null;

        CUIDebug.Capture(null, this, "RemoveAllChildren", memberName, "child", $"{c}");
      }

      NamedComponents.Clear();
      Children.Clear();
    }


    /// <summary>
    /// Pass props like ZIndex, Visible to a new child
    /// </summary>
    /// <param name="child"></param>
    protected virtual void PassPropsToChild(CUIComponent child)
    {
      if (!ShouldPassPropsToChildren) return;

      if (ZIndex.HasValue && !child.IgnoreParentZIndex) child.ZIndex = ZIndex.Value + 1;
      if (IgnoreEvents && !child.IgnoreParentEventIgnorance) child.IgnoreEvents = true;
      if (!Visible && !child.IgnoreParentVisibility) child.Visible = false;
    }

    #endregion
  }
}