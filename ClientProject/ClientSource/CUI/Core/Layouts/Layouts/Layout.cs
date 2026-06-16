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
    public interface TargetParent
    {
      public CUIRect Rect { get; set; }
      public IReadOnlyList<TargetChild> Children { get; }
      public bool CullChildren { get; }

      void NotifyVisualsRestructured();
    }

    public interface TargetChild
    {
      public CUIRect Rect { get; set; }
      public bool CulledOut { get; set; }
    }



    public bool RequireChildrenUpdate { get; set; } = true;
    public bool RequireParentUpdate { get; set; } = true;

    public virtual void UpdateChildren()
    {
      if (Host.CullChildren)
      {
        foreach (Target child in Host.Children)
        {
          child.CulledOut = !child.Rect.Intersect(Host.Rect);
        }

        Host.NotifyVisualsRestructured();
      }

      RequireChildrenUpdate = false;
    }

    public virtual void UpdateParent()
    {
      RequireParentUpdate = false;
    }
  }
}