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
  /// <summary>
  /// CUIAnchor is just a Vector2  
  /// This is a static class containing some helper methods
  /// </summary>
  public class CUIAnchor
  {
    public static Vector2 TopLeft = new Vector2(0.0f, 0.0f);
    public static Vector2 TopCenter = new Vector2(0.5f, 0.0f);
    public static Vector2 TopRight = new Vector2(1.0f, 0.0f);
    public static Vector2 CenterLeft = new Vector2(0.0f, 0.5f);
    public static Vector2 Center = new Vector2(0.5f, 0.5f);
    public static Vector2 CenterRight = new Vector2(1.0f, 0.5f);
    public static Vector2 BottomLeft = new Vector2(0.0f, 1.0f);
    public static Vector2 BottomCenter = new Vector2(0.5f, 1.0f);
    public static Vector2 BottomRight = new Vector2(1.0f, 1.0f);

    public static Vector2 Direction(Vector2 anchor)
    {
      return (Center - anchor) * 2;
    }

    public static Vector2 PosIn(CUIComponent host) => PosIn(host.Real, host.Anchor);
    public static Vector2 PosIn(CUIRect rect, Vector2 anchor)
    {
      return new Vector2(
        rect.Left + rect.Width * anchor.X,
        rect.Top + rect.Height * anchor.Y
      );
    }

    public static Vector2 AnchorFromPos(CUIRect rect, Vector2 pos)
    {
      return (pos - rect.Position) / rect.Size;
    }

    public static Vector2 GetChildPos(CUIRect parent, Vector2 anchor, Vector2 offset, Vector2 childSize)
    {
      return PosIn(parent, anchor) + offset - PosIn(new CUIRect(childSize), anchor);
    }

    public static Vector2 GetChildPos(CUIRect parent, Vector2 parentAnchor, Vector2 offset, Vector2 childSize, Vector2 anchor)
    {
      return PosIn(parent, parentAnchor) + offset - PosIn(new CUIRect(childSize), anchor);
    }
  }
}