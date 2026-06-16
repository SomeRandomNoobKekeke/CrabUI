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

namespace CrabUI
{

  public abstract class Layout
  {
    public interface Host
    {
      public IReadOnlyList<Child> Children { get; }
      public Vector2 ChildrenOffset { get; }
      public bool CullChildren { get; }
      public CUIRect Rect { get; set; }
      public CUIBool2 FitContent { get; }

      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }

      void NotifyVisualsRestructured();
    }
    public interface ChildBase
    {
      public CUIRect Rect { get; set; }
      public bool CulledOut { get; set; }

      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }
    }
    public interface Child : ChildBase, CUIVerticalListLayout.Child, PlainLayout.Child
    {

    }

    private Host Parent;
    public virtual void ConnectTo(Host host)
    {
      Parent = host;
    }




    public bool RequireChildrenUpdate { get; set; } = true;
    public bool RequireParentUpdate { get; set; } = true;

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