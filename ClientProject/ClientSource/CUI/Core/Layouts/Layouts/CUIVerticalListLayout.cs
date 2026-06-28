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
    public interface Host : Layout.Host
    {
      public CUIDirection Direction { get; }
      public float TotalHeight { set; }
    }
    public interface Child : Layout.ChildBase
    {
      public float? Flex { get; }
    }

    private Host Parent;
    public override void ConnectTo(Layout.Host host)
    {
      base.ConnectTo(host);
      Parent = host as Host;
    }


    public class ChildSize
    {
      public Layout.Child Child { get; set; }
      public float Width { get; set; }
      public float Height { get; set; }
    }

    public float TotalHeight { get; private set; }

    public override void UpdateChildren()
    {
      if (Parent is null) return;
      if (!RequireChildrenUpdate) return;

      List<ChildSize> sizes = new();
      List<ChildSize> resizables = new();

      TotalHeight = 0;
      foreach (Layout.Child c in Parent.Children)
      {
        float w = Parent.InnerRect.Width;// Resize to host width by default
        float h = 0;

        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Parent.InnerRect.Width;
        if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Parent.InnerRect.Height;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Parent.InnerRect.Width);
        if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
        if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value);

        if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Parent.InnerRect.Width);
        if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
        if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);


        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Parent.InnerRect.Height;
        if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Parent.InnerRect.Width;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Parent.InnerRect.Height);
        if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
        if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value);

        if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Parent.InnerRect.Height);
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

      float emptySpace = Parent.InnerRect.Height - TotalHeight;
      float totalFlex = resizables.Sum(size => size.Child.Flex.Value);
      foreach (ChildSize size in resizables)
      {
        size.Height = emptySpace * size.Child.Flex.Value / totalFlex;
      }


      if (Parent.Direction == CUIDirection.Straight)
      {
        float y = 0;
        foreach (ChildSize c in sizes)
        {
          c.Child.OuterRect = new CUIRect(
            Parent.InnerRect.Left + 0 + Parent.ChildrenOffset.X,
            Parent.InnerRect.Top + y + Parent.ChildrenOffset.Y,
            c.Width,
            c.Height
          );

          y += c.Height;
        }
      }

      if (Parent.Direction == CUIDirection.Reverse)
      {
        float y = Parent.InnerRect.Height;
        foreach (ChildSize c in sizes)
        {
          y -= c.Height;

          c.Child.OuterRect = new CUIRect(
            Parent.InnerRect.Left + 0 + Parent.ChildrenOffset.X,
            Parent.InnerRect.Top + y + Parent.ChildrenOffset.Y,
            c.Width,
            c.Height
          );
        }
      }


      Parent.TotalHeight = TotalHeight;//HACK

      base.UpdateChildren();
    }

    public override void UpdateParent()
    {
      if (Parent.FitContent.X)
      {
        float maxWidth = 0;
        foreach (Layout.Child c in Parent.Children)
        {
          float w = 0;

          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
          if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value);
          if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);

          maxWidth = Math.Max(maxWidth, w);
        }

        Parent.MinSize = Parent.MinSize with { X = maxWidth };
        Parent.MaxSize = Parent.MaxSize with { X = maxWidth };
      }

      if (Parent.FitContent.Y)
      {
        float maxHeight = 0;
        foreach (Layout.Child c in Parent.Children)
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

        Parent.MinSize = Parent.MinSize with { Y = maxHeight };
        Parent.MaxSize = Parent.MaxSize with { Y = maxHeight };
      }

      RequireParentUpdate = false;
    }
  }
}