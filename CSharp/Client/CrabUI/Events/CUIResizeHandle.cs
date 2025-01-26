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
  public class CUIResizeHandle : ICUIVitalizable
  {
    public void SetHost(CUIComponent host) => Host = host;
    public CUIComponent Host;
    public CUIRect Real;

    public Vector2 Anchor;
    public Vector2 StaticPointAnchor;
    public Vector2 AnchorDif;

    public CUINullRect Absolute;

    //TODO unhardcode these colors
    public Color BackgroundColor = Color.White * 0.25f;
    public Color GrabbedColor = Color.Cyan * 0.5f;
    public Vector2 MemoStaticPoint;


    public bool Grabbed;
    public bool Visible = false;

    public CUIMouseEvent Trigger = CUIMouseEvent.Down;

    public bool ShouldStart(CUIInput input)
    {
      return Visible && Real.Contains(input.MousePosition) && (
        (Trigger == CUIMouseEvent.Down && input.MouseDown) ||
        (Trigger == CUIMouseEvent.DClick && input.DoubleClick)
      );
    }

    public void BeginResize(Vector2 cursorPos)
    {
      Grabbed = true;
      MemoStaticPoint = CUIAnchor.PosIn(Host.Real, StaticPointAnchor);
    }

    public void EndResize()
    {
      Grabbed = false;
      CUI.Main.OnResizeEnd(this);
    }

    public void Resize(Vector2 cursorPos)
    {
      float limitedX;
      if (CUIAnchor.Direction(StaticPointAnchor).X >= 0)
      {
        limitedX = Math.Max(MemoStaticPoint.X + Real.Width, cursorPos.X);
      }
      else
      {
        limitedX = Math.Min(MemoStaticPoint.X - Real.Width, cursorPos.X);
      }
      float limitedY;
      if (CUIAnchor.Direction(StaticPointAnchor).Y >= 0)
      {
        limitedY = Math.Max(MemoStaticPoint.Y + Real.Height, cursorPos.Y);
      }
      else
      {
        limitedY = Math.Min(MemoStaticPoint.Y - Real.Height, cursorPos.Y);
      }

      Vector2 LimitedCursorPos = new Vector2(limitedX, limitedY);


      Vector2 RealDif = MemoStaticPoint - LimitedCursorPos;
      Vector2 SizeFactor = RealDif / AnchorDif;
      Vector2 TopLeft = MemoStaticPoint - SizeFactor * StaticPointAnchor;


      Vector2 newSize = new Vector2(
        Math.Max(Real.Width, SizeFactor.X),
        Math.Max(Real.Height, SizeFactor.Y)
      );

      Vector2 newPos = TopLeft - CUIAnchor.PosIn(Host.Parent.Real, Host.ParentAnchor ?? Host.Anchor) + CUIAnchor.PosIn(new CUIRect(newSize), Host.Anchor);

      Host.CUIProps.Absolute.SetValue(new CUINullRect(newPos, newSize));
    }
    public void Update()
    {
      if (!Visible) return;

      float x, y, w, h;
      x = y = w = h = 0;

      if (Absolute.Left.HasValue) x = Absolute.Left.Value;
      if (Absolute.Top.HasValue) y = Absolute.Top.Value;
      if (Absolute.Width.HasValue) w = Absolute.Width.Value;
      if (Absolute.Height.HasValue) h = Absolute.Height.Value;

      Vector2 Pos = CUIAnchor.GetChildPos(Host.Real, Anchor, new Vector2(x, y), new Vector2(w, h));

      Real = new CUIRect(Pos, new Vector2(w, h));
    }
    public void Draw(SpriteBatch spriteBatch)
    {
      if (!Visible) return;
      GUI.DrawRectangle(spriteBatch, Real.Position, Real.Size, Grabbed ? GrabbedColor : BackgroundColor, isFilled: true);
    }

    public CUIResizeHandle(Vector2 anchor)
    {
      if (anchor == CUIAnchor.Center)
      {
        CUI.Log($"Pls don't use CUIAnchor.Center for CUIResizeHandle, it makes no sense:\nThe StaticPointAnchor is symetric to Anchor and in this edge case == Anchor");
      }

      Anchor = anchor;
      StaticPointAnchor = Vector2.One - Anchor;
      AnchorDif = StaticPointAnchor - Anchor;

      Absolute = new CUINullRect(0, 0, 15, 10);
      //Host.AbsoluteMin = Host.AbsoluteMin with { Size = Absolute.Size };
    }

    public CUIResizeHandle(CUIComponent host, Vector2 anchor)
    {
      Host = host;

      if (anchor == CUIAnchor.Center)
      {
        Host.Info($"Pls don't use CUIAnchor.Center for CUIResizeHandle, it makes no sense:\nThe StaticPointAnchor is symetric to Anchor and in this edge case == Anchor");
      }

      Anchor = anchor;
      StaticPointAnchor = Vector2.One - Anchor;
      AnchorDif = StaticPointAnchor - Anchor;

      Absolute = new CUINullRect(0, 0, 15, 10);
      //Host.AbsoluteMin = Host.AbsoluteMin with { Size = Absolute.Size };
    }
  }
}