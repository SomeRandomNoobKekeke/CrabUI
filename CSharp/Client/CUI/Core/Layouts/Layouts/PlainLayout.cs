using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public class PlainLayout : Layout
  {
    public interface Target : Layout.Target
    {
      public CUIRect Rect { get; set; }
      public CUINullRect Absolute { get; }
      public CUINullRect AbsoluteMin { get; }
      public CUINullRect AbsoluteMax { get; }
      public CUINullRect Relative { get; }
      public CUINullRect RelativeMin { get; }
      public CUINullRect RelativeMax { get; }
      public CUINullRect CrossRelative { get; }

      public CUIBool2 FitContent { get; }
      public CUINullVector2 MinSize { get; set; }
      public CUINullVector2 MaxSize { get; set; }

      public IReadOnlyList<Target> Children { get; }
      public Vector2 Anchor { get; }
      public Vector2? ParentAnchor { get; }
      public Vector2 ChildrenOffset { get; }
    }

    public override void InjectHost(Layout.Target host) { Host = host as Target; }
    public Target Host { get; private set; }

    public override void UpdateChildren()
    {
      if (Host is null) return;
      if (!RequireChildrenUpdate) return;

      foreach (Target c in Host.Children)
      {
        // Offset to anchor pos
        float x, y, w, h;

        x = 0;
        if (c.Relative.Left.HasValue) x = c.Relative.Left.Value * Host.Rect.Width;
        if (c.CrossRelative.Left.HasValue) x = c.CrossRelative.Left.Value * Host.Rect.Height;
        if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;

        if (c.RelativeMin.Left.HasValue) x = Math.Max(x, c.RelativeMin.Left.Value * Host.Rect.Width);
        if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);

        if (c.RelativeMax.Left.HasValue) x = Math.Min(x, c.RelativeMax.Left.Value * Host.Rect.Width);
        if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);


        y = 0;
        if (c.Relative.Top.HasValue) y = c.Relative.Top.Value * Host.Rect.Height;
        if (c.CrossRelative.Top.HasValue) y = c.CrossRelative.Top.Value * Host.Rect.Width;
        if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;

        if (c.RelativeMin.Top.HasValue) y = Math.Max(y, c.RelativeMin.Top.Value * Host.Rect.Height);
        if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);

        if (c.RelativeMax.Top.HasValue) y = Math.Min(y, c.RelativeMax.Top.Value * Host.Rect.Height);
        if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);

        w = 0;
        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Rect.Width;
        if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Host.Rect.Height;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Host.Rect.Width);
        if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
        if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value);

        if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Host.Rect.Width);
        if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
        if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);

        h = 0;
        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Rect.Height;
        if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Host.Rect.Width;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Host.Rect.Height);
        if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
        if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value);

        if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Host.Rect.Height);
        if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
        if (c.MaxSize.Y.HasValue) h = Math.Min(h, c.MaxSize.Y.Value);


        Vector2 anchorPos = CUIAnchor.ChildPosIn(
          Host.Rect.Size,
          c.ParentAnchor ?? c.Anchor,
          new Vector2(w, h),
          c.Anchor
        );

        c.Rect = new CUIRect(
          anchorPos + new Vector2(x, y) + Host.Rect.LeftTop + Host.ChildrenOffset,
          new Vector2(w, h)
        );
      }

      RequireChildrenUpdate = false;
    }

    public override void UpdateParent()
    {
      if (Host.FitContent.X)
      {
        float rightmostRight = 0;
        foreach (Target c in Host.Children)
        {
          float x = 0;
          float w = 0;

          if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;
          if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);
          if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);

          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
          if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value);
          if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);

          rightmostRight = Math.Max(rightmostRight, x + w);
        }

        Host.MinSize = Host.MinSize with { X = rightmostRight };
        Host.MaxSize = Host.MaxSize with { X = rightmostRight };
      }

      if (Host.FitContent.Y)
      {
        float bottommostBottom = 0;
        foreach (Target c in Host.Children)
        {
          float y = 0;
          float h = 0;

          if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;
          if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);
          if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);

          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
          if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value);
          if (c.MaxSize.Y.HasValue) h = Math.Min(h, c.MaxSize.Y.Value);

          bottommostBottom = Math.Max(bottommostBottom, y + h);
        }

        Host.MinSize = Host.MinSize with { Y = bottommostBottom };
        Host.MaxSize = Host.MaxSize with { Y = bottommostBottom };
      }

      RequireParentUpdate = false;
    }
  }
}