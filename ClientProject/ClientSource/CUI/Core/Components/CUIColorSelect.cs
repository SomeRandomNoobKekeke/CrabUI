using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{

  public class CUIColorSelect : CUIPosSelect
  {
    //TODO Mb i shouldn't hide base selected
    public new Action<Color> OnSelected { set { Selected += value; } }
    public new event Action<Color> Selected;

    protected override void SelectPos(Vector2 pos)
    {
      if (!Selecting) return;

      //TODO remove code duplication
      Vector2 v = (pos - ChildrenRect.Position) / ChildrenRect.Size;
      v = new Vector2(Math.Clamp(v.X, 0, 1), Math.Clamp(v.Y, 0, 1));

      Selected?.Invoke(Background.Sprite.GetPixel(v));
    }

  }
}