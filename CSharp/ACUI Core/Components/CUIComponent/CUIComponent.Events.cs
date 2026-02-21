using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent : IMouseEventConsumer
  {
    public CUIEvent<CUIMouseDownEvent> MouseDown
    {
      get => Background.MouseDown;
      set => Background.MouseDown = value;
    }

    public CUIEventHandler<CUIMouseDownEvent> OnMouseDown { set { MouseDown += value; } }

    public CUIEvent<CUIMouseUpEvent> MouseUp
    {
      get => Background.MouseUp;
      set => Background.MouseUp = value;
    }
    public CUIEvent<CUIMouseClickEvent> MouseClick
    {
      get => Background.MouseClick;
      set => Background.MouseClick = value;
    }
    public CUIEvent<CUIMouseDoubleClickEvent> MouseDoubleClick
    {
      get => Background.MouseDoubleClick;
      set => Background.MouseDoubleClick = value;
    }
    public CUIEvent<CUIMouseMovedEvent> MouseMoved
    {
      get => Background.MouseMoved;
      set => Background.MouseMoved = value;
    }

    public CUIEvent Updated { get; set; } = new CUIEvent();
  }
}