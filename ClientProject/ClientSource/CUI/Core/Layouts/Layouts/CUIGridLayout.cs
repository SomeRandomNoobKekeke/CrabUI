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
  /// <summary>
  /// It's still very primitive
  /// </summary>
  public class CUIGridLayout : Layout
  {
    public struct Line
    {
      public float? AbsoluteSize;
      public float? RelativeSize;
      public float? Fraction;

      public Line(float? absolute = null, float? relative = null, float? fraction = null)
      {
        AbsoluteSize = absolute;
        RelativeSize = relative;
        Fraction = fraction;
      }
    }


    public interface Host : Layout.Host
    {
      public ICollection<Line> RowSizes { get; }
      public ICollection<Line> ColumnSizes { get; }
    }
    public interface Child : Layout.ChildBase
    {
      public int GridRow { get; }
      public int GridColumn { get; }
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

      if (Parent.RowSizes.Count == 0 || Parent.ColumnSizes.Count == 0)
      {
        base.UpdateChildren();
        return;
      }


      List<CUISegment> RealRowSizes = new();
      List<CUISegment> RealColumnSizes = new();

      float y = Parent.ChildrenRect.Top;
      float fry = Parent.ChildrenRect.Height / Parent.RowSizes.Where(l => l.Fraction.HasValue).Sum(l => l.Fraction.Value);
      foreach (Line line in Parent.RowSizes)
      {
        float size = 0;
        if (line.Fraction.HasValue) size = fry * line.Fraction.Value;
        if (line.RelativeSize.HasValue) size = Parent.ChildrenRect.Height * line.RelativeSize.Value;
        if (line.AbsoluteSize.HasValue) size = line.AbsoluteSize.Value;

        RealRowSizes.Add(new CUISegment() { Left = y, Width = size });

        y += size;
      }


      float x = Parent.ChildrenRect.Left;
      float frx = Parent.ChildrenRect.Width / Parent.ColumnSizes.Where(l => l.Fraction.HasValue).Sum(l => l.Fraction.Value);
      foreach (Line line in Parent.ColumnSizes)
      {
        float size = 0;
        if (line.Fraction.HasValue) size = frx * line.Fraction.Value;
        if (line.RelativeSize.HasValue) size = Parent.ChildrenRect.Width * line.RelativeSize.Value;
        if (line.AbsoluteSize.HasValue) size = line.AbsoluteSize.Value;

        RealColumnSizes.Add(new CUISegment() { Left = x, Width = size });

        x += size;
      }



      foreach (Layout.Child c in Parent.Children)
      {
        int i = Math.Clamp(c.GridColumn - 1, 0, RealColumnSizes.Count - 1);
        int j = Math.Clamp(c.GridRow - 1, 0, RealRowSizes.Count - 1);

        c.OuterRect = new CUIRect(
          RealColumnSizes[i].Left,
          RealRowSizes[j].Left,
          RealColumnSizes[i].Width,
          RealRowSizes[j].Width
        );
      }

      base.UpdateChildren();
    }

    public override void UpdateParent()
    {
      RequireParentUpdate = false;
    }
  }
}