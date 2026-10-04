using System;
using System.Collections.Generic;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework;


namespace CursedUI
{
  public partial class CUIComponent
  {
    //CRINGE
    public class Border_Part : Part, NestedCUISerializable
    {
      public CUIRect Rect
      {
        get => Self._Border.Rect;
        set => Self._Border.Rect = value;
      }

      public CUIRect InnerRect => Self._Border.InnerRect;

      public CUISizes Sizes
      {
        get => Self._Border.Sizes;
        set
        {
          Self._Border.Sizes = value;
          Self.UpdateSizeDiffs(); //This is the only reason why this class exist
        }
      }

      public float Top
      {
        get => Self._Border.Top;
        set => Self._Border.Top = value;
      }

      public float Right
      {
        get => Self._Border.Right;
        set => Self._Border.Right = value;
      }

      public float Bottom
      {
        get => Self._Border.Bottom;
        set => Self._Border.Bottom = value;
      }

      public float Left
      {
        get => Self._Border.Left;
        set => Self._Border.Left = value;
      }

      //TODO
      // [CUISerializableProp]
      public CUISprite Sprite
      {
        get => Self._Border.Sprite;
        set => Self._Border.Sprite = value;
      }

      [CUISerializableProp]
      public Color Color
      {
        get => Sprite.Color;
        set => Sprite.Color = value;
      }

      public bool Contains(Vector2 pos) => Self._Border.Contains(pos);
      public void Draw(CUISpriteBatch spriteBatch) => Self._Border.Draw(spriteBatch);


      public VisualUnit.PrimitiveVisualElement VisualWrapper => Self._Border.VisualWrapper;

      public bool Visible
      {
        get => Self._Border.Visible;
        set => Self._Border.Visible = value;
      }


      public bool MouseOver
      {
        get => Self._Border.MouseOver;
        set => Self._Border.MouseOver = value;
      }
      public bool MousePressed
      {
        get => Self._Border.MousePressed;
        set => Self._Border.MousePressed = value;
      }

      public bool IsPointOnTransparentPixel(Vector2 point) => Self._Border.IsPointOnTransparentPixel(point);

      public bool ConsumeMouseEvents
      {
        get => Self._Border.ConsumeMouseEvents;
        set => Self._Border.ConsumeMouseEvents = value;
      }

      public ClearableEvent<CUIMouseDownEvent> MouseDown => Self._Border.MouseDown;
      public ClearableEvent<CUIMouseUpEvent> MouseUp => Self._Border.MouseUp;
      public ClearableEvent<CUIMouseClickEvent> MouseClick => Self._Border.MouseClick;
      public ClearableEvent<CUIMouseDoubleClickEvent> MouseDoubleClick => Self._Border.MouseDoubleClick;
      public ClearableEvent<CUIMouseMovedEvent> MouseMoved => Self._Border.MouseMoved;
      public ClearableEvent<CUIMouseEnterEvent> MouseEnter => Self._Border.MouseEnter;
      public ClearableEvent<CUIMouseLeaveEvent> MouseLeave => Self._Border.MouseLeave;
      public ClearableEvent<CUIMouseOnEvent> MouseOn => Self._Border.MouseOn;
      public ClearableEvent<CUIMouseOffEvent> MouseOff => Self._Border.MouseOff;
      public ClearableEvent<CUIMouseScrollEvent> MouseScroll => Self._Border.MouseScroll;
    }

    [CUISerializableProp]
    public Border_Part Border { get; } = new();
  }
}