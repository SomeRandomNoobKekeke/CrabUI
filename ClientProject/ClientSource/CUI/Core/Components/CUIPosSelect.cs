using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{

  public class CUIPosSelect : CUIComponent
  {
    protected bool Selecting;

    public Action<Vector2> OnSelected { set { Selected += value; } }
    public event Action<Vector2> Selected;

    protected virtual void SelectPos(Vector2 pos)
    {
      if (!Selecting) return;
      Vector2 v = (pos - ChildrenRect.Position) / ChildrenRect.Size;
      v = new Vector2(Math.Clamp(v.X, 0, 1), Math.Clamp(v.Y, 0, 1));
      Selected?.Invoke(v);
    }

    private void HandleMouseUp(CUIMouseUpEvent e)
    {
      Selecting = false;
    }

    private void HandleMouseMoved(CUIMouseMovedEvent e)
    {
      SelectPos(e.Pos);
    }

    protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      base.OnAttachedToMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseUp.Add(HandleMouseUp);
      mainComponent.GlobalEvents.MouseMoved.Add(HandleMouseMoved);
    }
    protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      base.OnDetachedFromMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseUp.Remove(HandleMouseUp);
      mainComponent.GlobalEvents.MouseMoved.Remove(HandleMouseMoved);
    }

    public CUIPosSelect()
    {
      ConsumeMouseEvents = true;
      MouseDown += (e) =>
      {
        Selecting = true;
        SelectPos(e.Pos);
      };

      // MouseLeave += (e) => SelectPos(e.Pos);
    }
  }
}