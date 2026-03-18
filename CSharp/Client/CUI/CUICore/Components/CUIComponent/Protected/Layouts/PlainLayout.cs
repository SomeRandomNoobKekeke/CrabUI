using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public class PlainLayout : Layout
  {
    public interface Target : Layout.Target
    {
      public CUINullRect Absolute { get; }
      public CUINullRect Relative { get; }
      public IReadOnlyList<Target> Children { get; }
    }

    public override void InjectHost(Layout.Target host) { Host = host as Target; }
    public Target Host { get; private set; }

    public override void UpdateChildren()
    {
      if (Host is null) return;
      if (!RequireChildrenUpdate) return;

      foreach (Target c in Host.Children)
      {
        float x, y, w, h;

        x = 0;
        if (c.Relative.Left.HasValue) x = Host.Rect.Left + c.Relative.Left.Value * Host.Rect.Width;
        // if (c.CrossRelative.Left.HasValue) x = c.CrossRelative.Left.Value * Host.Rect.Height;
        if (c.Absolute.Left.HasValue) x = Host.Rect.Left + c.Absolute.Left.Value;

        // if (c.RelativeMin.Left.HasValue) x = Math.Max(x, c.RelativeMin.Left.Value * Host.Rect.Width);
        // if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);
        // if (c.RelativeMax.Left.HasValue) x = Math.Min(x, c.RelativeMax.Left.Value * Host.Rect.Width);
        // if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);


        y = 0;
        if (c.Relative.Top.HasValue) y = Host.Rect.Top + c.Relative.Top.Value * Host.Rect.Height;
        // if (c.CrossRelative.Top.HasValue) y = c.CrossRelative.Top.Value * Host.Rect.Width;
        if (c.Absolute.Top.HasValue) y = Host.Rect.Top + c.Absolute.Top.Value;

        // if (c.RelativeMin.Top.HasValue) y = Math.Max(y, c.RelativeMin.Top.Value * Host.Rect.Height);
        // if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);
        // if (c.RelativeMax.Top.HasValue) y = Math.Min(y, c.RelativeMax.Top.Value * Host.Rect.Height);
        // if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);


        w = 0;
        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Rect.Width;
        // if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Host.Rect.Height;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        // if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Host.Rect.Width);
        // if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
        // if (c.ForcedMinSize.X.HasValue) w = Math.Max(w, c.ForcedMinSize.X.Value);
        // if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Host.Rect.Width);
        // if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);


        h = 0;
        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Rect.Height;
        // if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Host.Rect.Width;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        // if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Host.Rect.Height);
        // if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
        // if (c.ForcedMinSize.Y.HasValue) h = Math.Max(h, c.ForcedMinSize.Y.Value);
        // if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Host.Rect.Height);
        // if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);

        c.Rect = new CUIRect(x, y, w, h);
      }


      RequireChildrenUpdate = false;
    }

    public override void UpdateParent()
    {
      RequireParentUpdate = false;
    }
  }
}