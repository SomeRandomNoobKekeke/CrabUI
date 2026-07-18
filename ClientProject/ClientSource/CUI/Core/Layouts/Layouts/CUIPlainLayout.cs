using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public class CUIPlainLayout : Layout
  {
    public interface Host : Layout.Host
    {
      public Func<CUIRect, CUIBoundaries> ChildrenBounds { get; }
    }
    public interface Child : Layout.ChildBase
    {
      public CUINullRect Absolute { get; }
      public CUINullRect AbsoluteMin { get; }
      public CUINullRect AbsoluteMax { get; }
      public CUINullRect Relative { get; }
      public CUINullRect RelativeMin { get; }
      public CUINullRect RelativeMax { get; }
      public CUINullRect CrossRelative { get; }

      public CUISizes OutToChildDiff { get; }

      public Vector2 Anchor { get; }
      public Vector2? ParentAnchor { get; }
    }

    private Host Parent;
    public override void ConnectTo(Layout.Host host)
    {
      base.ConnectTo(host);
      Parent = host as Host;
    }



    public override void UpdateChildren()
    {
      if (Parent is null) return;
      if (!RequireChildrenUpdate) return;

      foreach (Layout.Child c in Parent.Children)
      {
        // Offset to anchor pos
        float x, y, w, h;

        x = 0;
        if (c.Relative.Left.HasValue) x = c.Relative.Left.Value * Parent.ChildrenRect.Width;
        if (c.CrossRelative.Left.HasValue) x = c.CrossRelative.Left.Value * Parent.ChildrenRect.Height;
        if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;

        if (c.RelativeMin.Left.HasValue) x = Math.Max(x, c.RelativeMin.Left.Value * Parent.ChildrenRect.Width);
        if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);

        if (c.RelativeMax.Left.HasValue) x = Math.Min(x, c.RelativeMax.Left.Value * Parent.ChildrenRect.Width);
        if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);


        y = 0;
        if (c.Relative.Top.HasValue) y = c.Relative.Top.Value * Parent.ChildrenRect.Height;
        if (c.CrossRelative.Top.HasValue) y = c.CrossRelative.Top.Value * Parent.ChildrenRect.Width;
        if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;

        if (c.RelativeMin.Top.HasValue) y = Math.Max(y, c.RelativeMin.Top.Value * Parent.ChildrenRect.Height);
        if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);

        if (c.RelativeMax.Top.HasValue) y = Math.Min(y, c.RelativeMax.Top.Value * Parent.ChildrenRect.Height);
        if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);

        w = 0;
        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Parent.ChildrenRect.Width;
        if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Parent.ChildrenRect.Height;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Parent.ChildrenRect.Width);
        if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
        if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value + c.OutToChildDiff.FullWidth);

        if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Parent.ChildrenRect.Width);
        if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
        if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value); //TODO should MaxSize use OutToChildDiff?, why are they used differently

        h = 0;
        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Parent.ChildrenRect.Height;
        if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Parent.ChildrenRect.Width;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Parent.ChildrenRect.Height);
        if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
        if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value + c.OutToChildDiff.FullHeigth);

        if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Parent.ChildrenRect.Height);
        if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
        if (c.MaxSize.Y.HasValue) h = Math.Min(h, c.MaxSize.Y.Value);

        Vector2 anchorPos = CUIAnchor.ChildPosIn(
          Parent.ChildrenRect.Size,
          c.ParentAnchor ?? c.Anchor,
          new Vector2(w, h),
          c.Anchor
        );

        CUIRect rect = new CUIRect(
          anchorPos + new Vector2(x, y) + Parent.ChildrenRect.LeftTop + Parent.ChildrenOffset,
          new Vector2(w, h)
        );

        if (Parent.ChildrenBounds != null)
        {
          rect = Parent.ChildrenBounds(Parent.ChildrenRect).Check(rect);
        }

        c.OuterRect = rect;
      }

      base.UpdateChildren();
    }

    public override void UpdateParent()
    {
      if (Parent.FitContent.X)
      {
        float rightmostRight = 0;
        foreach (Child c in Parent.Children)
        {
          float x = 0;
          float w = 0;

          if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;
          if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);
          if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);

          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
          if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value + c.OutToChildDiff.FullWidth);
          if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);

          rightmostRight = Math.Max(rightmostRight, x + w);
        }

        Parent.MinSize = Parent.MinSize with { X = rightmostRight };
        Parent.MaxSize = Parent.MaxSize with { X = rightmostRight };
      }

      if (Parent.FitContent.Y)
      {
        float bottommostBottom = 0;
        foreach (Child c in Parent.Children)
        {
          float y = 0;
          float h = 0;

          if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;
          if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);
          if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);

          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
          if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value + c.OutToChildDiff.FullHeigth);
          if (c.MaxSize.Y.HasValue) h = Math.Min(h, c.MaxSize.Y.Value);

          bottommostBottom = Math.Max(bottommostBottom, y + h);
        }

        Parent.MinSize = Parent.MinSize with { Y = bottommostBottom };
        Parent.MaxSize = Parent.MaxSize with { Y = bottommostBottom };
      }

      RequireParentUpdate = false;
    }
  }
}