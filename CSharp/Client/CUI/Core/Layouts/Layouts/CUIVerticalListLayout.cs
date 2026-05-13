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
      public CUIRect Rect { get; set; }
      public CUINullRect Absolute { get; }
      public CUINullRect Relative { get; }
      public CUIDirection Direction { get; }
      public IReadOnlyList<Target> Children { get; }
    }

    public record ChildSize(Target Child, float Width, float Height);


    public override void InjectHost(Layout.Target host) { Host = host as Target; }
    public Target Host { get; private set; }

    public override void UpdateChildren()
    {
      if (Host is null) return;
      if (!RequireChildrenUpdate) return;

      List<ChildSize> sizes = new();
      foreach (Target c in Host.Children)
      {
        float w, h;

        w = 0;
        if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Rect.Width;
        if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

        h = 0;
        if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Rect.Height;
        if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

        sizes.Add(new ChildSize(c, w, h));
      }

      if (Host.Direction == CUIDirection.Straight)
      {
        float y = 0;
        foreach (ChildSize c in sizes)
        {
          c.Child.Rect = new CUIRect(
            Host.Rect.Left + 0,
            Host.Rect.Top + y,
            c.Width,
            c.Height
          );

          y += c.Height;
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