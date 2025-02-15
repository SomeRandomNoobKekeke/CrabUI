using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Draggable and resizable container for other components
  /// </summary>
  public class CUIFrame : CUIComponent
  {
    public override void Draw(SpriteBatch spriteBatch)
    {
      if (BackgroundVisible) CUI.DrawRectangle(spriteBatch, Real, BackgroundColor, BackgroundSprite);
    }

    public override void DrawFront(SpriteBatch spriteBatch)
    {
      //if (BorderVisible) CUI.DrawBorders(spriteBatch, Real, BorderColor, BorderSprite, BorderThickness);
      // GUI.DrawRectangle(spriteBatch, BorderBox.Position, BorderBox.Size, BorderColor, thickness: BorderThickness);
      CUI.DrawBorders(spriteBatch, this);

      if (OutlineVisible) GUI.DrawRectangle(spriteBatch, OutlineBox.Position, OutlineBox.Size, OutlineColor, thickness: OutlineThickness);

      LeftResizeHandle.Draw(spriteBatch);
      RightResizeHandle.Draw(spriteBatch);

      //base.DrawFront(spriteBatch);
    }

    public event Action OnOpen;
    public event Action OnClose;

    /// <summary>
    /// This will reveal the frame and append it to CUI.Main
    /// </summary>
    public void Open()
    {
      if (CUI.Main == null && Parent != CUI.Main) return;
      CUI.Main.Append(this);
      Revealed = true;
      OnOpen?.Invoke();
    }

    /// <summary>
    /// This will hide the frame and remove it from children of CUI.Main
    /// </summary>
    public void Close()
    {
      RemoveSelf();
      Revealed = false;
      OnClose?.Invoke();
    }

    public CUIFrame() : base()
    {
      CullChildren = true;
      Resizible = true;
      Draggable = true;
    }

    public CUIFrame(float? x = null, float? y = null, float? w = null, float? h = null) : this()
    {
      Relative = new CUINullRect(x, y, w, h);
    }
  }
}