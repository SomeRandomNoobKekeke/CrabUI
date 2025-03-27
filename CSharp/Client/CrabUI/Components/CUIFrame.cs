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
    public event Action OnOpen;
    public event Action OnClose;

    [CUISerializable] public bool MoveToFrontOnClick { get; set; } = true;

    /// <summary>
    /// This will reveal the frame and append it to CUI.Main
    /// </summary>
    public void Open(CUIComponent Host = null)
    {
      Host ??= CUI.Main;
      if (Host == null || Parent == Host) return;

      Host.Append(this);
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
      ConsumeMouseClicks = true;

      //TODO remove this temporary workaround after events rework
      OnMouseDown += (e) =>
      {
        if (MoveToFrontOnClick && CUI.Main.MouseOn == this) MoveToFront();
      };

      //From CUICloseButton
      AddCommand("Close Frame", (o) => Close());
    }

    public CUIFrame(float? x = null, float? y = null, float? w = null, float? h = null) : this()
    {
      Relative = new CUINullRect(x, y, w, h);
    }
  }
}