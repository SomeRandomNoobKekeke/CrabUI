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
  /// Default layout, simple descartes coordinate plane
  /// </summary>
  public class CUILayoutSimple : CUILayout
  {
    internal override void Update()
    {
      if (Changed && Host.Children.Count > 0)
      {
        Host.InvokeOnLayoutUpdated();

        CUI3DOffset offset = Host.ChildOffsetBounds.Check(Host.ChildrenOffset);

        foreach (CUIComponent c in Host.Children)
        {
          float x, y, w, h;

          x = 0;
          if (c.Relative.Left.HasValue) x = c.Relative.Left.Value * Host.Real.Width;
          if (c.CrossRelative.Left.HasValue) x = c.CrossRelative.Left.Value * Host.Real.Height;
          if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;

          if (c.RelativeMin.Left.HasValue) x = Math.Max(x, c.RelativeMin.Left.Value * Host.Real.Width);
          if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);

          if (c.RelativeMax.Left.HasValue) x = Math.Min(x, c.RelativeMax.Left.Value * Host.Real.Width);
          if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);


          y = 0;
          if (c.Relative.Top.HasValue) y = c.Relative.Top.Value * Host.Real.Height;
          if (c.CrossRelative.Top.HasValue) y = c.CrossRelative.Top.Value * Host.Real.Width;
          if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;

          if (c.RelativeMin.Top.HasValue) y = Math.Max(y, c.RelativeMin.Top.Value * Host.Real.Height);
          if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);

          if (c.RelativeMax.Top.HasValue) y = Math.Min(y, c.RelativeMax.Top.Value * Host.Real.Height);
          if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);


          w = 0;
          if (c.Relative.Width.HasValue) w = c.Relative.Width.Value * Host.Real.Width;
          if (c.CrossRelative.Width.HasValue) w = c.CrossRelative.Width.Value * Host.Real.Height;
          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;

          if (c.RelativeMin.Width.HasValue) w = Math.Max(w, c.RelativeMin.Width.Value * Host.Real.Width);
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);

          if (c.ForcedMinSize.X.HasValue) w = Math.Max(w, c.ForcedMinSize.X.Value);

          if (c.RelativeMax.Width.HasValue) w = Math.Min(w, c.RelativeMax.Width.Value * Host.Real.Width);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);


          h = 0;
          if (c.Relative.Height.HasValue) h = c.Relative.Height.Value * Host.Real.Height;
          if (c.CrossRelative.Height.HasValue) h = c.CrossRelative.Height.Value * Host.Real.Width;
          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;

          if (c.RelativeMin.Height.HasValue) h = Math.Max(h, c.RelativeMin.Height.Value * Host.Real.Height);
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          if (c.ForcedMinSize.Y.HasValue) h = Math.Max(h, c.ForcedMinSize.Y.Value);



          if (c.RelativeMax.Height.HasValue) h = Math.Min(h, c.RelativeMax.Height.Value * Host.Real.Height);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);


          (w, h) = c.AmIOkWithThisSize(new Vector2(w, h));
          (x, y) = CUIAnchor.GetChildPos(
            new CUIRect(Vector2.Zero, Host.Real.Size),
            c.ParentAnchor ?? c.Anchor,
            new Vector2(x, y),
            new Vector2(w, h),
            c.Anchor
          );
          CUIRect real;
          if (Host.ChildrenBoundaries != null)
          {
            real = Host.ChildrenBoundaries(Host.Real).Check(x, y, w, h);
          }
          else
          {
            real = new CUIRect(x, y, w, h);
          }

          if (!c.Fixed)
          {
            real = offset.Transform(real);
          }
          //TODO guh...
          real = real.Shift(Host.Real.Position);


          c.SetReal(real, "Simple Layout update");
        }
      }

      base.Update();
    }

    internal override void ResizeToContent()
    {
      if (AbsoluteChanged && Host.FitContent.X)
      {
        float rightmostRight = 0;
        foreach (CUIComponent c in Host.Children)
        {
          if (c.Ghost.X) continue;

          float x = 0;
          float w = 0;

          if (c.Absolute.Left.HasValue) x = c.Absolute.Left.Value;
          if (c.AbsoluteMin.Left.HasValue) x = Math.Max(x, c.AbsoluteMin.Left.Value);
          if (c.AbsoluteMax.Left.HasValue) x = Math.Min(x, c.AbsoluteMax.Left.Value);

          if (c.Absolute.Width.HasValue) w = c.Absolute.Width.Value;
          if (c.AbsoluteMin.Width.HasValue) w = Math.Max(w, c.AbsoluteMin.Width.Value);
          else if (c.ForcedMinSize.X.HasValue) w = Math.Max(w, c.ForcedMinSize.X.Value);
          if (c.AbsoluteMax.Width.HasValue) w = Math.Min(w, c.AbsoluteMax.Width.Value);

          rightmostRight = Math.Max(rightmostRight, x + w);
        }

        Host.SetForcedMinSize(Host.ForcedMinSize with { X = rightmostRight });
      }

      if (AbsoluteChanged && Host.FitContent.Y)
      {
        float bottommostBottom = 0;
        foreach (CUIComponent c in Host.Children)
        {
          if (c.Ghost.Y) continue;

          float y = 0;
          float h = 0;

          if (c.Absolute.Top.HasValue) y = c.Absolute.Top.Value;
          if (c.AbsoluteMin.Top.HasValue) y = Math.Max(y, c.AbsoluteMin.Top.Value);
          if (c.AbsoluteMax.Top.HasValue) y = Math.Min(y, c.AbsoluteMax.Top.Value);

          if (c.Absolute.Height.HasValue) h = c.Absolute.Height.Value;
          if (c.AbsoluteMin.Height.HasValue) h = Math.Max(h, c.AbsoluteMin.Height.Value);
          else if (c.ForcedMinSize.Y.HasValue) h = Math.Max(h, c.ForcedMinSize.Y.Value);
          if (c.AbsoluteMax.Height.HasValue) h = Math.Min(h, c.AbsoluteMax.Height.Value);

          bottommostBottom = Math.Max(bottommostBottom, y + h);
        }

        Host.SetForcedMinSize(Host.ForcedMinSize with { Y = bottommostBottom });
      }

      base.ResizeToContent();
    }

    public CUILayoutSimple() : base() { }
    public CUILayoutSimple(CUIComponent host) : base(host) { }
  }
}