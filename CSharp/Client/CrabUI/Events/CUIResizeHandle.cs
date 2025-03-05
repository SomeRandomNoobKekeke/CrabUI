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

    public CUISprite Sprite;
    public Vector2 MemoStaticPoint;

    public bool Grabbed;
    public bool Visible = false;

    public CUIBool2 Direction { get; set; } = new CUIBool2(true, true);

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
      Host.MainComponent?.OnResizeEnd(this);
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

      if (Direction.X) Host.CUIProps.Absolute.SetValue(new CUINullRect(newPos.X, Host.Absolute.Top, newSize.X, Host.Absolute.Height));
      if (Direction.Y) Host.CUIProps.Absolute.SetValue(new CUINullRect(Host.Absolute.Left, newPos.Y, Host.Absolute.Width, newSize.Y));
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
      CUI.DrawRectangle(spriteBatch, Real, Grabbed ? Host.ResizeHandleGrabbedColor : Host.ResizeHandleColor, Sprite);
    }

    public CUIResizeHandle(Vector2 anchor, CUIBool2 flipped)
    {
      if (anchor == CUIAnchor.Center)
      {
        CUI.Log($"Pls don't use CUIAnchor.Center for CUIResizeHandle, it makes no sense:\nThe StaticPointAnchor is symetric to Anchor and in this edge case == Anchor");
      }

      Anchor = anchor;
      StaticPointAnchor = Vector2.One - Anchor;
      AnchorDif = StaticPointAnchor - Anchor;

      Absolute = new CUINullRect(0, 0, 15, 15);
      Sprite = CUI.TextureManager.GetSprite(CUI.CUITexturePath);
      Sprite.SourceRect = new Rectangle(0, 32, 32, 32);
      if (flipped.X) Sprite.Effects |= SpriteEffects.FlipHorizontally;
      if (flipped.Y) Sprite.Effects |= SpriteEffects.FlipVertically;
    }
  }
}