using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class CUIEvent
  {
  }

  public class CUIMouseEvent : CUIEvent
  {

  }

  public class CUIMouseDownEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseDownEvent(CUIMouseButton button, CUIInput.MouseInput mouse)
    {
      Button = button;
      Mouse = mouse;
    }

    public override string ToString() => $"{Button} Down";
  }

  public class CUIMouseUpEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseUpEvent(CUIMouseButton button, CUIInput.MouseInput mouse)
    {
      Button = button;
      Mouse = mouse;
    }

    public override string ToString() => $"{Button} Up";
  }

  public class CUIMouseClickEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse)
    {
      Button = button;
      Mouse = mouse;
    }

    public override string ToString() => $"{Button} Click";
  }

  public class CUIMouseDoubleClickEvent : CUIMouseEvent
  {
    CUIMouseButton Button { get; }
    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseDoubleClickEvent(CUIMouseButton button, CUIInput.MouseInput mouse)
    {
      Button = button;
      Mouse = mouse;
    }

    public override string ToString() => $"{Button} Double Click";
  }

  public class CUIMouseMovedEvent : CUIMouseEvent
  {
    public Vector2 Pos => Mouse.Pos;
    public Vector2 PosDiff => Mouse.PosDiff;


    public CUIInput.MouseInput Mouse { get; }

    public CUIMouseMovedEvent(CUIInput.MouseInput mouse)
    {
      Mouse = mouse;
    }

    public override string ToString() => $"Mouse Moved {Pos}";
  }
}