using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public class CUIVerticalListLayout : Layout
  {
    public interface Target : Layout.Target
    {
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



      public CUIDirection Direction { get; }
      public float? Flex { get; }
      public IReadOnlyList<Target> Children { get; }
      public Vector2 ChildrenOffset { get; }
    }

    public class ChildSize
    {
      public Target Child { get; set; }
      public float Width { get; set; }
      public float Height { get; set; }
    }


    public override void InjectHost(Layout.Target host)
    {
      Host = host as Target;
      base.InjectHost(host);
    }
    public new Target Host { get; private set; }

    public override void UpdateChildren()
    {
      if (Host is null) return;
      if (!RequireChildrenUpdate) return;

      List<ChildSize> sizes = new();
      List<ChildSize> resizables = new();

      float TotalHeight = 0;
      foreach (Target c in Host.Children)
      {
        float w, h;

        w = Host.Rect.Width; // Resize to host width by default
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

        ChildSize size = new ChildSize
        {
          Child = c,
          Width = w,
          Height = h,
        };
        sizes.Add(size);

        if (c.Flex.HasValue)
        {
          resizables.Add(size);
        }
        else
        {
          TotalHeight += h;
        }
      }

      float emptySpace = Host.Rect.Height - TotalHeight;
      float totalFlex = resizables.Sum(size => size.Child.Flex.Value);
      foreach (ChildSize size in resizables)
      {
        size.Height = emptySpace * size.Child.Flex.Value / totalFlex;
      }


      if (Host.Direction == CUIDirection.Straight)
      {
        float y = 0;
        foreach (ChildSize c in sizes)
        {
          c.Child.Rect = new CUIRect(
            Host.Rect.Left + 0 + Host.ChildrenOffset.X,
            Host.Rect.Top + y + Host.ChildrenOffset.Y,
            c.Width,
            c.Height
          );

          y += c.Height;
        }
      }

      if (Host.Direction == CUIDirection.Reverse)
      {
        float y = Host.Rect.Height;
        foreach (ChildSize c in sizes)
        {
          y -= c.Height;

          c.Child.Rect = new CUIRect(
            Host.Rect.Left + 0 + Host.ChildrenOffset.X,
            Host.Rect.Top + y + Host.ChildrenOffset.Y,
            c.Width,
            c.Height
          );
        }
      }

      base.UpdateChildren();
    }

    public override void UpdateParent()
    {
      if (Host.FitContent.X)
      {
        float maxWidth = 0;
        foreach (Target c in Host.Children)
        {
          float w = 0;

          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
          if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value);
          if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);

          maxWidth = Math.Max(maxWidth, w);
        }

        Host.MinSize = Host.MinSize with { X = maxWidth };
        Host.MaxSize = Host.MaxSize with { X = maxWidth };
      }

      if (Host.FitContent.Y)
      {
        float maxHeight = 0;
        foreach (Target c in Host.Children)
        {
          if (c.Flex != null) continue;

          float h = 0;

          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
          if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value);
          if (c.MaxSize.Y.HasValue) h = Math.Min(h, c.MaxSize.Y.Value);

          maxHeight = Math.Max(maxHeight, h);
        }

        Host.MinSize = Host.MinSize with { Y = maxHeight };
        Host.MaxSize = Host.MaxSize with { Y = maxHeight };
      }

      RequireParentUpdate = false;
    }
  }
}