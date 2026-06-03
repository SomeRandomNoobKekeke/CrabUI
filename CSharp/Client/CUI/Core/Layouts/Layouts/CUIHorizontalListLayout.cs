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
  public class CUIHorizontalListLayout : Layout
  {
    public interface Target : Layout.Target
    {
      public CUIRect Rect { get; set; }
      public CUINullRect Absolute { get; }
      public CUINullRect Relative { get; }
      public CUIDirection Direction { get; }
      public float? Flex { get; }
      public IReadOnlyList<Target> Children { get; }
    }

    public class ChildSize
    {
      public Target Child { get; set; }
      public float Width { get; set; }
      public float Height { get; set; }
    }


    public override void InjectHost(Layout.Target host) { Host = host as Target; }
    public Target Host { get; private set; }

    public override void UpdateChildren()
    {
      if (Host is null) return;
      if (!RequireChildrenUpdate) return;

      List<ChildSize> sizes = new();
      List<ChildSize> resizables = new();

      float TotalWidth = 0;
      foreach (Target c in Host.Children)
      {
        float w, h;

        h = Host.Rect.Height; // Resize to host Height by default
        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Rect.Height;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        w = 0;
        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Rect.Width;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

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
          TotalWidth += w;
        }
      }

      float emptySpace = Host.Rect.Width - TotalWidth;
      float totalFlex = resizables.Sum(size => size.Child.Flex.Value);
      foreach (ChildSize size in resizables)
      {
        size.Width = emptySpace * size.Child.Flex.Value / totalFlex;
      }


      if (Host.Direction == CUIDirection.Straight)
      {
        float x = 0;
        foreach (ChildSize c in sizes)
        {
          c.Child.Rect = new CUIRect(
            Host.Rect.Left + x,
            Host.Rect.Top + 0,
            c.Width,
            c.Height
          );

          x += c.Width;
        }
      }
      else
      {
        float x = Host.Rect.Width;
        foreach (ChildSize c in sizes)
        {
          x -= c.Width;

          c.Child.Rect = new CUIRect(
            Host.Rect.Left + x,
            Host.Rect.Top + 0,
            c.Width,
            c.Height
          );
        }
      }

      RequireChildrenUpdate = false;
    }

    public override void UpdateParent()
    {
      RequireParentUpdate = false;
    }
  }
}