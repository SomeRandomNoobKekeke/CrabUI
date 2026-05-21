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
      public CUINullRect Relative { get; }
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
        if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;

        y = 0;
        if (c.Relative.Top.HasValue) y = c.Relative.Top.Value * Host.Rect.Height;
        if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;

        w = 0;
        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Rect.Width;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        h = 0;
        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Rect.Height;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;



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
      RequireParentUpdate = false;
    }
  }
}