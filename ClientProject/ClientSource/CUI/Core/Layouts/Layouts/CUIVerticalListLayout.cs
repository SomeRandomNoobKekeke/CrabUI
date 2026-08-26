using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
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
        //TODO add prop for it
        float w = Parent.ChildrenRect.Width;// Resize to host width by default
        float h = 0;

        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Parent.ChildrenRect.Width;
        if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Parent.ChildrenRect.Height;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Parent.ChildrenRect.Width);
        if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
        if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value + c.OutToChildDiff.FullWidth);

        if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Parent.ChildrenRect.Width);
        if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
        if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);


        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Parent.ChildrenRect.Height;
        if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Parent.ChildrenRect.Width;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Parent.ChildrenRect.Height);
        if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
        if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value + c.OutToChildDiff.FullHeigth);

        if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Parent.ChildrenRect.Height);
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

      if (resizables.Count > 0)
      {
        float emptySpace = Parent.ChildrenRect.Height - TotalHeight;
        float totalFlex = resizables.Sum(size => size.Child.Flex.Value);

        float spaceleft = emptySpace;
        for (int i = 0; i < resizables.Count - 1; i++)
        {
          resizables[i].Height = emptySpace * resizables[i].Child.Flex.Value / totalFlex;
          spaceleft -= resizables[i].Height;
        }

        resizables.Last().Height = spaceleft;
      }

      if (Parent.Direction == CUIDirection.Straight)
      {
        float y = 0;
        foreach (ChildSize c in sizes)
        {
          c.Child.OuterRect = new CUIRect(
            Parent.ChildrenRect.Left + 0 + Parent.ChildrenOffset.X,
            Parent.ChildrenRect.Top + y + Parent.ChildrenOffset.Y,
            c.Width,
            c.Height
          );

          y += c.Height;
        }
      }

      if (Parent.Direction == CUIDirection.Reverse)
      {
        float y = Parent.ChildrenRect.Height;
        foreach (ChildSize c in sizes)
        {
          y -= c.Height;

          c.Child.OuterRect = new CUIRect(
            Parent.ChildrenRect.Left + 0 + Parent.ChildrenOffset.X,
            Parent.ChildrenRect.Top + y + Parent.ChildrenOffset.Y,
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
          if (c.MinSize.X.HasValue) w = Math.Max(w, c.MinSize.X.Value + c.OutToChildDiff.FullWidth);
          if (c.MaxSize.X.HasValue) w = Math.Min(w, c.MaxSize.X.Value);

          maxWidth = Math.Max(maxWidth, w);
        }

        Parent.MinSize = Parent.MinSize with { X = maxWidth };
        Parent.MaxSize = Parent.MaxSize with { X = maxWidth };
      }

      if (Parent.FitContent.Y)
      {
        float totalHeight = 0;
        foreach (Layout.Child c in Parent.Children)
        {
          if (c.Flex != null) continue;

          float h = 0;

          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
          if (c.MinSize.Y.HasValue) h = Math.Max(h, c.MinSize.Y.Value + c.OutToChildDiff.FullHeigth);
          if (c.MaxSize.Y.HasValue) h = Math.Min(h, c.MaxSize.Y.Value);

          totalHeight += h;
        }

        Parent.MinSize = Parent.MinSize with { Y = totalHeight };
        Parent.MaxSize = Parent.MaxSize with { Y = totalHeight };
      }

      RequireParentUpdate = false;
    }
  }
}