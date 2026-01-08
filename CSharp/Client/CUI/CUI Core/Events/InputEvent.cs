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
    public abstract void Dispatch(IEventConsumer consumer);
  }

  public abstract class CUIMouseEvent : InputEvent
  {
    public Vector2 Pos => Mouse.Pos;
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseEvent(CUIInput.MouseInput mouse) => Mouse = mouse;
  }

  public class CUIMouseDownEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseDown.Raise(this);
      }
    }

    public CUIMouseDownEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(mouse)
    {
      Button = button;
    }

    public override string ToString() => $"{Button} Down";
  }

  public class CUIMouseUpEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseUp.Raise(this);
      }
    }

    public CUIMouseUpEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(mouse)
    {
      Button = button;
    }

    public override string ToString() => $"{Button} Up";
  }

  public class CUIMouseClickEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseClick.Raise(this);
      }
    }

    public CUIMouseClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(mouse)
    {
      Button = button;
    }

    public override string ToString() => $"{Button} Click";
  }


  public class CUIMouseDoubleClickEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }

    public override void Dispatch(IEventConsumer consumer)
    {
      if (consumer is IMouseEventConsumer MEConsumer)
      {
        MEConsumer.MouseDoubleClick.Raise(this);
      }
    }
    public CUIMouseDoubleClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse) : base(mouse)
    {
      Button = button;
    }

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

    public CUIMouseMovedEvent(CUIInput.MouseInput mouse) : base(mouse)
    {

    }

    public override string ToString() => $"Mouse Moved {Pos}";
  }
}