using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public class CUIAnchor
  {
    public static Vector2 LeftTop = new Vector2(0.0f, 0.0f);
    public static Vector2 CenterTop = new Vector2(0.5f, 0.0f);
    public static Vector2 RightTop = new Vector2(1.0f, 0.0f);
    public static Vector2 LeftCenter = new Vector2(0.0f, 0.5f);
    public static Vector2 Center = new Vector2(0.5f, 0.5f);
    public static Vector2 RightCenter = new Vector2(1.0f, 0.5f);
    public static Vector2 LeftBottom = new Vector2(0.0f, 1.0f);
    public static Vector2 CenterBottom = new Vector2(0.5f, 1.0f);
    public static Vector2 RightBottom = new Vector2(1.0f, 1.0f);

    public static Vector2 Direction(Vector2 anchor)
    {
      return (Center - anchor) * 2;
    }

    public static Vector2 PosFromAnchor(CUIRect rect, Vector2 anchor)
    {
      return new Vector2(
        rect.Left + rect.Width * anchor.X,
        rect.Top + rect.Height * anchor.Y
      );
    }

    public static Vector2 PosFromAnchor(Vector2 parentSize, Vector2 anchor)
    {
      return new Vector2(
        parentSize.X * anchor.X,
        parentSize.Y * anchor.Y
      );
    }

    public static Vector2 AnchorFromPos(CUIRect rect, Vector2 pos)
    {
      return (pos - rect.Position) / rect.Size;
    }

    public static Vector2 GetOffset(CUIRect parentRect, Vector2 parentAnchor, CUIRect childRect, Vector2 childAnchor)
    {
      return PosFromAnchor(childRect, childAnchor) - PosFromAnchor(parentRect, parentAnchor);
    }

    /// <summary>
    /// E.g. bottom right child vertex is attached to bottom right parent vertex
    /// </summary>
    public static Vector2 ChildPosIn(CUIRect parent, Vector2 anchor, Vector2 childSize)
    {
      return PosFromAnchor(parent, anchor) - PosFromAnchor(childSize, anchor);
    }

    public static Vector2 ChildPosIn(Vector2 parentSize, Vector2 anchor, Vector2 childSize)
    {
      return PosFromAnchor(parentSize, anchor) - PosFromAnchor(childSize, anchor);
    }

    /// <summary>
    /// childAnchor point in child is attached to parentAnchor point in parent
    /// </summary>
    public static Vector2 ChildPosIn(CUIRect parent, Vector2 parentAnchor, Vector2 childSize, Vector2 childAnchor)
    {
      return PosFromAnchor(parent, parentAnchor) - PosFromAnchor(new CUIRect(childSize), childAnchor);
    }

    public static Vector2 ChildPosIn(Vector2 parentSize, Vector2 parentAnchor, Vector2 childSize, Vector2 childAnchor)
    {
      return PosFromAnchor(parentSize, parentAnchor) - PosFromAnchor(new CUIRect(childSize), childAnchor);
    }

    public static CUIRect RectFrom2PointsWithAnchors(Vector2 point1, Vector2 anchor1, Vector2 point2, Vector2 anchor2)
    {
      // point1 = position + size * anchor1;
      // point2 = position + size * anchor2;

      // position = point1 - size * anchor1;
      // position = point2 - size * anchor2;

      // point1 - size * anchor1 = point2 - size * anchor2
      // size * (anchor1  -  anchor2) = point1 - point2

      Vector2 size = (point1 - point2) / (anchor1 - anchor2);
      Vector2 position = point1 - size * anchor1;

      return new CUIRect(position, size);
    }

    public static CUIRect RectFromPointAndSize(Vector2 point1, Vector2 anchor1, Vector2 size)
    {
      Vector2 position = point1 - size * anchor1;

      return new CUIRect(position, size);
    }

    public static CUIRect AbsoluteRectToAchored(CUIRect rect, CUIRect parentRect, Vector2 anchor)
    {
      return new CUIRect(
        GetOffset(parentRect, anchor, rect, anchor),
        rect.Size
      );
    }
  }
}