using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Resizing components to it's Width and placing them sequentially
  /// </summary>
  public class CUILayoutVerticalList : CUILayout
  {
    internal float TotalHeight;
    public CUIDirection Direction;

    public float Gap { get; set; }

    public bool ResizeToHostWidth { get; set; } = true;


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
      Stopwatch sw = new Stopwatch();

      if (Changed)
      {
        Host.InvokeOnLayoutUpdated();

        Sizes.Clear();
        Resizible.Clear();

        TotalHeight = 0;

        sw.Restart();
        foreach (CUIComponent c in Host.Children)
        {
          float h = 0;
          float w = 0;

          if (ResizeToHostWidth)
          {
            w = Host.Real.Width;
          }
          else
          {
            if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Real.Width;
            if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Host.Real.Height;
            if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

            if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Host.Real.Width);
            if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);

            if (!c.RelativeMin.Width.HasValue && !c.AbsoluteMin.Width.HasValue && c.ForcedMinSize.X.HasValue)
            {
              w = Math.Max(w, c.ForcedMinSize.X.Value);
            }

            if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Host.Real.Width);
            if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);
          }

          Vector2 s = new Vector2(w, h);


          if (!c.FillEmptySpace.Y && !c.Ghost.Y)
          {
            if (c.Relative.Height.HasValue)
            {
              h = c.Relative.Height.Value * Host.Real.Height;
              CUIDebug.Capture(Host, c, "VerticalList.Update", "Relative.Height", "h", h.ToString());
            }
            if (c.CrossRelative.Height.HasValue)
            {
              h = c.CrossRelative.Height.Value * Host.Real.Width;
              CUIDebug.Capture(Host, c, "VerticalList.Update", "CrossRelative.Height", "h", h.ToString());
            }
            if (c.Absolute.Height.HasValue)
            {
              h = c.Absolute.Height.Value;
              CUIDebug.Capture(Host, c, "VerticalList.Update", "Absolute.Height", "h", h.ToString());
            }

            if (c.RelativeMin.Height.HasValue)
            {
              h = Math.Max(h, c.RelativeMin.Height.Value * Host.Real.Height);
              CUIDebug.Capture(Host, c, "VerticalList.Update", "RelativeMin.Height", "h", h.ToString());
            }
            if (c.AbsoluteMin.Height.HasValue)
            {
              h = Math.Max(h, c.AbsoluteMin.Height.Value);
              CUIDebug.Capture(Host, c, "VerticalList.Update", "AbsoluteMin.Height", "h", h.ToString());
            }
            if (!c.RelativeMin.Height.HasValue && !c.AbsoluteMin.Height.HasValue && c.ForcedMinSize.Y.HasValue)
            {
              h = Math.Max(h, c.ForcedMinSize.Y.Value);
              CUIDebug.Capture(Host, c, "VerticalList.Update", "ForcedMinSize.Y", "h", h.ToString());
            }

            if (c.RelativeMax.Height.HasValue)
            {
              h = Math.Min(h, c.RelativeMax.Height.Value * Host.Real.Height);
              CUIDebug.Capture(Host, c, "VerticalList.Update", "RelativeMax.Height", "h", h.ToString());
            }
            if (c.AbsoluteMax.Height.HasValue)
            {
              h = Math.Min(h, c.AbsoluteMax.Height.Value);
              CUIDebug.Capture(Host, c, "VerticalList.Update", "AbsoluteMax.Height", "h", h.ToString());
            }

            s = new Vector2(w, h);
            Vector2 okSize = c.AmIOkWithThisSize(s);
            CUIDebug.Capture(Host, c, "VerticalList.Update", "AmIOkWithThisSize", "s", okSize.ToString());

            s = okSize;

            if (!c.Fixed) s = new Vector2(s.X, s.Y / c.Scale);

            TotalHeight += s.Y;
          }

          CUIComponentSize size = new CUIComponentSize(c, s);
          Sizes.Add(size);

          if (c.FillEmptySpace.Y) Resizible.Add(size);
        }

        TotalHeight += Math.Max(0, Host.Children.Count - 1) * Gap;

        sw.Stop();
        //CUI.Log($"{Host} vlist measuring {sw.ElapsedMilliseconds}");

        float dif = Math.Max(0, Host.Real.Height - TotalHeight);

        Resizible.ForEach(c =>
        {
          c.Size = c.Component.AmIOkWithThisSize(new Vector2(c.Size.X, (float)Math.Round(dif / Resizible.Count)));
          //c.Size = new Vector2(c.Size.X, dif / Resizible.Count);

          CUIDebug.Capture(Host, c.Component, "VerticalList.Update", "Resizible.ForEach", "c.Size", c.Size.ToString());
        });


        CUI3DOffset offset = Host.ChildOffsetBounds.Check(Host.ChildrenOffset);



        if (Direction == CUIDirection.Straight)
        {
          float y = 0;
          foreach (CUIComponentSize c in Sizes)
          {
            CUIRect real;

            if (Host.ChildrenBoundaries != null) real = Host.ChildrenBoundaries(Host.Real).Check(0, y, c.Size.X, c.Size.Y);
            else real = new CUIRect(0, y, c.Size.X, c.Size.Y);

            real = offset.Transform(real);
            real = real.Shift(Host.Real.Position);

            c.Component.SetReal(real, "VerticalList.Update");

            y += c.Size.Y + Gap;
          }
        }

        if (Direction == CUIDirection.Reverse)
        {
          float y = Host.Real.Height;
          foreach (CUIComponentSize c in Sizes)
          {
            y -= c.Size.Y + Gap;
            CUIRect real;
            if (Host.ChildrenBoundaries != null) real = Host.ChildrenBoundaries(Host.Real).Check(0, y, c.Size.X, c.Size.Y);
            else real = new CUIRect(0, y, c.Size.X, c.Size.Y);

            real = offset.Transform(real);
            real = real.Shift(Host.Real.Position);

            c.Component.SetReal(real, "VerticalList.Update");
          }
        }
      }

      base.Update();
    }

    internal override void ResizeToContent()
    {
      if (AbsoluteChanged && Host.FitContent.X)
      {
        float tw = 0;
        foreach (CUIComponent c in Host.Children)
        {
          if (c.Ghost.X) continue;

          float w = 0;
          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
          else if (c.ForcedMinSize.X.HasValue) w = Math.Max(w, c.ForcedMinSize.X.Value);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);

          tw = Math.Max(tw, w);
        }

        CUIDebug.Capture(null, Host, "VerticalList.ResizeToContent", "tw", "ForcedMinSize.W", tw.ToString());
        Host.SetForcedMinSize(Host.ForcedMinSize with { X = tw });
      }

      if (AbsoluteChanged && Host.FitContent.Y)
      {
        float th = 0;
        foreach (CUIComponent c in Host.Children)
        {
          if (c.Ghost.Y) continue;

          float h = 0;
          if (!c.FillEmptySpace.Y)
          {
            if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
            if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
            else if (c.ForcedMinSize.Y.HasValue) h = Math.Max(h, c.ForcedMinSize.Y.Value);
            if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);
            th += h;
          }
        }

        CUIDebug.Capture(null, Host, "VerticalList.ResizeToContent", "th", "ForcedMinSize.Y", th.ToString());
        Host.SetForcedMinSize(Host.ForcedMinSize with { Y = th });
      }

      base.ResizeToContent();
    }

    public CUILayoutVerticalList() : base() { }

    public CUILayoutVerticalList(CUIDirection d, CUIComponent host = null) : base(host)
    {
      Direction = d;
    }
  }
}