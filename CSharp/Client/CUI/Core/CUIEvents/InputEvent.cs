using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public abstract class InputEvent
  {
    public bool Consumed { get; set; } = false;
    public abstract void Dispatch(IEventConsumer consumer);
  }

  public abstract class CUIMouseEvent : InputEvent
  {
    public Vector2 Pos => Mouse.Pos;
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseEvent(CUIInput.MouseInput mouse) => Mouse = mouse;
  }




  public abstract class CUIMouseButtonEvent : CUIMouseEvent
  {
    public CUIMouseButton Button { get; }

    public CUIMouseButtonEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(mouse)
       => Button = button;
  }

  public class CUIMouseDownEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseDown.Raise(this);
      }
    }

    public CUIMouseDownEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }
    public override string ToString() => $"{Button} Down";
  }

  public class CUIMouseUpEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseUp.Raise(this);
      }
    }

    public CUIMouseUpEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }
    public override string ToString() => $"{Button} Up";
  }

  public class CUIMouseClickEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseClick.Raise(this);
      }
    }

    public CUIMouseClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }
    public override string ToString() => $"{Button} Click";
  }


  public class CUIMouseDoubleClickEvent : CUIMouseButtonEvent
  {
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseDoubleClick.Raise(this);
      }
    }

    public CUIMouseDoubleClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(button, mouse) { }

    public override string ToString() => $"{Button} Double Click";
  }


  public class CUIMouseMovedEvent : CUIMouseEvent
  {
    public Vector2 PosDiff => Mouse.PosDiff;
    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseMoved.Raise(this);
      }
    }

    public CUIMouseMovedEvent(CUIInput.MouseInput mouse) : base(mouse) { }

    public override string ToString() => $"Mouse Moved {Pos}";
  }
}