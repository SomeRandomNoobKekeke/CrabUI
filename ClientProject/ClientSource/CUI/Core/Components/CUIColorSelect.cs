using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{

  public class CUIColorSelect : CUIComponent
  {
    private bool Selecting;

    public Action<Color> OnSelected { set { Selected += value; } }
    public event Action<Color> Selected;

    private void SelectColor(Vector2 pos)
    {
      if (!Selecting) return;

      Selected?.Invoke(Background.Sprite.GetPixel(
        (pos - ChildrenRect.Position) / ChildrenRect.Size
      ));
    }

    private void HandleMouseUp(CUIMouseUpEvent e)
    {
      SelectColor(e.Pos);
      Selecting = false;
    }

    protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      base.OnAttachedToMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseUp.Add(HandleMouseUp);
    }
    protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      base.OnDetachedFromMainComponent(mainComponent);
      mainComponent.GlobalEvents.MouseUp.Remove(HandleMouseUp);
    }

    public CUIColorSelect()
    {
      ConsumeMouseEvents = true;
      MouseDown += (e) =>
      {
        Selecting = true;
        SelectColor(e.Pos);
      };

      MouseMoved += (e) => SelectColor(e.Pos);
      // MouseLeave += (e) => Selecting = false;
    }
  }
}