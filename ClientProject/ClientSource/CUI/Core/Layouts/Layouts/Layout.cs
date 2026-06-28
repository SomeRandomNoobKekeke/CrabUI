using System;
using System.Collections;
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

  public abstract class Layout : IAware
  {
    public interface Host
    {
      public IReadOnlyList<Child> Children { get; }
      public Vector2 ChildrenOffset { get; }
      public bool CullChildren { get; }
      public CUIRect Rect { get; set; }
      public CUIRect OuterRect { get; set; }
      public CUIRect InnerRect { get; }

      public CUIBool2 FitContent { get; }

      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }

      void NotifyVisualsRestructured();
    }
    public interface ChildBase
    {
      public CUIRect Rect { get; set; }
      public CUIRect OuterRect { get; set; }
      public CUIRect InnerRect { get; }

      public bool CulledOut { get; set; }

      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }
    }
    public interface Child : ChildBase, CUIVerticalListLayout.Child, PlainLayout.Child
    {

    }

    public DebugNode<object, string> Debug_LayoutMarked { get; } = new(
      DebugCategory.LayoutMarked, CUI.DebugHub,
      (host, propName) => $"{host}.{propName} = true"
    );

    public object HostComponent { get; set; }
    public string HostPropName { get; set; }

    private Host Parent;
    public virtual void ConnectTo(Host host)
    {
      Parent = host;
    }



    private bool _RequireChildrenUpdate = true; public bool RequireChildrenUpdate
    {
      get => _RequireChildrenUpdate;
      set
      {
        _RequireChildrenUpdate = value;
        Debug_LayoutMarked.Send(HostComponent, "RequireChildrenUpdate");
      }
    }


    private bool _RequireParentUpdate = true; public bool RequireParentUpdate
    {
      get => _RequireParentUpdate;
      set
      {
        _RequireParentUpdate = value;
        Debug_LayoutMarked.Send(HostComponent, "RequireParentUpdate");
      }
    }

    public virtual void UpdateChildren()
    {
      if (Parent.CullChildren)
      {
        foreach (Child child in Parent.Children)
        {
          child.CulledOut = !child.Rect.Intersect(Parent.Rect);
        }

        Parent.NotifyVisualsRestructured();
      }

      RequireChildrenUpdate = false;
    }

    public virtual void UpdateParent()
    {
      RequireParentUpdate = false;
    }
  }
}