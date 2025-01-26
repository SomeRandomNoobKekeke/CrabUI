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
  public class CUIDragHandle : ICUIVitalizable
  {
    public void SetHost(CUIComponent host) => Host = host;
    public CUIComponent Host;
    public Vector2 GrabOffset;
    public bool Grabbed;
    public bool Draggable;
    public CUIMouseEvent Trigger = CUIMouseEvent.Down;

    public bool ShouldStart(CUIInput input)
    {
      return Draggable && (
        (Trigger == CUIMouseEvent.Down && input.MouseDown) ||
        (Trigger == CUIMouseEvent.DClick && input.DoubleClick)
      );
    }
    public void BeginDrag(Vector2 cursorPos)
    {
      Grabbed = true;
      GrabOffset = cursorPos - CUIAnchor.PosIn(Host);
    }

    public void EndDrag()
    {
      Grabbed = false;
      CUI.Main.OnDragEnd(this);
    }

    //TODO test in 3d child offset
    public void DragTo(Vector2 to)
    {
      Vector2 pos = Host.Parent.ChildrenOffset.ToPlaneCoords(
        to - GrabOffset - CUIAnchor.PosIn(Host.Parent.Real, Host.ParentAnchor ?? Host.Anchor)
      );

      Host.CUIProps.Absolute.SetValue(Host.Absolute with { Position = pos });
      Host.InvokeOnDrag(pos.X, pos.Y);
    }

    public CUIDragHandle() { }
    public CUIDragHandle(CUIComponent host) => Host = host;
  }
}