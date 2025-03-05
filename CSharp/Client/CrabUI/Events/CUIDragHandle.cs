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
    /// <summary>
    /// If true, will change relative prop instead of Absolute
    /// </summary>
    public bool DragRelative { get; set; } = false;
    public bool OutputRealPos { get; set; } = false;

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
      Host.MainComponent?.OnDragEnd(this);
    }

    //TODO test in 3d child offset
    public void DragTo(Vector2 to)
    {
      Vector2 pos = Host.Parent.ChildrenOffset.ToPlaneCoords(
        to - GrabOffset - CUIAnchor.PosIn(Host.Parent.Real, Host.ParentAnchor ?? Host.Anchor)
      );

      if (DragRelative)
      {
        Vector2 newRelPos = new Vector2(
          pos.X / Host.Parent.Real.Width,
          pos.Y / Host.Parent.Real.Height
        );
        Host.CUIProps.Relative.SetValue(Host.Relative with { Position = newRelPos });
        Host.InvokeOnDrag(newRelPos.X, newRelPos.Y);
      }
      else
      {
        Host.CUIProps.Absolute.SetValue(Host.Absolute with { Position = pos });
        if (OutputRealPos) Host.InvokeOnDrag(to.X, to.Y);
        else Host.InvokeOnDrag(pos.X, pos.Y);
      }
    }

    public CUIDragHandle() { }
    public CUIDragHandle(CUIComponent host) => Host = host;
  }
}