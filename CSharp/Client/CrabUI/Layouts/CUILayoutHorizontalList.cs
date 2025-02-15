using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Resizing components to it's Height and placing them sequentially
  /// </summary>
  public class CUILayoutHorizontalList : CUILayout
  {
    internal float TotalWidth;
    public CUIDirection Direction;
    public bool ResizeToHostHeight { get; set; } = true;

    private class CUIComponentSize
    {
      public CUIComponent Component;
      public Vector2 Size;
      public CUIComponentSize(CUIComponent component, Vector2 size)
      {
        Component = component;
        Size = size;
      }
    }
    private List<CUIComponentSize> Sizes = new List<CUIComponentSize>();
    private List<CUIComponentSize> Resizible = new List<CUIComponentSize>();

    internal override void Update()
    {
      if (Changed)
      {
        Host.InvokeOnLayoutUpdated();

        Sizes.Clear();
        Resizible.Clear();

        TotalWidth = 0;


        foreach (CUIComponent c in Host.Children)
        {
          float h = 0;
          float w = 0;

          if (ResizeToHostHeight)
          {
            h = Host.Real.Height;
          }
          else
          {
            if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Real.Height;
            if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Host.Real.Width;
            if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

            if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Host.Real.Height);
            if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);

            if (!c.RelativeMin.Height.HasValue && !c.AbsoluteMin.Height.HasValue && c.ForcedMinSize.Y.HasValue)
            {
              h = Math.Max(h, c.ForcedMinSize.Y.Value);
            }

            if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Host.Real.Height);
            if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
          }

          Vector2 s = new Vector2(w, h);


          if (!c.FillEmptySpace.X && !c.Ghost.X)
          {
            if (c.Relative.Width.HasValue)
            {
              w = c.Relative.Width.Value * Host.Real.Width;
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "Relative.Width", "w", w.ToString());
            }
            if (c.CrossRelative.Width.HasValue)
            {
              w = c.CrossRelative.Width.Value * Host.Real.Height;
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "CrossRelative.Width", "w", w.ToString());
            }
            if (c.Absolute.Width.HasValue)
            {
              w = c.Absolute.Width.Value;
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "Absolute.Width", "w", w.ToString());
            }

            if (c.RelativeMin.Width.HasValue)
            {
              w = Math.Max(w, c.RelativeMin.Width.Value * Host.Real.Width);
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "RelativeMin.Width", "w", w.ToString());
            }
            if (c.AbsoluteMin.Width.HasValue)
            {
              w = Math.Max(w, c.AbsoluteMin.Width.Value);
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "AbsoluteMin.Width", "w", w.ToString());
            }
            if (!c.RelativeMin.Width.HasValue && !c.AbsoluteMin.Width.HasValue && c.ForcedMinSize.X.HasValue)
            {
              w = Math.Max(w, c.ForcedMinSize.X.Value);
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "ForcedMinSize.X", "w", w.ToString());
            }

            if (c.RelativeMax.Width.HasValue)
            {
              w = Math.Min(w, c.RelativeMax.Width.Value * Host.Real.Width);
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "RelativeMax.Width", "w", w.ToString());
            }
            if (c.AbsoluteMax.Width.HasValue)
            {
              w = Math.Min(w, c.AbsoluteMax.Width.Value);
              CUIDebug.Capture(Host, c, "HorizontalList.Update", "AbsoluteMax.Width", "w", w.ToString());
            }

            s = new Vector2(w, h);
            Vector2 okSize = c.AmIOkWithThisSize(s);
            CUIDebug.Capture(Host, c, "HorizontalList.Update", "AmIOkWithThisSize", "s", okSize.ToString());

            s = okSize;

            if (!c.Fixed) s = new Vector2(s.X / c.Scale, s.Y);

            TotalWidth += s.X;
          }

          CUIComponentSize size = new CUIComponentSize(c, s);
          Sizes.Add(size);

          if (c.FillEmptySpace.X) Resizible.Add(size);
        }



        float dif = Math.Max(0, Host.Real.Width - TotalWidth);

        Resizible.ForEach(c =>
        {
          c.Size = c.Component.AmIOkWithThisSize(new Vector2((float)Math.Round(dif / Resizible.Count), c.Size.Y));
          //c.Size = new Vector2(dif / Resizible.Count, c.Size.Y);
          CUIDebug.Capture(Host, c.Component, "HorizontalList.Update", "Resizible.ForEach", "c.Size", c.Size.ToString());
        });


        CUI3DOffset offset = Host.ChildOffsetBounds.Check(Host.ChildrenOffset);

        if (Direction == CUIDirection.Straight)
        {
          float x = 0;
          foreach (CUIComponentSize c in Sizes)
          {
            CUIRect real;
            if (Host.ChildrenBoundaries != null)
            {
              real = Host.ChildrenBoundaries(Host.Real).Check(x, 0, c.Size.X, c.Size.Y);
            }
            else
            {
              real = new CUIRect(x, 0, c.Size.X, c.Size.Y);
            }

            real = offset.Transform(real);
            real = real.Shift(Host.Real.Position);

            c.Component.SetReal(real, "HorizontalList layout update");

            x += c.Size.X;
          }
        }

        if (Direction == CUIDirection.Reverse)
        {
          float x = Host.Real.Width;
          foreach (CUIComponentSize c in Sizes)
          {
            x -= c.Size.X;

            CUIRect real;
            if (Host.ChildrenBoundaries != null)
            {
              real = Host.ChildrenBoundaries(Host.Real).Check(x, 0, c.Size.X, c.Size.Y);
            }
            else
            {
              real = new CUIRect(x, 0, c.Size.X, c.Size.Y);
            }
            real = offset.Transform(real);
            real = real.Shift(Host.Real.Position);

            c.Component.SetReal(real, "HorizontalList layout update");
          }
        }

      }

      base.Update();
    }

    //TODO sync with vlist
    internal override void ResizeToContent()
    {
      if (AbsoluteChanged && Host.FitContent.X)
      {
        float tw = 0;
        foreach (CUIComponent c in Host.Children)
        {
          float w = 0;
          if (!c.FillEmptySpace.X)
          {
            if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
            if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
            else if (c.ForcedMinSize.X.HasValue) w = Math.Max(w, c.ForcedMinSize.X.Value);
            if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
            tw += w;
          }
        }

        CUIDebug.Capture(null, Host, "HorizontalList ResizeToContent", "tw", "ForcedMinSize.X", tw.ToString());
        Host.SetForcedMinSize(Host.ForcedMinSize with { X = tw });
      }

      if (AbsoluteChanged && Host.FitContent.Y)
      {
        float th = 0;
        foreach (CUIComponent c in Host.Children)
        {
          float h = 0;
          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          else if (c.ForcedMinSize.Y.HasValue) h = Math.Max(h, c.ForcedMinSize.Y.Value);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
          th = Math.Max(th, h);
        }

        CUIDebug.Capture(null, Host, "HorizontalList ResizeToContent", "th", "ForcedMinSize.Y", th.ToString());
        Host.SetForcedMinSize(Host.ForcedMinSize with { Y = th });
      }

      base.ResizeToContent();
    }
    public CUILayoutHorizontalList() : base() { }
    public CUILayoutHorizontalList(CUIDirection d, CUIComponent host = null) : base(host)
    {
      Direction = d;
    }
  }
}