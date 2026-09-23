using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public partial class ResizeHandle : CUIVisualComponent
  {
    public Vector2 StaticPointAnchor { get; set; }


    public bool Grabbed { get; private set; }

    public Vector2 GrabPoint { get; private set; }
    public Vector2 PrevPinPoint { get; private set; }

    public bool OnlyHorizontal
    {
      get => PreventMovement == new CUIBool2(false, true);
      set => PreventMovement = new CUIBool2(false, true);
    }

    public bool OnlyVertical
    {
      get => PreventMovement == new CUIBool2(true, false);
      set => PreventMovement = new CUIBool2(true, false);
    }
    public CUIBool2 PreventMovement { get; set; } = new CUIBool2(false, false);

    private void Grab(CUIMouseEvent e)
    {
      if (!Host.TryGrab(this)) return;

      Grabbed = true;
      GrabPoint = e.Pos;
      PrevPinPoint = CUIAnchor.PosFromAnchor(Host.Rect, ParentAnchor.Value);
      host.HubMouseMoved += Update;
      host.HubMouseUp += Release;
    }

    public void Update(CUIMouseEvent e)
    {
      Vector2 delta = e.Pos - GrabPoint;
      if (PreventMovement.Y) delta = new Vector2(delta.X, 0);
      if (PreventMovement.X) delta = new Vector2(0, delta.Y);

      Resize(PrevPinPoint + delta);
    }

    private void Resize(Vector2 pinPoint)
    {
      Vector2 staticPoint = CUIAnchor.PosFromAnchor(Host.Rect, StaticPointAnchor);

      CUINullVector2 nullSize = CUIAnchor.NullSizeFrom2PointsWithAnchors(
        staticPoint, StaticPointAnchor,
        pinPoint, ParentAnchor.Value
      );

      Vector2 size = new Vector2(
        nullSize.X.HasValue ? nullSize.X.Value : Host.Rect.Width,
        nullSize.Y.HasValue ? nullSize.Y.Value : Host.Rect.Height
      );

      size = new Vector2(
        Math.Max(size.X, Host.MinSize.X),
        Math.Max(size.Y, Host.MinSize.Y)
      );

      Host.ResizeToAbsoluteRect(
        CUIAnchor.RectFromPointAndSize(
          staticPoint, StaticPointAnchor, size
        ),
        PreventMovement
      );
    }

    private void Release(CUIMouseEvent e)
    {
      Vector2 delta = e.Pos - GrabPoint;
      if (PreventMovement.Y) delta = new Vector2(delta.X, 0);
      if (PreventMovement.X) delta = new Vector2(0, delta.Y);

      Resize(PrevPinPoint + delta);


      Grabbed = false;
      host.HubMouseMoved -= Update;
      host.HubMouseUp -= Release;
      host.Release(this);
    }

    public void ForceRelease()
    {
      Grabbed = false;
      host.HubMouseMoved -= Update;
      host.HubMouseUp -= Release;
      host.Release(this);
    }

  }
}